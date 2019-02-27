using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace StatePatternExample
{
    class GameObject
    {

        protected Vector2 position;           // position used in updating


        // fields for viewing and drawing the character
        protected Texture2D characterImage;                   // image used to display object
        public Color Colour { set; get; }                      // drawing colour
        protected Rectangle characterRectanglePosition;       // Position and size of object
        // end


        public GameObject(Rectangle pos, Color bColor)
        {

            position = new Vector2(pos.X + pos.Width/ 2f, pos.Y + pos.Height/ 2f);

            characterRectanglePosition = new Rectangle(pos.X, pos.Y, pos.Width, pos.Height);

            Colour = bColor;
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            characterRectanglePosition.X = (int)position.X;
            characterRectanglePosition.Y = (int)position.Y;

            Vector2 drawingOrigin = new Vector2(characterImage.Width / 2, characterImage.Height / 2);


            // give the spritebatch these parameters:

            // characterImage               an image 
            // characterRectanglePosition   a position as a rectangle (and thus a size)
            // null                         this means draw the whole image
            // characterColour              a colour! 
            // orientation                  the orientaion which to draw the image
            // drawingOrigin                the origin around which the image is rotated
            // SpriteEffects                any special effects
            // 0.0f                         the layer depth, By default, 0 represents the front layer 

            spriteBatch.Draw(characterImage, characterRectanglePosition, null, Colour, 0, drawingOrigin, SpriteEffects.None, 0.0f);
        }

        public Vector2 Position
        {
            set
            {
                position = value;
            }

            get
            {

                return position;
            }

        }

        public Texture2D CharacterImage
        {
            set
            {
                characterImage = value;

            }
        }
    }
}
