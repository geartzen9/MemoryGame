using System.Windows;

namespace MemoryGame
{
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Initialize a new window and set the main menu as the content of the frame.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            parentFrame.Content = new MainMenu(parentFrame);
        }
    }
}
