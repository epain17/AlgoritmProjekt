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
        public delegate bool Requirement();
        Requirement require;

        public InputAbilities(Action action, Requirement require)
        {
            this.action = action;
            this.require = require;
        }

        public bool Execute(PlayerStates playerStates)
        {
            if (require())
            {
                action();
                return true;
            }
            return false;
        }
    }
}
