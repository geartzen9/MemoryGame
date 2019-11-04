using MemoryGame.Properties;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for NewGameScreen.xaml
    /// </summary>
    public partial class NewGameScreen : Page
    {
        // The Frame to navigate between pages.
        Frame parentFrame;

        /// <summary>
        ///     Initialize a new new game screen.
        /// </summary>
        /// <param name="parentFrame">The Frame to navigate between pages.</param>
        public NewGameScreen(Frame parentFrame)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
        }

        /// <summary>
        ///     The click event for the start button.
        ///     This checks if the inputs are not empty and starts the game.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="e">The event arguments.</param>
        private void startButton_Click(object sender , RoutedEventArgs e)
        {           
            if (string.IsNullOrEmpty(InputP1.Text) || string.IsNullOrEmpty(InputP2.Text))
            {
                MessageBox.Show("Vul beide namen in om te kunnen spelen");
                return;
            }

            string difficulty = Moeilijkheidsgraad.SelectedValue.ToString();
            int pairs;
            Player player1 = new Player(InputP1.Text, 0, true);
            Player player2 = new Player(InputP2.Text, 0, true);

            this.parentFrame.Navigate(new GameScreen(parentFrame, player1, player2, difficulty));
        }

        /// <summary>
        ///     The click event for the close popup button.
        ///     This closes the popup.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="e">The event arguments.</param>
        public void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }

        /// <summary>
        ///     The click event for the show popup button.
        ///     This opens the popup to select a theme.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="e">The event arguments.</param>
        public void ShowPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
        }

        /// <summary>
        ///     The load event for the dropdown in the popup.
        /// </summary>
        /// <param name="sender">The object that is being loaded.</param>
        /// <param name="e">The event arguments.</param>
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            //Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            //Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        /// <summary>
        ///     The selection changed event for the dropdown.
        ///     This saves the selected value to the settings file.
        /// </summary>
        /// <param name="sender">The object where the selection is being changed.</param>
        /// <param name="e">The event arguments.</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the ComboBox.
            var comboBox = sender as ComboBox;

            int value = comboBox.SelectedIndex + 1;
            Settings.Default["ThemeNumber"] = value;
            Settings.Default.Save();
        }

        /// <summary>
        ///     The click event for the back button.
        ///     This navigates to the main menu.
        /// </summary>
        /// <param name="sender">The object that is being clicked on.</param>
        /// <param name="args">The event arguments.</param>
        public void BackButtonClick(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new MainMenu(this.parentFrame));
        }
    }
}
