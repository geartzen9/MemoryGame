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
        string theme;
        public NewGameScreen()
        {
            InitializeComponent();
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

            int value = comboBox.SelectedIndex + 1;
            Settings.Default["ThemeNumber"] = value;
            Settings.Default.Save();
            Console.WriteLine(Settings.Default["ThemeNumber"]);
        }
    }
}
