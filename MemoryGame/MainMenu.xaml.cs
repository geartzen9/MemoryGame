using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        Frame parentFrame;

        public MainMenu(Frame parentFrame)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
        }

        private void newGameButton_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new NewGameScreen(this.parentFrame));
        }

        private void continueButton_Click(object sender, RoutedEventArgs args)
        {
            if (!File.Exists("Saves/memory.sav"))
            {
                MessageBox.Show("Geen opslag bestand gevonden. Begin een nieuw spel.", "Doorgaan");
            } else
            {
                XmlDocument saveFile = new XmlDocument();
                saveFile.Load("Saves/memory.sav");

                XmlNode player1Element = saveFile.GetElementsByTagName("player").Item(0);
                XmlNode player2Element = saveFile.GetElementsByTagName("player").Item(1);
                XmlNode cardsElement = saveFile.GetElementsByTagName("cards").Item(0);

                if (player1Element == null || player2Element == null || cardsElement == null)
                {
                    MessageBox.Show("Kon het opslagbestand niet lezen.", "Doorgaan");
                } else
                {
                    this.parentFrame.Navigate(new GameScreen(this.parentFrame, saveFile, player1Element, player2Element, cardsElement));
                }
            }
        }

        private void highscoresButton_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new HighscoresScreen(this.parentFrame));
        }

        private void shutdownButton_Click(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        private void gameRulesButton_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new GameRules(this.parentFrame));
        }
    }
}
