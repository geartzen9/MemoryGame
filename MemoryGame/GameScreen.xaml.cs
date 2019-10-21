using System;
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
        Player player1;
        Player player2;
        string difficulty;

        MemoryGrid grid;
        private int rowSize = 4;
        private int colSize = 4;
        private int theme_nbr = 1;

        // Constructor to begin a new game.
        public GameScreen(Frame parentFrame, Player player1, Player player2, string difficulty)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
            this.player1 = player1;
            this.player2 = player2;
            this.difficulty = difficulty;

            grid = new MemoryGrid(cardHolder, colSize, rowSize, theme_nbr);
        }

        // Constructor to continue a previous game.
        public GameScreen(Frame parentFrame)
        {
            InitializeComponent();
            
            this.parentFrame = parentFrame;
            grid = new MemoryGrid(cardHolder, colSize, rowSize, theme_nbr);

            LoadData();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs args)
        {
            MessageBoxResult result = MessageBox.Show("Weet je zeker dat je opnieuw wilt beginnen? Als je dit doet kom je niet op het scorebord.", "Opnieuw beginnen", MessageBoxButton.YesNo);

            if (result.Equals(MessageBoxResult.Yes))
            {
                grid = new MemoryGrid(cardHolder, colSize, rowSize, theme_nbr);

                player1.SetScore(0);
                player2.SetScore(0);

                player1.SetTurn(true);
                player2.SetTurn(false);

                UpdateLabels();
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

            UpdateLabels();

            //for (int i = 0; i < grid.cards.Count; i++)
            //{
                //grid.cards[i].SetBackImage(new BitmapImage(new Uri(cardsElement.ChildNodes.Item(i).ChildNodes.Item(0).InnerText, UriKind.Relative)));
                //grid.cards[i].SetFrontImage(new BitmapImage(new Uri(cardsElement.ChildNodes.Item(i).ChildNodes.Item(1).InnerText, UriKind.Relative)));
                //grid.cards[i].SetClicked(Convert.ToBoolean(cardsElement.ChildNodes.Item(i).ChildNodes.Item(2).InnerText));
                //grid.cards[i].SetVisibility(Convert.ToBoolean(cardsElement.ChildNodes.Item(i).ChildNodes.Item(3).InnerText));
            //}
        }

        private void SaveData()
        {
            // Create a new save file in the "Saves" directory.
            XmlWriter writer = XmlWriter.Create("Saves/memory.sav");
            writer.WriteStartElement("game");

            WriteTag(writer, "difficulty", difficulty);

            writer.WriteStartElement("cards");

            for (int i = 0; i < grid.cards.Count; i++)
            {
                writer.WriteStartElement("card");

                WriteTag(writer, "backImg", grid.cards[i].GetBackImage().ToString().Replace("pack://application:,,,/MemoryGame;component/", ""));
                WriteTag(writer, "frontImg", grid.cards[i].GetFrontImage().ToString().Replace("pack://application:,,,/MemoryGame;component/", ""));
                WriteTag(writer, "clicked", grid.cards[i].GetClicked().ToString());
                WriteTag(writer, "visibility", grid.cards[i].GetVisibility().ToString());

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

        private void UpdateLabels()
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
    }
}
