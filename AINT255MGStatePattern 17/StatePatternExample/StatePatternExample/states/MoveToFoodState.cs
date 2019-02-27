using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace StatePatternExample
{
    class MoveToFoodState : IState
    {

        public virtual void Update(GameAgent agent, GameTime gameTime)
        {
            agent.MoveToFood();

            // when at food then change state
            if (agent.AtFood)
            {
                agent.ChangeState(new EatState());
            }
        }

        public virtual void OnEnter(GameAgent agent)
        {
            Console.WriteLine("MoveToFoodState OnEnter() called \n");
        }

        public virtual void OnExit(GameAgent agent)
        {
            Console.WriteLine("MoveToFoodState OnExit() called \n");

            // agent no longer at food
            agent.AtFood = false;
        }
    }
}
