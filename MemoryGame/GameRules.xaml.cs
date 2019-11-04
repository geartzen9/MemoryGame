using System.Windows;
using System.Windows.Controls;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for GameRules.xaml
    /// </summary>
    public partial class GameRules : Page
    {
        // The Frame to navigate between pages.
        Frame parentFrame;
        
        /// <summary>
        ///     Initialize the game rules page.
        /// </summary>
        /// <param name="parentFrame">The Frame to navigate between pages.</param>
        public GameRules(Frame parentFrame)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
        }

        /// <summary>
        ///     The click event to return to the main menu.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
        private void returntoMenu_Button(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new MainMenu(this.parentFrame));
        }
    }
}
