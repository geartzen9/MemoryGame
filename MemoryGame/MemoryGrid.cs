using MemoryGame.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
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
        // The grid on the game screen that needs to be filled.
        private Grid grid;

        // The list where the cards are being stored.
        private List<Card> cards = new List<Card>();

        // The game screen object.
        private GameScreen gameScreen;

        // The object of the first player.
        private Player player1;

        // The object of the first player.
        private Player player2;

        // The integer to define how much cols are needed in the grid.
        private int cols;

        // The integer to define how much rows are needed in the grid.
        private int rows;

        // The integer that holds the number of the current theme.
        private int theme_nbr;

        // The amount of pairs that is left on the grid.
        private int pairs;

        // The amount of bonuspoints the current player gets when he continues the streak.
        private int bonusPoints = 0;

        // The integer that holds how long the streak is going.
        private int streak = 1;

        // The integer that holds the index of the previously guessed card.
        private int previousCardIndex;

        // The integer that holds the amount of cards that is clicked per turn.
        private int clickedCardAmount = 0;

        /// <summary>
        ///     Constructor to give the XAML grid rows and columns so the cards can be added.
        /// </summary>
        /// <param name="grid">The XAML grid name.</param>
        /// <param name="cols">The amount of cols.</param>
        /// <param name="rows">The amount of rows.</param>
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

        /// <summary>
        ///     Change the cards to the cards that are in the save file.
        /// </summary>
        /// <param name="backImg">The ImageSource of the back image of this card.</param>
        /// <param name="frontImg">The ImageSource of the front image of this card.</param>
        /// <param name="clicked">The variable that checks if the card is already clicked.</param>
        /// <param name="visibility">The variable that checks if the card is already guessed.</param>
        /// <param name="imgNr">The image number of the current theme to show on the front of the card.</param>
        public void ChangeCards(ImageSource backImg, ImageSource frontImg, bool clicked, bool visibility, int imgNr)
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
        ///     Makes a list of cards.
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
        ///     Adds the cards to the grid.
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
        ///     Scrambles the order of the cards.
        /// </summary>
        /// <param name="cards">The list of cards.</param>
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
        ///     Changes the image of the card from the back to the front image when clicked on.
        /// </summary>
        /// <param name="sender">Object that is clicked on.</param>
        /// <param name="e">The event data.</param>
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            int index = (int)card.Tag;

            if (cards[index].getClicked() == true)
            {
                return;
            }

            card.Source = null;
            cards[index].SetClicked(true);
            clickedCardAmount++;

            ShowCards();

            // Check if the player clicked two cards.
            if (clickedCardAmount == 2)
            {
                // Check if the two cards are the same.
                if (cards[previousCardIndex].GetImgNumber() == cards[index].GetImgNumber())
                {
                    cards[index].SetVisibility(false);
                    cards[previousCardIndex].SetVisibility(false);

                    // Add bonuspoints if the player has a streak.
                    if (streak % 3 == 0)
                    {
                        bonusPoints += 10;
                    }

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

                    // Check if all the cards are turned.
                    if (pairs == 0)
                    {
                        Player winner = (player1.GetScore() > player2.GetScore()) ? player1 : player2;

                        string winText = winner.GetName() + " wins with: " + winner.GetScore() + " points!";
                        MessageBoxResult result = MessageBox.Show(winText, "Winner!", MessageBoxButton.OK);

                        if (result == MessageBoxResult.OK)
                        {
                            gameScreen.SaveHighscores(winner);

                            Frame parentFrame = gameScreen.GetParentFrame();

                            XmlDocument saveFile = new XmlDocument();
                            saveFile.Load("Saves/scores.sav");

                            var highscoresElement = saveFile.GetElementsByTagName("highscores").Item(0);

                            if (highscoresElement == null)
                            {
                                MessageBox.Show("Kon de score niet opslaan in de highscores.", "Highscores");
                            }
                            else
                            {
                                saveFile.Load("Saves/scores.sav");
                                highscoresElement = saveFile.GetElementsByTagName("highscores").Item(0);

                                parentFrame.Navigate(new HighscoresScreen(parentFrame, saveFile, highscoresElement));
                            }
                        }
                    }
                }
                else
                {
                    cards[index].SetClicked(false);
                    cards[previousCardIndex].SetClicked(false);
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

        /// <summary>
        ///     Get the list of cards.
        /// </summary>
        /// <returns>The list where the cards are being stored.</returns>
        public List<Card> GetCards()
        {
            return cards;
        }

        /// <summary>
        ///     Get the grid of the game screen.
        /// </summary>
        /// <returns>The grid on the game screen that needs to be filled.</returns>
        public Grid GetGrid()
        {
            return grid;
        }

        /// <summary>
        ///     Set the pairs.
        /// </summary>
        /// <param name="newPairs">The new value of pairs.</param>
        public void SetPairs(int newPairs)
        {
            pairs = newPairs;
        }
    }
}
