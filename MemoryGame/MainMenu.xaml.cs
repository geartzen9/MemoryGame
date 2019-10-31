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
            this.parentFrame.Navigate(new GameScreen(this.parentFrame));
        }

        private void highscoresButton_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new HighscoresScreen());
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
