using System.Windows.Media;

namespace MemoryGame
{
    class Card
    {
        // The ImageSource of the front image of this card.
        private ImageSource frontImg;

        // The ImageSource of the back image of this card.
        private ImageSource backImg;
        
        // The variable that checks if the card is already clicked.
        private bool clicked;
        
        // The variable that checks if the card is already guessed.
        private bool visibility;

        // The image number of the current theme to show on the front of the card.
        private int imgNumber;
        
        /// <summary>
        ///     Initialize a new card object.
        /// </summary>
        /// <param name="frontImgSource">The front image of the card.</param>
        /// <param name="backImgSource">The back image of the card.</param>
        /// <param name="imgNbr">The number of the image in the current theme.</param>
        public Card(ImageSource frontImgSource, ImageSource backImgSource, int imgNbr)
        {
            backImg = backImgSource;
            frontImg = frontImgSource;
            imgNumber = imgNbr;
            clicked = false;
            visibility = true;
        }

        /// <summary>
        ///     Get the clicked variable.
        /// </summary>
        /// <returns>The clicked variable</returns>
        public bool getClicked()
        {
            return clicked;
        }

        /// <summary>
        ///     Show the card on the grid.
        /// </summary>
        /// <returns>
        ///     If the visibility variable is true and if the clicked variable is true, then this function returns the front image of this card.
        ///     If the visibility variable is true and if the clicked variable is false, then this function returns the back image of this card.
        ///     If the visibility variable is false, then this function returns null.
        /// </returns>
        public ImageSource Show()
        {
            if(visibility == true)
            {
                if(clicked == true)
                {
                    return frontImg;
                }
                else
                {
                    return backImg;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///     Get the front image of this card.
        /// </summary>
        /// <returns>The front image of this card.</returns>
        public ImageSource GetFrontImage()
        {
            return frontImg;
        }

        /// <summary>
        ///     Set the front image of this card.
        /// </summary>
        /// <param name="newFrontImg">The ImageSource to change the front image into.</param>
        public void SetFrontImage(ImageSource newFrontImg)
        {
            frontImg = newFrontImg;
        }

        /// <summary>
        ///     Get the back image of this card.
        /// </summary>
        /// <returns>The back image of this card.</returns>
        public ImageSource GetBackImage()
        {
            return backImg;
        }

        /// <summary>
        ///     Set the back image of this card.
        /// </summary>
        /// <param name="newBackImg">The ImageSource to change the back image into.</param>
        public void SetBackImage(ImageSource newBackImg)
        {
            backImg = newBackImg;
        }

        /// <summary>
        ///     Get the clicked variable.
        /// </summary>
        /// <returns>The clicked variable.</returns>
        public bool GetClicked()
        {
            return clicked;
        }

        /// <summary>
        ///     Set the clicked variable.
        /// </summary>
        /// <param name="newClicked">The boolean to change the clicked variable into.</param>
        public void SetClicked(bool newClicked)
        {
            clicked = newClicked;
        }

        /// <summary>
        ///     Get the visibility variable.
        /// </summary>
        /// <returns>The visibility variable.</returns>
        public bool GetVisibility()
        {
            return visibility;
        }

        /// <summary>
        ///     Set the visibility variable.
        /// </summary>
        /// <param name="newVisibility">The boolean to change the visibility variable into.</param>
        public void SetVisibility(bool newVisibility)
        {
            visibility = newVisibility;
        }

        /// <summary>
        ///     Get the image number of the current theme of this card.
        /// </summary>
        /// <returns>The image number of the current theme.</returns>
        public int GetImgNumber()
        {
            return imgNumber;
        }
    }
}
