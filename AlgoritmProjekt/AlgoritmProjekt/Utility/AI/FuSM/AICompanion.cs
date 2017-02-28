using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Objects.Companion.FuSMStates;
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
        float shotInterval = 0;
        float startSpeed;

        FuSMDefault defaultState;
        FuSMApproach approachState;
        FuSMEvade evadeState;
        FuSMAttack attackState;

        public float StartSpeed
        {
            get { return startSpeed; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public bool TimeToShoot()
        {
            if (shotInterval > 0.2f)
            {
                shotInterval = 0;
                return true;
            }
            return false;
        }

        public AICompanion(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = new Vector2(position.X, position.Y - size);
            this.size = size;
            this.startSpeed = 150;
            speed = startSpeed;
            defaultState = new FuSMDefault(this);
            approachState = new FuSMApproach(this);
            evadeState = new FuSMEvade(this);
            attackState = new FuSMAttack(this);
        }

        public override void Update(ref float time)
        {
            shotInterval += time;
            direction.Normalize();
            position += time * speed * direction;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(myTexture, position, null, Color.Red, (float)Math.PI * 0.25f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Blue, (float)Math.PI * 0.45f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Green, (float)Math.PI * 0.6f, origin, 0.5f, SpriteEffects.None, 1);
        }

        public void AccumulateDirection(Vector2 offset)
        {
            direction += offset;
        }

        public void Perception(float time, Player player, List<Item> items, List<Enemy> enemies, List<EnemySpawner> spawners)
        {
            Vector2 nearestEnemyToCompanion = new Vector2();
            Vector2 nearestEnemyToPlayer = new Vector2();
            Vector2 nearestItemToPlayer = new Vector2();

            // Iterate various world objects to find ideal targets
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

            //Give States New Perceptional Values
            defaultState.Execute(player.myPosition, player);

            if (nearestEnemyToPlayer != Vector2.Zero)
            {
                attackState.Execute(nearestEnemyToPlayer, player);
                approachState.Execute(nearestEnemyToPlayer, player);
            }

            if (nearestEnemyToCompanion != Vector2.Zero)
            {
                evadeState.Execute(nearestEnemyToCompanion, player);
            }

            if(nearestItemToPlayer != Vector2.Zero)
            {
                speed = startSpeed;
                approachState.Execute(nearestItemToPlayer, player);
            }

            //Console.WriteLine("Speed: " + Speed);
        }
    }
}
