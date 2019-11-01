using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

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

            XmlDocument saveFile = new XmlDocument();
            saveFile.Load("Saves/scores.sav");
            var highscores = saveFile.GetElementsByTagName("highscores").Item(0);

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

                    //Placement
                    if (col == 0)
                        text.Text = (row + 1).ToString();

                    //Name
                    if (col == 1)
                        text.Text = highscores.ChildNodes.Item(row).ChildNodes.Item(0).InnerText;

                    //Score
                    if (col == 2)
                        text.Text = highscores.ChildNodes.Item(row).ChildNodes.Item(1).InnerText; ;

                    Grid.SetColumn(text, col);
                    Grid.SetRow(text, row);
                    scoreGrid.Children.Add(text);
                }
            }

        }
    }
}

//TODO: HighScores Read from save
