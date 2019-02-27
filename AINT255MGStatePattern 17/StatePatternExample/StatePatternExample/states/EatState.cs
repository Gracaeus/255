using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace StatePatternExample
{
    class EatState : IState
    {
        

        public virtual void Update(GameAgent agent, GameTime gameTime)
        {
            Random rnd = new Random();
            int prob = rnd.Next(0, 100);
            Console.WriteLine(prob);
            agent.Eat(gameTime);

            // when satiated then change state
            if (agent.FinishedEating)
            {
                if (prob<=60)
                {
                    agent.ChangeState(new MoveToBedState());
                }
                else if (prob>60)
                {
                    agent.ChangeState(new MoveToWaterState());
                }
                

            }
        }

        public virtual void OnEnter(GameAgent agent)
        {
            Console.WriteLine("\nEatState OnEnter() called: Agent picking up knife and fork \n");
            agent.Colour = Color.Red;
        }

        public virtual void OnExit(GameAgent agent)
        {
            agent.FinishedEating = false;

            Console.WriteLine("\nEatState OnExit() called: Agent putting down knife and fork \n");
            agent.Colour = Color.Blue;

        }
    }
}
