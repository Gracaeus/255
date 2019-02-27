using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePatternExample
{
    class MoveToBedState: IState
    {

        public virtual void Update(GameAgent agent, GameTime gameTime)
        {
            agent.MoveToBed();

            // when at water then change state
            if (agent.AtBed)
            {
                agent.ChangeState(new SleepState());
            }
        }

        public virtual void OnEnter(GameAgent agent)
        {
            Console.WriteLine("MoveToBedState OnEnter() called \n");
        }

        public virtual void OnExit(GameAgent agent)
        {
            Console.WriteLine("MoveToBedSate OnExit() called \n");

            // agent no longer at food
            agent.AtBed = false;
        }
    }
}
