using System.Windows;

namespace MemoryGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            parentFrame.Content = new MainMenu(parentFrame);
        }
    }
}
