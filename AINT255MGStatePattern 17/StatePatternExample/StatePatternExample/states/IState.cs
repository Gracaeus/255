using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace StatePatternExample
{
    interface IState
    {

        void Update(GameAgent agent, GameTime gameTime);

        void OnEnter(GameAgent agent);

        void OnExit(GameAgent agent);
    }
}
