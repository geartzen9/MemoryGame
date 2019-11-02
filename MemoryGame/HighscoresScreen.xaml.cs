using System;
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
        
        public HighscoresScreen(Frame parentFrame, XmlDocument saveFile, XmlNode highscoresElement)
        {
            InitializeComponent();

            this.parentFrame = parentFrame;

            generateList(saveFile, highscoresElement);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs args)
        {
            this.parentFrame.Navigate(new MainMenu(this.parentFrame));
        }

        private void generateList(XmlDocument saveFile, XmlNode highscoresElement)
        {
            for (int i = 0; i < 10; i++)
            {
                scoreGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 3; i++)
            {
                scoreGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < highscoresElement.ChildNodes.Count; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    TextBlock text = new TextBlock();
                    text.TextAlignment = TextAlignment.Center;
                    text.FontFamily = new FontFamily ("Tempus Sans ITC");
                    text.Foreground = new SolidColorBrush(Colors.White);
                    Border border = new Border()
                    {
                        BorderThickness = new Thickness()
                        {
                            Bottom = (row == 9) ? 2 : 1,
                            Top = (row == 0) ? 2 : 1,
                        },
                        BorderBrush = new SolidColorBrush(Colors.White)
                    };

                    switch (col)
                    {
                        // Rank
                        case 0:
                            text.Text = (row + 1).ToString();
                            break;
                        // Player name
                        case 1:
                            text.Text = highscoresElement.ChildNodes.Item(row).ChildNodes.Item(0).InnerText;
                            break;
                        // Score
                        case 2:
                            text.Text = highscoresElement.ChildNodes.Item(row).ChildNodes.Item(1).InnerText;
                            break;
                    }

                    Grid.SetColumn(text, col);
                    Grid.SetRow(text, row);
                    scoreGrid.Children.Add(text);

                    Grid.SetColumn(border, col);
                    Grid.SetRow(border, row);
                    scoreGrid.Children.Add(border);
                }
            }

        }
    }
}
