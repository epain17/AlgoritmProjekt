using AlgoritmProjekt.Characters;
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
            Attack,
            UseItem,
            Power,
        }
        public Status CurrentStatus = Status.Normal;

        bool fade = true;
        float invulnerableTimer;
        float speedMultiplier;
        float attackTimer;

        Player player;

        public PlayerStates(Player player)
        {
            this.player = player;
            speedMultiplier = 1.5f;
            invulnerableTimer = 0;
            attackTimer = 0;
        }

        private void AttackCoolDown(float time)
        {
            attackTimer += time;

            if(attackTimer > player.attackInterval)
            {
                CurrentStatus = Status.Normal;
                attackTimer = 0;
            }
        }

        private void InvulnerableState(float time)
        {
            invulnerableTimer += time;
            if (player.mySpeed < (player.myStartSpeed * speedMultiplier))
                player.mySpeed *= speedMultiplier;

            player.colorAlpha = ChangeColor(player.colorAlpha);

            if (invulnerableTimer > player.invulnerableTimeLimit)
            {
                CurrentStatus = Status.Normal;
                invulnerableTimer = 0;
                player.colorAlpha = 1;
                fade = true;
            }
        }

        private float ChangeColor(float colorAlpha)
        {
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

            return colorAlpha;
        }

        public void HandlePlayerStates(float time)
        {
            switch (CurrentStatus)
            {
                case Status.Normal:
                    if (player.mySpeed != player.myStartSpeed)
                        player.mySpeed = player.myStartSpeed;
                    if (player.energy < player.maxEnergy)
                        player.energy += 0.1f;
                    break;
                case Status.Invulnerable:
                    InvulnerableState(time);
                    break;
                case Status.Attack:
                    AttackCoolDown(time);
                    break;
                case Status.UseItem:

                    break;
                case Status.Power:

                    break;
            }
        }
    }
}
