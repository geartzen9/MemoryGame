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
    /// Interaction logic for GameRules.xaml
    /// </summary>
    public partial class GameRules : Page
    {
        Frame parentFrame;
        public GameRules(Frame parentFrame)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;
        }

        private void returntoMenu_Button(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new MainMenu(this.parentFrame));
        }
    }
}
