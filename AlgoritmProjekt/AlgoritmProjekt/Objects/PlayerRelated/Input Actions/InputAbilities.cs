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
        Keys powerKey;

        private bool isKeyDown(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
                return true;
            return false;
        }

        public InputAbilities(Keys powerKey)
        {
            this.powerKey = powerKey;
        }

        public void Update(PlayerStates playerStates)
        {
            Powering(powerKey, playerStates);
        }

        private void Powering(Keys powerKey, PlayerStates playerStates)
        {
            if (isKeyDown(powerKey) && playerStates.status != PlayerStates.Status.Invulnerable)
                playerStates.status = PlayerStates.Status.Power;
            else if (playerStates.status != PlayerStates.Status.Invulnerable)
                playerStates.status = PlayerStates.Status.Normal;
        }
    }
}
