using System;
using System.Windows.Media;

namespace MemoryGame
{
    class Card
    {
        private ImageSource frontImg, backImg;
        private bool clicked, visibility;
        private int imgNumber;
        
        public Card(ImageSource frontImgSource, ImageSource backImgSource, int imgNbr)
        {
            Console.WriteLine(backImgSource);
            backImg = frontImgSource;
            frontImg = frontImgSource;
            imgNumber = imgNbr;
            clicked = false;
            visibility = true;
        }

        public bool getClicked()
        {
            return clicked;
        }

        public void ShowFront()
        {
            clicked = true;
        }

        public void ShowBack()
        {
            clicked = false;
        }

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

        public ImageSource GetFrontImage()
        {
            return frontImg;
        }

        public void SetFrontImage(ImageSource newFrontImg)
        {
            frontImg = newFrontImg;
        }

        public ImageSource GetBackImage()
        {
            return backImg;
        }

        public void SetBackImage(ImageSource newBackImg)
        {
            backImg = newBackImg;
        }

        public bool GetClicked()
        {
            return clicked;
        }

        public void SetClicked(bool newClicked)
        {
            clicked = newClicked;
        }

        public bool GetVisibility()
        {
            return visibility;
        }

        public void SetVisibility(bool newVisibility)
        {
            visibility = newVisibility;
        }

        public int GetImgNumber()
        {
            return imgNumber;
        }
    }
}
