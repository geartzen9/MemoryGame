using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryGame
{
    class MemoryGrid
    {
        private Grid grid;
        private int cols;
        private int rows;
        private int theme_nbr;

        private List<Card> cards = new List<Card>();
        private int clickedCardAmount = 0;
        private int previousCardIndex;

        /// <summary>
        ///     Constructor to give the xaml grid rows and columns so the cards can be added
        /// </summary>
        /// <param name="grid">The xaml grid name</param>
        /// <param name="cols">The amount of cols</param>
        /// <param name="rows">The amount of rows</param>
        /// <param name="theme">The number of the chosen theme</param>
        public MemoryGrid(Grid grid, int cols, int rows, int theme)
        {
            this.grid = grid;
            this.cols = cols;
            this.rows = rows;
            this.theme_nbr = theme;
            InitializeGrid(cols, rows);
            AddImages();
            ShowCards();
        }

        public void ChangeCards(int selector, ImageSource backImg, ImageSource frontImg, bool clicked, bool visibility, int imgNr)
        {
            //int imgNr = selector % ((cols * rows) / 2) + 1;

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
        ///     Add the cards/images to the grid
        /// </summary>
        private void AddImages()
        {
            for (int i = 0; i < rows * cols; i++)
            {
                int imgNr = i % ((cols * rows) / 2) + 1;
                ImageSource frontImg = new BitmapImage(new Uri("Res/themes/theme_" + theme_nbr + "/" + imgNr + ".png", UriKind.Relative));
                ImageSource backImg = new BitmapImage(new Uri("Res/themes/theme_" + theme_nbr + "/back.png", UriKind.Relative));
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
            if (clickedCardAmount < 2)
            {
                Image card = (Image)sender;
                int index = (int)card.Tag;
                card.Source = null;
                cards[index].ShowFront();
                clickedCardAmount++;

                ShowCards();
                //TODO: add some delay here to be able to see which card you picked

                //TODO: make sure you can't press the same img twice to win

                //TODO: make pointcount

                if (clickedCardAmount == 2)
                {
                    if (cards[previousCardIndex].GetImgNumber() == cards[index].GetImgNumber())
                    {
                        cards[index].SetVisibility(false);
                        cards[previousCardIndex].SetVisibility(false);

                        MessageBox.Show("Goed: " + cards[previousCardIndex].GetImgNumber() + " - " + cards[index].GetImgNumber());
                    }
                    else
                    {
                        cards[index].ShowBack();
                        cards[previousCardIndex].ShowBack();


                        MessageBox.Show("Fout: " + cards[previousCardIndex].GetImgNumber() + " - " + cards[index].GetImgNumber());
                    }

                    clickedCardAmount = 0;
                }
                else
                {
                    previousCardIndex = index;
                }
            }

            ShowCards();
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
