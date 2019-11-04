using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : Page
    {
        // The Frame to navigate between pages.
        Frame parentFrame;

        // The object of the first player.
        Player player1;

        // The object of the second player.
        Player player2;

        // The string that holds the current difficulty.
        string difficulty;

        // The amount of pairs that is left on the grid.
        int pairs;

        // The grid where the cards are being generated.
        MemoryGrid grid;

        // The integer to define how much rows are needed in the grid.
        private int rowSize = 4;

        // The integer to define how much cols are needed in the grid.
        private int colSize = 4;

        /// <summary>
        ///     Initialize a new game screen to begin a new game.
        /// </summary>
        /// <param name="parentFrame">The Frame to navigate between pages.</param>
        /// <param name="player1">The object of the first player.</param>
        /// <param name="player2">The object of the second player.</param>
        /// <param name="difficulty">The string that holds the current difficulty.</param>
        public GameScreen(Frame parentFrame, Player player1, Player player2, string difficulty)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
            this.player1 = player1;
            this.player2 = player2;
            this.difficulty = difficulty;

            SetGridSize(this.difficulty);

            grid = new MemoryGrid(this, cardHolder, player1, player2, colSize, rowSize);
        }

        /// <summary>
        ///     Initialize a new game screen to continue a previous game.
        /// </summary>
        /// <param name="parentFrame">The Frame to navigate between pages.</param>
        /// <param name="saveFile">The XML save file of the previous game.</param>
        /// <param name="player1Element">The element of the first player of the previous game.</param>
        /// <param name="player2Element">The element of the second player of the previous game.</param>
        /// <param name="cardsElement">The element that contains the status of all cards of the previous game.</param>
        public GameScreen(Frame parentFrame, XmlDocument saveFile, XmlNode player1Element, XmlNode player2Element, XmlNode cardsElement)
        {
            InitializeComponent();
            
            this.parentFrame = parentFrame;

            this.player1 = new Player(player1Element.ChildNodes.Item(0).InnerText, Convert.ToInt32(player1Element.ChildNodes.Item(1).InnerText), Convert.ToBoolean(player1Element.ChildNodes.Item(2).InnerText));
            this.player2 = new Player(player2Element.ChildNodes.Item(0).InnerText, Convert.ToInt32(player2Element.ChildNodes.Item(1).InnerText), Convert.ToBoolean(player2Element.ChildNodes.Item(2).InnerText));
            this.difficulty = saveFile.GetElementsByTagName("difficulty").Item(0).InnerText;
            this.pairs = Convert.ToInt32(saveFile.GetElementsByTagName("pairs").Item(0).InnerText);

            SetGridSize(this.difficulty);

            grid = new MemoryGrid(this, cardHolder, player1, player2, colSize, rowSize);

            LoadData(cardsElement);
        }

        /// <summary>
        ///     The click event of the restart button.
        ///     This restarts the game to start over.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
        private void RestartButton_Click(object sender, RoutedEventArgs args)
        {
            MessageBoxResult result = MessageBox.Show("Weet je zeker dat je opnieuw wilt beginnen? Als je dit doet kom je niet op het scorebord.", "Opnieuw beginnen", MessageBoxButton.YesNo);

            if (result.Equals(MessageBoxResult.Yes))
            {
                grid = new MemoryGrid(this, cardHolder, player1, player2, colSize, rowSize);

                player1.SetScore(0);
                player2.SetScore(0);

                player1.SetTurn(true);
                player2.SetTurn(false);

                UpdateLabels(player1, player2);
            }
        }

        /// <summary>
        ///     The click event of the quit and save button.
        ///     This stops the game and saves the game data to a save file. Then it navigates to the main menu.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
        private void QuitButton_Click(object sender, RoutedEventArgs args)
        {
            SaveData();
            parentFrame.Navigate(new MainMenu(this.parentFrame));
        }

        /// <summary>
        ///     The click event to exit button.
        ///     This stops the game and naigates to the main menu without saving.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
        private void ExitButton_Click(object sender, RoutedEventArgs args)
        {
            MessageBoxResult result = MessageBox.Show("Als je doorgaat verlies je alle voortgang", "Zeker weten?", MessageBoxButton.YesNo);

            if (result.Equals(MessageBoxResult.Yes))
            {
                parentFrame.Navigate(new MainMenu(this.parentFrame));
            }
        }

        /// <summary>
        ///     Load the game data from the save file.
        /// </summary>
        /// <param name="cardsElement">The element that contains the status of all cards of the previous game.</param>
        private void LoadData(XmlNode cardsElement)
        {
            grid.SetPairs(pairs);
            UpdateLabels(player1, player2);

            // Save the amount of cards in the grid.
            int cardsAmount = grid.GetCards().Count;

            // Clear the grid.
            grid.GetGrid().Children.Clear();
            grid.GetCards().Clear();

            // Loop through all cards and change the values of these cards.
            for (int i = 0; i < cardsAmount; i++)
            {
                grid.ChangeCards(
                    new BitmapImage(new Uri(cardsElement.ChildNodes.Item(i).ChildNodes.Item(0).InnerText, UriKind.Relative)),
                    new BitmapImage(new Uri(cardsElement.ChildNodes.Item(i).ChildNodes.Item(1).InnerText, UriKind.Relative)),
                    Convert.ToBoolean(cardsElement.ChildNodes.Item(i).ChildNodes.Item(2).InnerText),
                    Convert.ToBoolean(cardsElement.ChildNodes.Item(i).ChildNodes.Item(3).InnerText),
                    Convert.ToInt32(cardsElement.ChildNodes.Item(i).ChildNodes.Item(4).InnerText)
                );
            }

            grid.ShowCards();
        }

        /// <summary>
        ///     Save game data to a save file.
        /// </summary>
        private void SaveData()
        {
            int cardCount = 0;

            foreach(Card card in grid.GetCards())
            {
                if (card.GetVisibility() == true)
                {
                    cardCount++;
                }
            }

            pairs = cardCount / 2;

            // Create a new save file in the "Saves" directory.
            XmlWriter writer = XmlWriter.Create("Saves/memory.sav");
            writer.WriteStartElement("game");

            WriteTag(writer, "difficulty", difficulty);
            WriteTag(writer, "pairs", pairs.ToString());

            writer.WriteStartElement("cards");

            // Loop through all the cards on the grid and save their values to the save file.
            for (int i = 0; i < grid.GetCards().Count; i++)
            {
                writer.WriteStartElement("card");

                WriteTag(writer, "backImg", grid.GetCards()[i].GetBackImage().ToString().Replace("pack://application:,,,/MemoryGame;component/", ""));
                WriteTag(writer, "frontImg", grid.GetCards()[i].GetFrontImage().ToString().Replace("pack://application:,,,/MemoryGame;component/", ""));
                WriteTag(writer, "clicked", grid.GetCards()[i].GetClicked().ToString());
                WriteTag(writer, "visibility", grid.GetCards()[i].GetVisibility().ToString());
                WriteTag(writer, "imgNr", grid.GetCards()[i].GetImgNumber().ToString());

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            // Save the properties of player 1.
            SavePlayer(writer, player1);

            // Save the properties of player 2.
            SavePlayer(writer, player2);

            // Close the document.
            writer.WriteEndElement();
            writer.Close();
        }

        /// <summary>
        ///     Save all properties of a player in the save file.
        /// </summary>
        /// <param name="writer">The XML document to write to.</param>
        /// <param name="player">The player object that needs to be saved.</param>
        private void SavePlayer(XmlWriter writer, Player player)
        {
            writer.WriteStartElement("player");

            WriteTag(writer, "name", player.GetName());
            WriteTag(writer, "score", player.GetScore().ToString());
            WriteTag(writer, "turn", player.GetTurn().ToString());

            writer.WriteEndElement();
        }

        /// <summary>
        ///     Write a new element to the XML save file.
        /// </summary>
        /// <param name="writer">The XML document to write to.</param>
        /// <param name="elementName">The name of the element that needs to be saved.</param>
        /// <param name="value">The value that needs to be saved in the element.</param>
        private void WriteTag(XmlWriter writer, string elementName, string value)
        {
            writer.WriteStartElement(elementName);
            writer.WriteString(value);
            writer.WriteEndElement();
        }

        /// <summary>
        ///     Update the labels of the players.
        /// </summary>
        /// <param name="player1">The object of the first player.</param>
        /// <param name="player2">The object of the second player.</param>
        public void UpdateLabels(Player player1, Player player2)
        {
            player1NameLabel.Content = player1.GetName();
            player2NameLabel.Content = player2.GetName();

            player1ScoreLabel.Text = player1.GetScore().ToString();
            player2ScoreLabel.Text = player2.GetScore().ToString();

            if (player1.GetTurn() == true)
            {
                player1NameLabel.Foreground = Brushes.Green;
                player2NameLabel.Foreground = Brushes.Black;
            }
            else
            {
                player1NameLabel.Foreground = Brushes.Black;
                player2NameLabel.Foreground = Brushes.Green;
            }
        }

        /// <summary>
        ///     Save the player object of the player who has won in a save file.
        /// </summary>
        /// <param name="player">The object of the player who has won the game.</param>
        public void SaveHighscores(Player player)
        {
            // Check if the save exists and is not empty.
            if (!File.Exists("Saves/scores.sav") || new FileInfo("Saves/scores.sav").Length == 0)
            {
                // Create a new save file in the "Saves" directory.
                XmlWriter writer = XmlWriter.Create("Saves/scores.sav");
                writer.WriteStartElement("highscores");

                writer.WriteStartElement("highscore");
                WriteTag(writer, "playerName", player.GetName());
                WriteTag(writer, "score", player.GetScore().ToString());
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.Close();
            } 
            else
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("Saves/scores.sav");
                XmlNode parentNode = xmlDocument.GetElementsByTagName("highscores").Item(0);

                XmlElement parentElement = xmlDocument.CreateElement("highscore");
                XmlElement playerNameElement = xmlDocument.CreateElement("playerName");
                XmlElement scoreElement = xmlDocument.CreateElement("score");

                playerNameElement.InnerText = player.GetName();
                scoreElement.InnerText = player.GetScore().ToString();

                parentElement.AppendChild(playerNameElement);
                parentElement.AppendChild(scoreElement);

                for (int i = parentNode.ChildNodes.Count - 1; i >= 0; i--)
                {
                    // Check if the winners score is lower than the current highscore.
                    if (Convert.ToInt32(parentNode.ChildNodes.Item(i).ChildNodes.Item(1).InnerText) > player.GetScore())
                    {
                        // Check if the last highscore is selected.
                        if (i == parentNode.ChildNodes.Count - 1)
                        {
                            return;
                        }

                        parentNode.InsertAfter(parentElement, parentNode.ChildNodes.Item(i));

                        RemoveLastChildSaveFile(parentNode);
                        xmlDocument.Save("Saves/scores.sav");

                        return;
                    }

                    // Check if the current highscore is the highest and if the winners score is higher than the current highscore.
                    if (i == 0 && player.GetScore() > Convert.ToInt32(parentNode.ChildNodes.Item(i).ChildNodes.Item(1).InnerText))
                    {
                        parentNode.InsertBefore(parentElement, parentNode.ChildNodes.Item(0));

                        RemoveLastChildSaveFile(parentNode);
                        xmlDocument.Save("Saves/scores.sav");

                        return;
                    }
                }
            }
        }

        /// <summary>
        ///     Set the size of the grid depending on the difficulty.
        /// </summary>
        /// <param name="difficulty">The string that holds the current difficulty.</param>
        public void SetGridSize(string difficulty)
        {
            switch (difficulty)
            {
                case "Makkelijk":
                    rowSize = 4;
                    colSize = 4;
                    break;
                case "Normaal":
                    rowSize = 6;
                    colSize = 6;
                    break;
                case "Moeilijk":
                    rowSize = 8;
                    colSize = 8;
                    break;
            }
        }

        /// <summary>
        ///     Check if there are more than 10 highscores in the safe file.
        /// </summary>
        /// <param name="parentNode">The root element of the save file.</param>
        private void RemoveLastChildSaveFile(XmlNode parentNode)
        {
            if (parentNode.ChildNodes.Count > 10)
            {
                parentNode.RemoveChild(parentNode.LastChild);
            }
        }

        /// <summary>
        ///     Get the frame that is used to navigate between pages.
        /// </summary>
        /// <returns>The Frame to navigate between pages.</returns>
        public Frame GetParentFrame()
        {
            return this.parentFrame;
        }
    }
}
