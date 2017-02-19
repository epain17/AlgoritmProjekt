using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.PlayerRelated.Actions;
using AlgoritmProjekt.Objects.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Companion
{
    class AICompanion : MovingTile
    {
        Random rand;
        float shotInterval = 0;
        bool shoot;
        float burst, halter;

        public bool TimeToShoot
        {
            get { return shoot; }
            set { shoot = value; }
        }

        public AICompanion(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = new Vector2(position.X, position.Y - size);
            this.size = size;
            this.burst = 200;
            this.halter = 50;
            speed = burst;
            rand = new Random();
        }

        public override void Update(ref float time)
        {
            shotInterval += time;

            direction.Normalize();
            position += time * speed * direction;
        }

        public void AccumulateDirection(Vector2 offset)
        {
            direction += offset;
        }

        public void DefaultState(Tile target)
        {
            float minDistance = size * 1.5f;
            float maxDistance = size * 3;
            float activationLevel = 0;
            float test = (Vector2.Distance(target.myPosition, position) - maxDistance) / maxDistance;
            Vector2 deltaPos = new Vector2((target.myPosition.X - position.X) + rand.Next(-size, size), (target.myPosition.Y - position.Y) + rand.Next(-size, size)); ;
            activationLevel = 1 - test;

            if (Vector2.Distance(position, target.myPosition) < minDistance)
            {
                activationLevel = 0.0001f;
                speed = halter;
            }
            else
                speed = burst;
            if (Vector2.Distance(position, target.myPosition) > maxDistance)
            {
                activationLevel = (Vector2.Distance(target.myPosition, position) - maxDistance) / maxDistance;
            //Console.WriteLine("Default: " + activationLevel);
            }

            AccumulateDirection(deltaPos * activationLevel);
        }

        public void ApproachState(Tile target, Player player)
        {
            float activationLevel = 0;
            float approachRange = size * 7;
            Vector2 deltaPos = target.myPosition - position;

            activationLevel = 1 - (Vector2.Distance(target.myPosition, position) - approachRange) / approachRange;
            if (Vector2.Distance(player.myPosition, target.myPosition) > approachRange)
            {
                activationLevel = 0;
            }
            //Console.WriteLine("Approach: " + activationLevel);

            AccumulateDirection(deltaPos * activationLevel);
        }

        public void EvadeState(Tile target)
        {
            float evadeRange = size * 2;
            float activationLevel = 0;
            Vector2 deltaPos = target.myPosition - position;
            if (Vector2.Distance(position, target.myPosition) < evadeRange)
            {
                activationLevel = 1 - (Vector2.Distance(position, target.myPosition) - (evadeRange * 40)) / (evadeRange * 40);
            }

            //Console.WriteLine("Evade: " + activationLevel);

            AccumulateDirection(-deltaPos * activationLevel);
        }

        public void AttackState(float time, Player player, Tile target)
        {
            if (shotInterval > 0.5f && Vector2.Distance(target.myPosition, position) < size * 4)
            {
                shotInterval = 0;
                player.Shoot(position, target.myPosition);
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(myTexture, position, null, Color.Red, (float)Math.PI * 0.25f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Blue, (float)Math.PI * 0.45f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Green, (float)Math.PI * 0.6f, origin, 0.5f, SpriteEffects.None, 1);
        }
    }
}
