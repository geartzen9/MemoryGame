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
        Frame parentFrame;
        Player player1, player2;
        string difficulty;

        MemoryGrid grid;
        private int rowSize = 4, colSize = 4;

        // Constructor to begin a new game.
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

        // Constructor to continue a previous game.
        public GameScreen(Frame parentFrame)
        {
            InitializeComponent();
            
            this.parentFrame = parentFrame;

            XmlDocument saveFile = new XmlDocument();
            saveFile.Load("Saves/memory.sav");

            var player1Element = saveFile.GetElementsByTagName("player").Item(0);
            var player2Element = saveFile.GetElementsByTagName("player").Item(1);
            this.player1 = new Player(player1Element.ChildNodes.Item(0).InnerText, Convert.ToInt32(player1Element.ChildNodes.Item(1).InnerText), Convert.ToBoolean(player1Element.ChildNodes.Item(2).InnerText));
            this.player2 = new Player(player2Element.ChildNodes.Item(0).InnerText, Convert.ToInt32(player2Element.ChildNodes.Item(1).InnerText), Convert.ToBoolean(player2Element.ChildNodes.Item(2).InnerText));
            this.difficulty = saveFile.GetElementsByTagName("difficulty").Item(0).InnerText;

            SetGridSize(this.difficulty);

            grid = new MemoryGrid(this, cardHolder, player1, player2, colSize, rowSize);

            LoadData();
        }

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

        private void QuitButton_Click(object sender, RoutedEventArgs args)
        {
            SaveData();
            parentFrame.Navigate(new MainMenu(this.parentFrame));
        }

        private void LoadData()
        {
            XmlDocument saveFile = new XmlDocument();
            saveFile.Load("Saves/memory.sav");

            var player1Element = saveFile.GetElementsByTagName("player").Item(0);
            var player2Element = saveFile.GetElementsByTagName("player").Item(1);
            var cardsElement = saveFile.GetElementsByTagName("cards").Item(0);

            this.player1 = new Player(player1Element.ChildNodes.Item(0).InnerText, Convert.ToInt32(player1Element.ChildNodes.Item(1).InnerText), Convert.ToBoolean(player1Element.ChildNodes.Item(2).InnerText));
            this.player2 = new Player(player2Element.ChildNodes.Item(0).InnerText, Convert.ToInt32(player2Element.ChildNodes.Item(1).InnerText), Convert.ToBoolean(player2Element.ChildNodes.Item(2).InnerText));

            UpdateLabels(player1, player2);

            int cardsAmount = grid.GetCards().Count;
            grid.GetGrid().Children.Clear();
            grid.GetCards().Clear();

            for (int i = 0; i < cardsAmount; i++)
            {
                grid.ChangeCards(
                    i,
                    new BitmapImage(new Uri(cardsElement.ChildNodes.Item(i).ChildNodes.Item(0).InnerText, UriKind.Relative)),
                    new BitmapImage(new Uri(cardsElement.ChildNodes.Item(i).ChildNodes.Item(1).InnerText, UriKind.Relative)),
                    Convert.ToBoolean(cardsElement.ChildNodes.Item(i).ChildNodes.Item(2).InnerText),
                    Convert.ToBoolean(cardsElement.ChildNodes.Item(i).ChildNodes.Item(3).InnerText),
                    Convert.ToInt32(cardsElement.ChildNodes.Item(i).ChildNodes.Item(4).InnerText)
                );
            }

            grid.ShowCards();
        }

        private void SaveData()
        {
            // Create a new save file in the "Saves" directory.
            XmlWriter writer = XmlWriter.Create("Saves/memory.sav");
            writer.WriteStartElement("game");

            WriteTag(writer, "difficulty", difficulty);

            writer.WriteStartElement("cards");

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

        // Saves all properties of a player in the save file.
        private void SavePlayer(XmlWriter writer, Player player)
        {
            writer.WriteStartElement("player");

            WriteTag(writer, "name", player.GetName());
            WriteTag(writer, "score", player.GetScore().ToString());
            WriteTag(writer, "turn", player.GetTurn().ToString());

            writer.WriteEndElement();
        }

        private void WriteTag(XmlWriter writer, string elementName, string value)
        {
            writer.WriteStartElement(elementName);
            writer.WriteString(value);
            writer.WriteEndElement();
        }

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

        public void SaveHighscores(Player player)
        {
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
                    Console.WriteLine("player score: " + player.GetScore());
                    Console.WriteLine("old score: " + parentNode.ChildNodes.Item(i).InnerText);

                    // Als de winner score lager is dan de huidige highscore.
                    if (Convert.ToInt32(parentNode.ChildNodes.Item(i).ChildNodes.Item(1).InnerText) > player.GetScore())
                    {
                        // Controleer of de laatste highscore is geselecteerd.
                        if (i == parentNode.ChildNodes.Count - 1)
                        {
                            return;
                        }    

                        parentNode.InsertAfter(parentElement, parentNode.ChildNodes.Item(i));
                    }
                }

                parentNode.InsertBefore(parentElement, parentNode.ChildNodes.Item(0));

                if (parentNode.ChildNodes.Count > 10)
                {
                    parentNode.RemoveChild(parentNode.LastChild);
                }

                xmlDocument.Save("Saves/scores.sav");
            }
        }

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

        public Frame GetParentFrame()
        {
            return this.parentFrame;
        }
    }
}
