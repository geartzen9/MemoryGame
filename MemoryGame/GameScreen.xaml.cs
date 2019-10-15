using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        // Constructor to begin a new game.
        public GameScreen(Frame parentFrame, Player player1, Player player2, string difficulty)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
            this.player1 = player1;
            this.player2 = player2;
            this.difficulty = difficulty;
        }

        // Constructor to continue a previous game.
        public GameScreen(Frame parentFrame)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
            LoadData();
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

            this.player1 = new Player(player1Element.ChildNodes.Item(0).InnerText, Convert.ToInt32(player1Element.ChildNodes.Item(1).InnerText), Convert.ToBoolean(player1Element.ChildNodes.Item(2).InnerText));
            this.player2 = new Player(player2Element.ChildNodes.Item(0).InnerText, Convert.ToInt32(player2Element.ChildNodes.Item(1).InnerText), Convert.ToBoolean(player2Element.ChildNodes.Item(2).InnerText));

            player1NameLabel.Content = player1.GetName();
            player2NameLabel.Content = player2.GetName();

            player1ScoreLabel.Text = player1.GetScore().ToString();
            player2ScoreLabel.Text = player2.GetScore().ToString();

            if (player1.GetTurn() == true)
            {
                player1NameLabel.Foreground = Brushes.Green;
                player2NameLabel.Foreground = Brushes.Black;
            } else
            {
                player1NameLabel.Foreground = Brushes.Black;
                player2NameLabel.Foreground = Brushes.Green;
            }
        }

        private void SaveData()
        {
            // Create a new save file in the "Saves" directory.
            XmlWriter writer = XmlWriter.Create("Saves/memory.sav");
            writer.WriteStartElement("game");

            writer.WriteStartElement("difficulty");
            writer.WriteString(this.difficulty);
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

            writer.WriteStartElement("name");
            writer.WriteString(player.GetName());
            writer.WriteEndElement();

            writer.WriteStartElement("score");
            writer.WriteString(player.GetScore().ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("turn");
            writer.WriteString(player.GetTurn().ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}
