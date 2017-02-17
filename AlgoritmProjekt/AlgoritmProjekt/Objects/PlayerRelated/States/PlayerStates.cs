using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.PlayerRelated
{
    class PlayerStates
    {
        public enum Status
        {
            Normal,
            Invulnerable,
            Power,
        }
        public Status status;

        bool fade = true;
        float invulnerableTimer;
        int invulnerableTimeLimit;
        int maxEnergy;
        float startSpeed, speedMultiplier;

        public PlayerStates(Status playerstate, int maxEnergy, int invulnerableTimeLimit, float speed)
        {
            this.status = playerstate;
            this.maxEnergy = maxEnergy;
            this.invulnerableTimeLimit = invulnerableTimeLimit;
            this.startSpeed = speed;
            speedMultiplier = 1.5f;
            invulnerableTimer = 0;
        }

        public void Update(ref float time, ref float energyMeter, ref float colorAlpha, ref float speed)
        {
            HandlePlayerStates(ref time, ref energyMeter, ref colorAlpha, ref speed);
        }

        private void InvulnerableState(float time, ref float colorAlpha)
        {
            invulnerableTimer += time;

            if (fade)
            {
                colorAlpha -= 0.1f;
                if (colorAlpha < 0.1f)
                    fade = false;
            }
            else
            {
                colorAlpha += 0.1f;
                if (colorAlpha > 0.9f)
                    fade = true;
            }

            if (invulnerableTimer > invulnerableTimeLimit)
            {
                status = Status.Normal;
                invulnerableTimer = 0;
                colorAlpha = 1;
                fade = true;
            }
        }

        private void SlowTime(ref float time, ref float energyMeter, ref float speed)
        {
            if (energyMeter > 0)
            {
                energyMeter -= 0.15f;
                time *= 0.35f;
                if (speed < (startSpeed * speedMultiplier))
                    speed *= speedMultiplier;
            }
        }

        private void HandlePlayerStates(ref float time, ref float energyMeter, ref float colorAlpha, ref float speed)
        {
            switch (status)
            {
                case Status.Normal:
                    if (speed != startSpeed)
                        speed /= speedMultiplier;
                    if (energyMeter < maxEnergy)
                        energyMeter += 0.05f;
                    break;
                case Status.Invulnerable:
                    InvulnerableState(time, ref colorAlpha);
                    break;
                case Status.Power:
                    SlowTime(ref time, ref energyMeter, ref speed);
                    break;
            }
        }
    }
}
