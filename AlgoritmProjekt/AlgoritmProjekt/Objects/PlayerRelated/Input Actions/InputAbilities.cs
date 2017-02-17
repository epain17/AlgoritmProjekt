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
        Keys shotKey, powerKey, aimKey;
        bool aim, shot;

        public bool ShotsFired
        {
            get { return shot; }
            set { shot = value; }
        }

        public bool Aim
        {
            get { return aim; }
        }

        private bool isKeyDown(Keys key)
        {
            if (KeyMouseReader.keyState.IsKeyDown(key))
                return true;
            return false;
        }

        public InputAbilities(Keys shotKey, Keys powerKey, Keys aimKey)
        {
            this.shotKey = shotKey;
            this.powerKey = powerKey;
            this.aimKey = aimKey;
            shot = false;
            aim = false;
        }

        public void Update(PlayerStates playerStates, ref float shotInterval)
        {
            Shooting(shotKey, ref shotInterval);
            Powering(powerKey, playerStates);
            Aiming(aimKey);
        }

        private void Shooting(Keys shotKey, ref float shotInterval)
        {
            if (isKeyDown(shotKey))
            {
                if (shotInterval > 0.5f)
                {
                    shot = true;
                    shotInterval = 0;
                }
            }
        }

        private void Powering(Keys powerKey, PlayerStates playerStates)
        {
            if (isKeyDown(powerKey) && playerStates.status != PlayerStates.Status.Invulnerable)
                playerStates.status = PlayerStates.Status.Power;
            else if (playerStates.status != PlayerStates.Status.Invulnerable)
                playerStates.status = PlayerStates.Status.Normal;
        }

        private void Aiming(Keys aimKey)
        {
            if (isKeyDown(aimKey))
                aim = true;
            else
                aim = false;
        }
    }
}
