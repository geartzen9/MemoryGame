using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        /// <summary>
        ///     Give the xaml grid the right amount of rows and columns 
        /// </summary>
        /// <param name="cols">The amount of columns</param>
        /// <param name="rows">The amount of rows</param>
        private void InitializeGrid(int cols, int rows)
        {
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
            List<ImageSource> images = GetImagesList();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Image cardBack = new Image
                    {
                        Source = new BitmapImage(new Uri("Res/themes/theme_" + theme_nbr + "/back.png", UriKind.Relative)),
                        Tag = images.First()
                    };
                    images.RemoveAt(0);
                    cardBack.MouseDown += new MouseButtonEventHandler(CardClick);
                    Grid.SetColumn(cardBack, col);
                    Grid.SetRow(cardBack, row);
                    grid.Children.Add(cardBack);
                }
            }
        }

        /// <summary>
        ///     Gets the source of the card images and randomizes the order
        /// </summary>
        /// <returns>List of image sources</returns>
        private List<ImageSource> GetImagesList()
        {
            List<ImageSource> images = new List<ImageSource>();
            for (int i = 0; i < cols * rows; i++)
            {
                int imageNr = i % ((cols * rows) / 2) + 1;
                ImageSource source = new BitmapImage(new Uri("Res/themes/theme_" + theme_nbr + "/" + imageNr + ".png", UriKind.Relative));
                images.Add(source);
            }

            Random rnd = new Random();
            for (int i = 0; i < images.Count(); i++)
            {
                int rand = rnd.Next(0, images.Count());
                ImageSource tmp_img = images[i];
                images[i] = images[rand];
                images[rand] = tmp_img;
            }
            return images;
        }

        /// <summary>
        ///     Changes the image of the card from the back to the front image when clicked on
        /// </summary>
        /// <param name="sender">Object that is clicked on</param>
        /// <param name="e">The event data</param>
        private void CardClick(object sender, MouseButtonEventArgs e)
        {
            Image card = (Image)sender;
            ImageSource front = (ImageSource)card.Tag;
            card.Source = front;
        }
    }
}
