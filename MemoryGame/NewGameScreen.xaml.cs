using MemoryGame.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for NewGameScreen.xaml
    /// </summary>
    public partial class NewGameScreen : Page
    {
        Frame parentFrame;

        public NewGameScreen(Frame parentFrame)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
        }

        private void startButton_Click(object sender , RoutedEventArgs e)
        {           
            if (string.IsNullOrEmpty(InputP1.Text) || string.IsNullOrEmpty(InputP2.Text))
            {
                MessageBox.Show("Vul beide namen in om te kunnen spelen");
                return;
            }
            
            string difficulty = Moeilijkheidsgraad.SelectedValue.ToString();            
            Player player1 = new Player(InputP1.Text, 0, true);
            Player player2 = new Player(InputP2.Text, 0, true);
            this.parentFrame.Navigate(new GameScreen(parentFrame, player1, player2, difficulty));
        }
       
            
            //Close Theme
        public void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        }
        //Open Theme
        public void ShowPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            //Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            //Make the first item selected.
            comboBox.SelectedIndex = 0;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the ComboBox.
            var comboBox = sender as ComboBox;

            // 
            int value = comboBox.SelectedIndex + 1;
            MessageBox.Show("" + value);
            Settings.Default["ThemeNumber"] = value;
            Settings.Default.Save();
            Console.WriteLine(Settings.Default["ThemeNumber"]);
        }
        //TODO: Logic for theme select
    }
}
