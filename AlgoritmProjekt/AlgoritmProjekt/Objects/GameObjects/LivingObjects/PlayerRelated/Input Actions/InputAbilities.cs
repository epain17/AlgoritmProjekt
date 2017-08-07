using AlgoritmProjekt.Input;
using AlgoritmProjekt.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.PlayerRelated.Actions
{
    class InputAbilities
    {
        public delegate void Action();
        Action action;
        Keys executeKey;

        public InputAbilities(Keys executeKey, Action action)
        {
            this.executeKey = executeKey;
            this.action = action;
        }

        public void Execute(PlayerStates playerStates)
        {
            if (KeyMouseReader.keyState.IsKeyDown(executeKey) && playerStates.CurrentStatus == PlayerStates.Status.Normal)
                action();
        }
    }
}
