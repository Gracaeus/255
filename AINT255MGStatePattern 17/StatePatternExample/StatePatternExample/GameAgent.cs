using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Threading;

namespace StatePatternExample
{
    class GameAgent : GameObject
    {
        /// <summary>
        /// State of agent
        /// </summary>
        private IState currentState;

        /// <summary>
        /// List of objects in the agents world
        /// </summary>
        private List<GameObject> gameObjects;

        /// <summary>
        /// The object to which this agent is currently 
        /// focussued on
        /// </summary>
        private GameObject targetObject;
        
        /// <summary>
        /// Set of booleans to to indicate
        /// which state the agent is in
        /// </summary>
        public bool AtFood { set;  get; }
        public bool AtWater { set; get; }
        public bool AtBed { set; get; }
        public bool FinishedEating { set; get; }
        public bool FinishedDrinking { set; get; }
        public bool FinishedSleeping { set; get; }
        public bool HasEaten { set; get; }
        public bool HasDrank { set; get; }




        public GameAgent(Rectangle pos, Rectangle b, Color bColor) : base(pos, bColor)
        {
            viewportbounds = new Rectangle(b.X, b.Y, b.Width, b.Height);

            random = new Random();

            // assume the agent starts life hungry
            currentState = new MoveToFoodState();
        }



        public void MoveToFood()
        {
            // gameObjects[0] is the Food
            targetObject = gameObjects[0];

            Arrive(targetObject);

            if (Vector2.Distance(position, targetObject.Position) < 5)
            {
                AtFood = true;
            }
        }

        public void MoveToWater()
        {
            // gameObjects[1] is the water
            targetObject = gameObjects[1];

            Arrive(targetObject);

            // if within some close distance agent has arrived
            if (Vector2.Distance(position, targetObject.Position) < 5)
            {
                AtWater = true;
            }
        }
        public void MoveToBed()
        {
            targetObject = gameObjects[2];

            Arrive(targetObject);

            if(Vector2.Distance(position, targetObject.Position) <5)
            {
                AtBed = true;
            }
        }

        public void Eat(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            // only display a message for start  of action
            if (timeElapsed <= 0.2)
            {
                Console.WriteLine(" Agent Eating ");
            }

            if (timeElapsed > 2)
            {
                FinishedEating = true;
                HasEaten = true;
                HasDrank = false;
                timeElapsed = 0;
            }
        }

        public void Drink(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            // only display a message for start  of action
            if (timeElapsed <= 0.2)
            {
                Console.WriteLine(" Agent Drinking  ");
            }

            if (timeElapsed > 2)
            {

                FinishedDrinking = true;
                HasDrank = true;
                HasEaten = false;
                timeElapsed = 0;
            }
        }

        public void Sleep(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed <=0.2)
            {
                Console.WriteLine("Agent Sleeping");
            }
            if (timeElapsed > 2)
            {
                FinishedSleeping = true;
                timeElapsed = 0;
            }
        }

        public void ChangeState(IState newState)
        {
            currentState.OnExit(this);

            currentState = newState;

            currentState.OnEnter(this);
        }

        public void ResetTimeElapsed()
        {
            timeElapsed = 0;

        }
        public void Update(GameTime gameTime)
        {

            currentState.Update(this, gameTime);
        
        }



        #region  steering  code =================================================


        private double timeElapsed;
        private float orientation = 0f;          // the angle which the character is facing in radians
        private Vector2 heading;            // vector represenation of orientation
        private Vector2 wanderPosition;
        private float maxRotation = 0.2f;          // the maximium number of radians character 
                                                   // can turn in one time step i.e. the angular velocity

        private float slowRadius;           // radius for slowing down when arriving
        private Rectangle viewportbounds;   // To keep track of the obejcts bounds i.e. ViewPort dimensions
        private Random random;              // random number used for wandering


        /// <summary>
        /// Arrive is very similar to Seek, but we adjust speed as a function of close distance to target
        /// </summary>
        /// <param name="arrivee"></param>
        private void Arrive(GameObject arrivee)
        {
            float turnFactor = 0.75f;

            float distanceToArrivee;
            float arriveSpeed;

            // set some intial speeds and angualr rotations
         float   maxCharacterSpeed = 2.0f;

         float   maxRotation = 0.20f;
            // ===================================

            slowRadius = 20;

            Vector2 steering = arrivee.Position - Position;

            orientation = TurnToFace(steering, orientation, turnFactor * maxRotation);

            heading = OrientationAsVector(orientation);

            // find the distance to the arrivee
            distanceToArrivee = steering.Length();

            // determine the speed to move
            arriveSpeed = maxCharacterSpeed * distanceToArrivee / slowRadius;

            // clamp arriveSpeed to max speed
            if (arriveSpeed > maxCharacterSpeed)
                arriveSpeed = maxCharacterSpeed;

            // finally update the characterposition
            position += heading * arriveSpeed;
        }


        /// <summary>
        /// Retruns the Vector representation of the orientation
        /// passed as parameter. Orientation is measured in radians
        /// </summary>
        /// <param name="orien"></param>
        /// <returns></returns>
        private Vector2 OrientationAsVector(float orien)
        {
            Vector2 orienAsVect;

            orienAsVect.X = (float)Math.Cos(orien);
            orienAsVect.Y = (float)Math.Sin(orien);

            return orienAsVect;
        }


        public void Wander()
        {
            // The wander effect is accomplished by having the character aim in a random
            // direction. Every frame, this random direction is slightly modified.

            // the max +/- the agent will wander from its current position
            float wanderLimits = 0.5f;

            // this defines what proportion of its maxRotation speed the agent will turn
            float turnFactor = 0.15f;

            // randomly define a new position
            wanderPosition.X +=  MathHelper.Lerp(-wanderLimits, wanderLimits, (float)random.NextDouble());
            wanderPosition.Y +=  MathHelper.Lerp(-wanderLimits, wanderLimits, (float)random.NextDouble());

            // normalize the wander position, ...
            if (wanderPosition != Vector2.Zero)
                wanderPosition.Normalize();

            // now find the new orientation based on the wanderPosition
            orientation = TurnToFace(wanderPosition, orientation, turnFactor * maxRotation);

            // determine the heading vector based on orientation
            heading = OrientationAsVector(orientation);

            // finally update the agents position based upon the new heading and its speed
            // assume a wandering agent only moves at 0.5 of maxSpeed
            position += heading * 0.5f * 0;

            WrapForViewport();
        }


        private float TurnToFace(Vector2 steering, float currentOrientation, float turnSpeed)
        {
            float newOrientation;
            float desiredOrientation;
            float orientationDifference;

            float x = steering.X;
            float y = steering.Y;

            // the desiredOrientation is given by the steering vector
            desiredOrientation = (float)Math.Atan2(y, x);

            // find the difference between the orientation we need to be
            // and our current Orientation
            orientationDifference = desiredOrientation - currentOrientation;

            // now using WrapAngle to get result from -Pi to Pi 
            // ( -180 degrees to 180 degrees )
            orientationDifference = WrapAngle(orientationDifference);

            // clamp that between -turnSpeed and turnSpeed.
            orientationDifference = MathHelper.Clamp(orientationDifference, -turnSpeed, turnSpeed);

            // the closest we can get to our target is currentAngle + orientationDifference.
            // return that, using WrapAngle again.
            newOrientation = WrapAngle(currentOrientation + orientationDifference);

            return newOrientation;
        }

        private void WrapForViewport()
        {
            if (position.X < 0)
                position.X = viewportbounds.Width;

            else if (position.X > viewportbounds.Width)
                position.X = 0;

            if (position.Y < 0)
                position.Y = viewportbounds.Height;

            else if (Position.Y > viewportbounds.Height)
                position.Y = 0;
        }

        /// <summary>
        /// Returns the angle expressed in radians between -Pi and Pi.
        /// <param name="theta">the angle to wrap, in radians.</param>
        /// <returns>the input value expressed in radians from -Pi to Pi.</returns>
        /// from MS XNA Chase and Evade sample
        /// </summary>
        /// 
        private float WrapAngle(float theta)
        {
            while (theta < -MathHelper.Pi)
            {
                theta += MathHelper.TwoPi;
            }
            while (theta > MathHelper.Pi)
            {
                theta -= MathHelper.TwoPi;
            }
            return theta;
        }

        /// Draws this object (and its related images) to the given graphics 
        /// context (SpriteBatch).
        public override void Draw(SpriteBatch spriteBatch)
        {
            characterRectanglePosition.X = (int)position.X;
            characterRectanglePosition.Y = (int)position.Y;

            // as we are rotaing an agent, we are specify an orientation of the agent. we need to also
            // specify the coordinates around which we are rotating the image.
            // In this instance we need to rotate around the centre of the image, which is simply 
            // the width/2 and the height/2
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

            spriteBatch.Draw(characterImage, characterRectanglePosition, null, Colour, orientation, drawingOrigin, SpriteEffects.None, 0.0f);
        }

        public List<GameObject> GameObjects
        {

            set
            {
                gameObjects = value;

            }

        }

    } // end GameAgent

        #endregion
}
