using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.PlayerRelated.Actions;
using AlgoritmProjekt.Objects.Projectiles;
using AlgoritmProjekt.Objects.Weapons;
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

        float evadeLevel, approachLevel, defaultLevel;

        bool leaveDefault = false;



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
            ResetActivation();
        }

        public void AccumulateDirection(Vector2 offset)
        {
            direction += offset;
        }

        public void DefaultState(Vector2 target)
        {
            float minDistance = size * 1.5f;
            float maxDistance = size * 100;
            float distance = Vector2.Distance(position, target);

            Vector2 deltaPos = new Vector2((target.X - position.X) + rand.Next(-size, size), (target.Y - position.Y) + rand.Next(-size, size)); ;
            defaultLevel = 1 - ((maxDistance - distance) / maxDistance);

            if (distance < minDistance && !leaveDefault)
            {
                defaultLevel = 0.0001f;
                speed = halter;
            }
            else
                speed = burst;

            BindActivationLevel(ref defaultLevel);

            //Console.WriteLine("Default: " + defaultLevel);
            AccumulateDirection(deltaPos * defaultLevel);
        }

        public void ApproachState(Vector2 target, Vector2 player)
        {
            float approachRange = size * 4;
            Vector2 deltaPos = target - position;
            float distanceToTarget = Vector2.Distance(position, target);
            float distanceFromTargetToPlayer = Vector2.Distance(player, target);

            if (target == null || distanceFromTargetToPlayer > size * 8)
            {
                approachLevel = 0;
            }
            else
            {
                approachLevel = 1 - ((approachRange - distanceToTarget) / approachRange);
                leaveDefault = true;
            }

            BindActivationLevel(ref approachLevel);

            Console.WriteLine("Approach: " + approachLevel);
            AccumulateDirection(deltaPos * approachLevel);
        }

        public void EvadeState(Vector2 target)
        {
            float evadeRange = size * 2;
            Vector2 deltaPos = target - position;
            float distance = Vector2.Distance(position, target);

            if (distance < evadeRange)
            {
                evadeLevel = 1 - ((evadeRange) - distance) / (evadeRange);
            }
            else
                evadeLevel = 0;

            Console.WriteLine("Evade: " + evadeLevel);
            AccumulateDirection(-deltaPos * evadeLevel);
        }

        public void AttackState(float time, Player player, Vector2 target)
        {
            if (shotInterval > 0.5f && Vector2.Distance(target, position) < size * 4)
            {
                shotInterval = 0;
                player.Shoot(position, target);
            }
        }

        void ResetActivation()
        {
            evadeLevel = 0;
            defaultLevel = 0;
            approachLevel = 0;
            leaveDefault = false;
        }

        void BindActivationLevel(ref float activationLevel)
        {
            if (activationLevel < 0)
                activationLevel = 0;
            else if (activationLevel > 1)
                activationLevel = 1;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(myTexture, position, null, Color.Red, (float)Math.PI * 0.25f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Blue, (float)Math.PI * 0.45f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Green, (float)Math.PI * 0.6f, origin, 0.5f, SpriteEffects.None, 1);
        }

        public void Perception(float time, Player player, List<Item> items, List<Enemy> enemies, List<EnemySpawner> spawners)
        {
            Vector2 nearestEnemyToCompanion = new Vector2();
            Vector2 nearestEnemyToPlayer = new Vector2();
            Vector2 nearestItemToPlayer = new Vector2();

            DefaultState(player.myPosition);

            foreach (Item item in items)
            {
                if (nearestItemToPlayer != Vector2.Zero)
                    nearestItemToPlayer = item.myPosition;
                else if (Vector2.Distance(player.myPosition, item.myPosition) < Vector2.Distance(player.myPosition, nearestItemToPlayer))
                    nearestItemToPlayer = item.myPosition;
            }

            foreach (Enemy enemy in enemies)
            {
                if (nearestEnemyToPlayer == Vector2.Zero)
                    nearestEnemyToPlayer = enemy.myPosition;
                else if (Vector2.Distance(player.myPosition, enemy.myPosition) < Vector2.Distance(player.myPosition, nearestEnemyToPlayer))
                    nearestEnemyToPlayer = enemy.myPosition;
                if (nearestEnemyToCompanion == Vector2.Zero)
                    nearestEnemyToCompanion = enemy.myPosition;
                else if (Vector2.Distance(myPosition, enemy.myPosition) < Vector2.Distance(myPosition, nearestEnemyToPlayer))
                    nearestEnemyToCompanion = enemy.myPosition;
            }

            foreach (EnemySpawner spawner in spawners)
            {
                if (nearestEnemyToPlayer == Vector2.Zero)
                    nearestEnemyToPlayer = spawner.myPosition;
                else if (Vector2.Distance(player.myPosition, spawner.myPosition) < Vector2.Distance(player.myPosition, nearestEnemyToPlayer))
                    nearestEnemyToPlayer = spawner.myPosition;
                if (nearestEnemyToCompanion == Vector2.Zero)
                    nearestEnemyToCompanion = spawner.myPosition;
                else if (Vector2.Distance(myPosition, spawner.myPosition) < Vector2.Distance(myPosition, nearestEnemyToPlayer))
                    nearestEnemyToCompanion = spawner.myPosition;
            }

            if (nearestEnemyToPlayer != Vector2.Zero)
            {
                AttackState(time, player, nearestEnemyToPlayer);
                ApproachState(nearestEnemyToPlayer, player.myPosition);
            }

            if (nearestEnemyToCompanion != Vector2.Zero)
            {
                EvadeState(nearestEnemyToCompanion);
            }

            if(nearestItemToPlayer != Vector2.Zero)
            {
                ApproachState(nearestItemToPlayer, player.myPosition);
            }
        }
    }
}
