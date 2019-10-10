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
            this.parentFrame.Content = new NewGameScreen();
        }

        private void continueButton_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Content = new GameScreen();
        }

        private void highscoresButton_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Content = new HighscoresScreen();
        }

        private void shutdownButton_Click(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }
    }
}
