﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryGame
{
    class MemoryGrid
    {
        private Grid grid;
        private int cols, rows, theme_nbr;

        private List<Card> cards = new List<Card>();
        private int clickedCardAmount = 0;
        private int previousCardIndex;

        private Player player1 = new Player("Thijs", 0, true);
        private Player player2 = new Player("Gerrit", 0, false);

        private int bonusPoints = 0, comboTurns = 1;

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
        private void ShowCards()
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

            if(index != previousCardIndex)
            {
                card.Source = null;
                cards[index].ShowFront();
                clickedCardAmount++;

                ShowCards();
                

                if (clickedCardAmount == 2)
                {
                    if (cards[previousCardIndex].imgNr == cards[index].imgNr)
                    {
                        cards[index].MakeInvisible();
                        cards[previousCardIndex].MakeInvisible();

                        if (comboTurns % 3 == 0)
                            bonusPoints += 10;

                        if (player1.GetTurn() == true)
                        {
                            player1.SetScore(player1.GetScore() + 10 + bonusPoints);
                        }
                        else
                        {
                            player2.SetScore(player2.GetScore() + 10 + bonusPoints);
                        }

                        comboTurns++;

                        //TODO: add some delay here to be able to see which card you picked
                        Thread.Sleep(1000);

                        /*MessageBox.Show("Goed: " + cards[previousCardIndex].imgNr + " - " + cards[index].imgNr);*/
                        MessageBox.Show("Goed: puntenaantal 1 = " + player1.GetScore() + " puntenaantal 2 = " + player2.GetScore());
                    }
                    else
                    {
                        cards[index].ShowBack();
                        cards[previousCardIndex].ShowBack();

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

                        comboTurns = 1;
                        bonusPoints = 0;

                        /*MessageBox.Show("Fout: " + cards[previousCardIndex].imgNr + " - " + cards[index].imgNr);*/
                        MessageBox.Show("Fout: puntenaantal 1 = " + player1.GetScore() + " puntenaantal 2 = " + player2.GetScore());
                    }

                    clickedCardAmount = 0;
                }
                else
                {
                    previousCardIndex = index;
                }
            }

            //TODO: Make this sh*t work
            /*GameScreen.SetScoreBoard();*/
            ShowCards();

        }
    }
}
