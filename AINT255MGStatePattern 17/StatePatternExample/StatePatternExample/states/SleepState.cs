using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePatternExample
{
    class SleepState : IState
    {
        public virtual void Update(GameAgent agent, GameTime gameTime)
        {
            agent.Sleep(gameTime);

            // when satiated then change state
            if (agent.FinishedSleeping)
            {
                if (agent.HasEaten == true)
                {
                    agent.ChangeState(new MoveToWaterState());
                }
                if (agent.HasDrank==true)
                {
                    agent.ChangeState(new MoveToFoodState());
                }
            }
        }

        public virtual void OnEnter(GameAgent agent)
        {
            Console.WriteLine("\nSleepState OnEnter() called: Agent Sleeping Hard \n");
            agent.Colour = Color.Green;
        }

        public virtual void OnExit(GameAgent agent)
        {
            Console.WriteLine("\nSleepState OnExit() called: Agent gets out of bed \n");

            agent.FinishedSleeping = false;
            agent.Colour = Color.Blue;
        }
    }
}
