using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for HighscoresScreen.xaml
    /// </summary>
    public partial class HighscoresScreen : Page
    {
        Frame parentFrame;
        public HighscoresScreen(Frame parentFrame)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;

            generateList();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new MainMenu(this.parentFrame));
        }

        private void generateList()
        {
            for (int i = 0; i < 10; i++)
            {
                scoreGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 3; i++)
            {
                scoreGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    TextBlock text = new TextBlock();
                    text.TextAlignment = TextAlignment.Center;
                    text.FontFamily = new FontFamily ("Tempus Sans ITC");
                    text.Foreground = new SolidColorBrush(Colors.White);

                    if (col == 0)
                        text.Text = (row + 1).ToString();

                    if (col == 1)
                        text.Text = "Name";

                    if (col == 2)
                        text.Text = "Score";

                    Grid.SetColumn(text, col);
                    Grid.SetRow(text, row);
                    scoreGrid.Children.Add(text);
                }
            }

        }
    }
}

//TODO: HighScores Read from save
