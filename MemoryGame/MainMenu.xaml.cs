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
        // The Frame to navigate between pages.
        Frame parentFrame;

        /// <summary>
        ///     Initialize a new main menu screen.
        /// </summary>
        /// <param name="parentFrame">The Frame to navigate between pages.</param>
        public MainMenu(Frame parentFrame)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
        }

        /// <summary>
        ///     The click event for the new game button.
        ///     This navigates to the new game screen.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
        private void newGameButton_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new NewGameScreen(this.parentFrame));
        }

        /// <summary>
        ///     The click event of the continue button.
        ///     This reads the save file and checks if it exists and it is valid.
        ///     If it is falid, then it navigates to the game screen.
        ///     And if it's not valid, it shows a messagebox.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
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

                // Check if the save file contains the right elements.
                if (player1Element == null || player2Element == null || cardsElement == null)
                {
                    MessageBox.Show("Kon het opslagbestand niet lezen.", "Doorgaan");
                } else
                {
                    this.parentFrame.Navigate(new GameScreen(this.parentFrame, saveFile, player1Element, player2Element, cardsElement));
                }
            }
        }

        /// <summary>
        ///     The click event for the highscores button.
        ///     This reads the save file and checks if it exists and it is valid.
        ///     If it is falid, then it navigates to the highscores screen.
        ///     And if it's not valid, it shows a messagebox.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
        private void highscoresButton_Click(object sender, RoutedEventArgs args)
        {
            if (!File.Exists("Saves/scores.sav"))
            {
                MessageBox.Show("Geen opslag bestand gevonden voor de scores.", "Highscores");
            }
            else
            {
                XmlDocument saveFile = new XmlDocument();
                saveFile.Load("Saves/scores.sav");

                var highscoresElement = saveFile.GetElementsByTagName("highscores").Item(0);

                // Check if the save file contains the right elements.
                if (highscoresElement == null)
                {
                    MessageBox.Show("Kon het opslagbestand niet lezen.", "Highscores");
                }
                else
                {
                    this.parentFrame.Navigate(new HighscoresScreen(this.parentFrame, saveFile, highscoresElement));
                }
            }
        }

        /// <summary>
        ///     The click event for the shutdown button.
        ///     This exits the application.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
        private void shutdownButton_Click(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        ///     The click event for the game rules button.
        ///     This navigates to the game rules screen.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
        private void gameRulesButton_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new GameRules(this.parentFrame));
        }
    }
}
