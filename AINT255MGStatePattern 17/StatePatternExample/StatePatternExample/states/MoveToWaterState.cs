using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace StatePatternExample
{
    class MoveToWaterState : IState
    {

        public virtual void Update(GameAgent agent, GameTime gameTime)
        {
            agent.MoveToWater();

            // when at water then change state
            if (agent.AtWater)
            {
                agent.ChangeState(new DrinkState());

            }
        }

        public virtual void OnEnter(GameAgent agent)
        {
            Console.WriteLine("MoveToWaterState OnEnter() called \n");
        }

        public virtual void OnExit(GameAgent agent)
        {
            Console.WriteLine("MoveToWaterState OnExit() called \n");

            // agent no longer at food
            agent.AtWater = false;
        }
    }
}
