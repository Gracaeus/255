using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace StatePatternExample
{
    class DrinkState : IState
    {

        public virtual void Update(GameAgent agent, GameTime gameTime)
        {
            agent.Drink(gameTime);

            // when satiated then change state
            if (agent.FinishedDrinking)
            {
                agent.ChangeState(new MoveToBedState());
            }
        }

        public virtual void OnEnter(GameAgent agent)
        {
            Console.WriteLine("\nDrinkState OnEnter() called: Agent filling glass with water \n");
            agent.Colour = Color.Yellow;
        }

        public virtual void OnExit(GameAgent agent)
        {
            Console.WriteLine("\nDrinkState OnExit() called: Agent putting down glass \n");

            agent.FinishedDrinking = false;
            agent.Colour = Color.Blue;
        }
    }
}
