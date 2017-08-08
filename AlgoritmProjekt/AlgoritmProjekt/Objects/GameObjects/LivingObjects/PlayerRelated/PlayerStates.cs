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
        float attackTimer;

        Player player;

        public PlayerStates(Player player)
        {
            this.player = player;
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

        private void InvulnerableCoolDown(float time)
        {
            invulnerableTimer += time;

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
                    player.OnIdle();
                    break;
                case Status.Invulnerable:
                    InvulnerableCoolDown(time);
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
