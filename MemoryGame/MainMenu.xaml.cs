using System.IO;
using System.Windows;
using System.Windows.Controls;

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
                this.parentFrame.Navigate(new GameScreen(this.parentFrame));
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
