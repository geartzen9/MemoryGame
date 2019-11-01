using MemoryGame.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace MemoryGame
{
    class MemoryGrid
    {
        private Grid grid;
        private List<Card> cards = new List<Card>();
        private GameScreen gameScreen;
        private Player player1, player2;
        private int cols, rows, theme_nbr, pairs, bonusPoints = 0, streak = 1, previousCardIndex, clickedCardAmount = 0;

        /// <summary>
        ///     Constructor to give the xaml grid rows and columns so the cards can be added
        /// </summary>
        /// <param name="grid">The xaml grid name</param>
        /// <param name="cols">The amount of cols</param>
        /// <param name="rows">The amount of rows</param>
        public MemoryGrid(GameScreen gameScreen, Grid grid, Player player1, Player player2, int cols, int rows)
        {
            this.gameScreen = gameScreen;
            this.grid = grid;
            this.player1 = player1;
            this.player2 = player2;
            this.cols = cols;
            this.rows = rows;
            this.pairs = (rows * cols) / 2;
            this.theme_nbr = Convert.ToInt32(Settings.Default["ThemeNumber"]);

            InitializeGrid(cols, rows);
            AddImages();
            ShowCards();
            gameScreen.UpdateLabels(player1, player2);
        }

        public void ChangeCards(int selector, ImageSource backImg, ImageSource frontImg, bool clicked, bool visibility, int imgNr)
        {
            Card card = new Card(frontImg, backImg, imgNr);
            card.SetClicked(clicked);
            card.SetVisibility(visibility);

            cards.Add(card);
        }

        /// <summary>
        ///     Give the xaml grid the right amount of rows and columns 
        /// </summary>
        /// <param name="cols">The amount of columns</param>
        /// <param name="rows">The amount of rows</param>
        private void InitializeGrid(int cols, int rows)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int j = 0; j < cols; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        /// <summary>
        ///     Makes a list of cards
        /// </summary>
        private void AddImages()
        {
            for (int i = 0; i < rows * cols; i++)
            {
                int imgNr = i % ((cols * rows) / 2) + 1;
                ImageSource frontImg = new BitmapImage(new Uri("Res/themes/theme_" + theme_nbr + "/" + imgNr + ".png", UriKind.Relative));
                ImageSource backImg = new BitmapImage(new Uri("Res/themes/theme_" + theme_nbr + "/back.png", UriKind.Relative));
                /*ImageSource backImg = frontImg;*/
                cards.Add(new Card(frontImg, backImg, imgNr));
            }

            Scramble(cards);
        }

        /// <summary>
        ///     Adds the cards to the grid
        /// </summary>
        public void ShowCards()
        {
            grid.Children.Clear();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Image card = new Image();
                    card.MouseDown += new MouseButtonEventHandler(CardClick);
                    card.Source = cards[col * cols + row].Show();
                    card.Tag = col * cols + row;
                    Grid.SetColumn(card, col);
                    Grid.SetRow(card, row);
                    grid.Children.Add(card);
                }
            }
        }

        /// <summary>
        ///     Scrambles the order of the cards
        /// </summary>
        /// <param name="cards">The list of cards</param>
        private void Scramble(List<Card> cards)
        {
            Random rnd = new Random();
            for (int i = 0; i < cards.Count(); i++)
            {
                int rand = rnd.Next(0, cards.Count());
                Card tmp_card = cards[i];
                cards[i] = cards[rand];
                cards[rand] = tmp_card;
            }
        }

        /// <summary>
        ///     Changes the image of the card from the back to the front image when clicked on
        /// </summary>
        /// <param name="sender">Object that is clicked on</param>
        /// <param name="e">The event data</param>
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            int index = (int)card.Tag;

            if (cards[index].getClicked() == true)
            {
                return;
            }

            card.Source = null;
            cards[index].ShowFront();
            clickedCardAmount++;

            ShowCards();

            if (clickedCardAmount == 2)
            {
                if (cards[previousCardIndex].GetImgNumber() == cards[index].GetImgNumber())
                {
                    cards[index].SetVisibility(false);
                    cards[previousCardIndex].SetVisibility(false);

                    if (streak % 3 == 0)
                        bonusPoints += 10;

                    if (player1.GetTurn() == true)
                    {
                        player1.SetScore(player1.GetScore() + 10 + bonusPoints);
                    }
                    else
                    {
                        player2.SetScore(player2.GetScore() + 10 + bonusPoints);
                    }

                    streak++;
                    pairs--;
                    if (pairs == 0)
                    {
                        Player winner = (player1.GetScore() > player2.GetScore()) ? player1 : player2;

                        string winText = winner.GetName() + " wins with: " + winner.GetScore() + " points!";
                        MessageBoxResult result = MessageBox.Show(winText,"Winner!",MessageBoxButton.OK);
                        
                        if (result == MessageBoxResult.OK)
                        {
                            Frame parentFrame = gameScreen.GetParentFrame();
                            parentFrame.Navigate(new HighscoresScreen(parentFrame));

                            XmlDocument saveFile = new XmlDocument();
                            saveFile.Load("Saves/scores.sav");

                            //if (saveFile.GetElementsByTagName("highscores").Item(0).ChildNodes.Count != 10)
                            //{
                                gameScreen.SaveHighscores(winner);
                            //}
                        }
                    }
                }
                else
                {
                    cards[index].ShowBack();
                    cards[previousCardIndex].ShowBack();
                    bonusPoints = 0;
                    streak = 1;

                    if (player1.GetTurn() == true)
                    {
                        player1.SetTurn(false);
                        player2.SetTurn(true);
                    }
                    else
                    {
                        player1.SetTurn(true);
                        player2.SetTurn(false);
                    }
                }

                clickedCardAmount = 0;
                gameScreen.UpdateLabels(player1, player2);
            }
            else
            {
                previousCardIndex = index;
            }
        }

        public List<Card> GetCards()
        {
            return cards;
        }

        public Grid GetGrid()
        {
            return grid;
        }
    }
}
