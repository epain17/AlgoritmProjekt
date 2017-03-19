using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Objects.Companion.FuSMStates;
using AlgoritmProjekt.Objects.Enemies;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.PlayerRelated.Actions;
using AlgoritmProjekt.Objects.Projectiles;
using AlgoritmProjekt.Objects.Weapons;
using AlgoritmProjekt.Utility.AI.DecisionTree;
using AlgoritmProjekt.Utility.Handle_Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Companion
{
    class AICompanion : MovingTile
    {
        List<Projectile> projectiles = new List<Projectile>();
        public WeaponStates weaponStates;

        Vector2 circulatingPos;
        float angle;

        float shotInterval = 0;
        float startSpeed;

        FuSMDefault defaultState;
        FuSMApproach approachState;
        FuSMAttack attackState;
        FuSMEvade evadeState;

        public List<Projectile> Projectiles
        {
            get { return projectiles; }
            set { projectiles = value; }
        }

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
            weaponStates = new WeaponStates();
        }

        public override void Update(ref float time)
        {
            weaponStates.Update(time, ref shotInterval);
            UpdateProjectiles(time);

            shotInterval += time;
            direction.Normalize();
            position += time * speed * direction;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            foreach (Projectile proj in projectiles)
            {
                proj.Draw(spritebatch);
            }
            spritebatch.Draw(myTexture, position, null, Color.Red, (float)Math.PI * 0.25f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Blue, (float)Math.PI * 0.45f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Green, (float)Math.PI * 0.6f, origin, 0.5f, SpriteEffects.None, 1);
        }

        public void Shoot(Vector2 target)
        {
            Random rand = new Random();
            switch (weaponStates.type)
            {
                case WeaponStates.WeaponType.Pistol:
                    projectiles.Add(new FireBullet(myTexture, position, size, new Vector2(target.X + rand.Next(-10, 10), target.Y + rand.Next(-10, 10)), 170, 6));
                    break;
                case WeaponStates.WeaponType.MachineGun:
                    projectiles.Add(new FireBullet(myTexture, position, size, new Vector2(target.X + rand.Next(-3, 3), target.Y + rand.Next(-3, 3)), 180, 7));
                    break;
                case WeaponStates.WeaponType.ShotGun:
                    for (int i = 0; i < 4; i++)
                        projectiles.Add(new FireBullet(myTexture, position, size, new Vector2(target.X + rand.Next(-20, 20), target.Y + rand.Next(-20, 20)), 150, 5));
                    break;
            }
        }

        private void UpdateProjectiles(float time)
        {
            if (projectiles.Count > 0)
            {
                for (int i = projectiles.Count - 1; i >= 0; --i)
                {
                    if (!projectiles[i].iamAlive)
                        projectiles.RemoveAt(i);
                }
                foreach (Projectile proj in projectiles)
                {
                    proj.Update(ref time);
                }
            }
        }

        public void PowerUp()
        {
            weaponStates.UpgradeWeapon();
        }

        public void AccumulateDirection(Vector2 offset)
        {
            direction += offset;
        }

        public void Perception(float time, Player player, List<PatrolEnemy> testEnemies)
        {
            Vector2 nearestEnemyToCompanion = new Vector2();
            Vector2 nearestEnemyToPlayer = new Vector2();

            foreach (Enemy enemy in testEnemies)
            {
                if (nearestEnemyToPlayer == Vector2.Zero)
                    nearestEnemyToPlayer = enemy.FutureWaypoint();
                else if (Vector2.Distance(player.myPosition, enemy.myPosition) < Vector2.Distance(player.myPosition, nearestEnemyToPlayer))
                    nearestEnemyToPlayer = enemy.FutureWaypoint();
                if (nearestEnemyToCompanion == Vector2.Zero)
                    nearestEnemyToCompanion = enemy.myPosition;
                else if (Vector2.Distance(myPosition, enemy.myPosition) < Vector2.Distance(myPosition, nearestEnemyToPlayer))
                    nearestEnemyToCompanion = enemy.myPosition;
            }

            if (nearestEnemyToPlayer != Vector2.Zero)
            {
                if (KeyMouseReader.keyState.IsKeyDown(Keys.Space))
                {
                    attackState.Execute(nearestEnemyToPlayer);
                    approachState.DistanceFromTargetToPlayer = Vector2.Distance(player.myPosition, nearestEnemyToPlayer);
                    approachState.Execute(nearestEnemyToPlayer);
                }
            }

            if (nearestEnemyToCompanion != Vector2.Zero)
            {
                evadeState.Execute(nearestEnemyToCompanion);
            }
        }

        public void Perception(float time, Player player, List<Enemy> testEnemies)
        {
            Vector2 nearestEnemyToCompanion = new Vector2();
            Vector2 nearestEnemyToPlayer = new Vector2();

            foreach (Enemy enemy in testEnemies)
            {
                if (nearestEnemyToPlayer == Vector2.Zero)
                    nearestEnemyToPlayer = enemy.FutureWaypoint();
                else if (Vector2.Distance(player.myPosition, enemy.myPosition) < Vector2.Distance(player.myPosition, nearestEnemyToPlayer))
                    nearestEnemyToPlayer = enemy.FutureWaypoint();
                if (nearestEnemyToCompanion == Vector2.Zero)
                    nearestEnemyToCompanion = enemy.myPosition;
                else if (Vector2.Distance(myPosition, enemy.myPosition) < Vector2.Distance(myPosition, nearestEnemyToPlayer))
                    nearestEnemyToCompanion = enemy.myPosition;
            }

            if (nearestEnemyToPlayer != Vector2.Zero)
            {
                if (KeyMouseReader.keyState.IsKeyDown(Keys.Space))
                {
                    attackState.Execute(nearestEnemyToPlayer);
                    approachState.DistanceFromTargetToPlayer = Vector2.Distance(player.myPosition, nearestEnemyToPlayer);
                    approachState.Execute(nearestEnemyToPlayer);
                }
            }

            if (nearestEnemyToCompanion != Vector2.Zero)
            {
                evadeState.Execute(nearestEnemyToCompanion);
            }
        }

        public void Perception(float time, Player player, Level level)
        {
            Vector2 nearestEnemyToCompanion = new Vector2();
            Vector2 nearestEnemyToPlayer = new Vector2();
            Vector2 nearestItemToPlayer = new Vector2();

            // Iterate various world objects to find ideal targets
            foreach (Item item in level.items)
            {
                if (nearestItemToPlayer != Vector2.Zero)
                    nearestItemToPlayer = item.myPosition;
                else if (Vector2.Distance(player.myPosition, item.myPosition) < Vector2.Distance(player.myPosition, nearestItemToPlayer))
                    nearestItemToPlayer = item.myPosition;
            }

            foreach (Enemy enemy in level.enemies)
            {
                if (nearestEnemyToPlayer == Vector2.Zero)
                    nearestEnemyToPlayer = enemy.FutureWaypoint();
                else if (Vector2.Distance(player.myPosition, enemy.myPosition) < Vector2.Distance(player.myPosition, nearestEnemyToPlayer))
                    nearestEnemyToPlayer = enemy.FutureWaypoint();
                if (nearestEnemyToCompanion == Vector2.Zero)
                    nearestEnemyToCompanion = enemy.myPosition;
                else if (Vector2.Distance(myPosition, enemy.myPosition) < Vector2.Distance(myPosition, nearestEnemyToPlayer))
                    nearestEnemyToCompanion = enemy.myPosition;
            }

            foreach (EnemySpawner spawner in level.spawners)
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
            angle += time * 1.5f;
            circulatingPos = player.myPosition;
            circulatingPos.X += (size * 2) * (float)Math.Cos(angle);
            circulatingPos.Y += (size * 2) * (float)Math.Sin(angle);
            defaultState.Execute(circulatingPos);

            if (nearestEnemyToPlayer != Vector2.Zero)
            {
                if (KeyMouseReader.keyState.IsKeyDown(Keys.Space))
                {
                    attackState.Execute(nearestEnemyToPlayer);
                    approachState.DistanceFromTargetToPlayer = Vector2.Distance(player.myPosition, nearestEnemyToPlayer);
                    approachState.Execute(nearestEnemyToPlayer);
                }
            }

            if (nearestEnemyToCompanion != Vector2.Zero)
            {
                evadeState.Execute(nearestEnemyToCompanion);
            }

            if(nearestItemToPlayer != Vector2.Zero)
            {
                speed = startSpeed;
                approachState.DistanceFromTargetToPlayer = Vector2.Distance(player.myPosition, nearestItemToPlayer);
                approachState.Execute(nearestItemToPlayer);
            }
        }
    }
}
