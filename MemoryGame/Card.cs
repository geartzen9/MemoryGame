using System.Windows.Media;

namespace MemoryGame
{
    class Card
    {
        private ImageSource frontImg, backImg;
        private bool clicked, visibillity;
        public int imgNr;
        public Card(ImageSource frontImgSource, ImageSource backImgSource, int imgNbr)
        {
            backImg = backImgSource;
            frontImg = frontImgSource;
            imgNr = imgNbr;
            clicked = false;
            visibillity = true;
        }

        public void ShowFront()
        {
            clicked = true;
        }

        public void ShowBack()
        {
            clicked = false;
        }

        public void MakeInvisible()
        {
            visibillity = false;
        }

        public ImageSource Show()
        {
            if(visibillity == true)
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

    }
}
