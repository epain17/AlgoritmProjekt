using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Input;
using AlgoritmProjekt.Objects.Companion.FuSMStates;
using AlgoritmProjekt.Objects.Enemies;
using AlgoritmProjekt.Objects.PlayerRelated;
using AlgoritmProjekt.Objects.PlayerRelated.Actions;
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
    class AICompanion
    {
        Vector2 circulatingPos;
        public Vector2 m_vTarget;
        float angle;

        float startSpeed, bulletSpeed, fireRate;
        int bulletRange, bulletAmount;

        FuSMDefault defaultState;
        FuSMApproach approachState;
        FuSMAttack attackState;
        FuSMEvade evadeState;

        public float StartSpeed
        {
            get { return startSpeed; }
        }

        //public AICompanion(Vector2 position, int size)
        //    : base(position, size)
        //{
        //    this.myPosition = new Vector2(position.X, position.Y - size);
        //    this.mySize = size;
        //    this.startSpeed = 150;
        //    speed = startSpeed;
        //    defaultState = new FuSMDefault(this);
        //    approachState = new FuSMApproach(this);
        //    evadeState = new FuSMEvade(this);
        //    attackState = new FuSMAttack(this);
        //    myShoot = new IShoot(this);
        //    bulletSpeed = 160;
        //    bulletRange = 3;
        //    fireRate = 0.7f;
        //    bulletAmount = 1;
        //}

        //public void Draw(SpriteBatch spritebatch)
        //{
        //    myShoot.Draw(spritebatch);
        //    spritebatch.Draw(myTexture, myPosition, null, Color.Red, (float)Math.PI * 0.25f, origin, 0.5f, SpriteEffects.None, 1);
        //    spritebatch.Draw(myTexture, myPosition, null, Color.Blue, (float)Math.PI * 0.45f, origin, 0.5f, SpriteEffects.None, 1);
        //    spritebatch.Draw(myTexture, myPosition, null, Color.Green, (float)Math.PI * 0.6f, origin, 0.5f, SpriteEffects.None, 1);
        //}

        //public void AccumulateDirection(Vector2 offset)
        //{
        //    direction += offset;
        //}

        //public void Perception(float time, Player player, Level level)
        //{
        //    Vector2 nearestEnemyToCompanion = new Vector2();
        //    Vector2 nearestEnemyToPlayer = new Vector2();
        //    Vector2 nearestItemToPlayer = new Vector2();

        //    // Iterate various world objects to find ideal targets
        //    foreach (Item item in level.items)
        //    {
        //        if (nearestItemToPlayer != Vector2.Zero)
        //            nearestItemToPlayer = item.myPosition;
        //        else if (Vector2.Distance(player.myPosition, item.myPosition) < Vector2.Distance(player.myPosition, nearestItemToPlayer))
        //            nearestItemToPlayer = item.myPosition;
        //    }

        //    foreach (Enemy enemy in level.enemies)
        //    {
        //        if (nearestEnemyToPlayer == Vector2.Zero)
        //            nearestEnemyToPlayer = enemy.FutureWaypoint();
        //        else if (Vector2.Distance(player.myPosition, enemy.myPosition) < Vector2.Distance(player.myPosition, nearestEnemyToPlayer))
        //            nearestEnemyToPlayer = enemy.FutureWaypoint();
        //        if (nearestEnemyToCompanion == Vector2.Zero)
        //            nearestEnemyToCompanion = enemy.myPosition;
        //        else if (Vector2.Distance(myPosition, enemy.myPosition) < Vector2.Distance(myPosition, nearestEnemyToPlayer))
        //            nearestEnemyToCompanion = enemy.myPosition;
        //    }

        //    foreach (EnemySpawner spawner in level.spawners)
        //    {
        //        if (nearestEnemyToPlayer == Vector2.Zero)
        //            nearestEnemyToPlayer = spawner.myPosition;
        //        else if (Vector2.Distance(player.myPosition, spawner.myPosition) < Vector2.Distance(player.myPosition, nearestEnemyToPlayer))
        //            nearestEnemyToPlayer = spawner.myPosition;
        //        if (nearestEnemyToCompanion == Vector2.Zero)
        //            nearestEnemyToCompanion = spawner.myPosition;
        //        else if (Vector2.Distance(myPosition, spawner.myPosition) < Vector2.Distance(myPosition, nearestEnemyToPlayer))
        //            nearestEnemyToCompanion = spawner.myPosition;
        //    }

        //    foreach (PatrolEnemy patroller in level.patrollers)
        //    {
        //        if (nearestEnemyToPlayer == Vector2.Zero)
        //            nearestEnemyToPlayer = patroller.myPosition;
        //        else if (Vector2.Distance(player.myPosition, patroller.myPosition) < Vector2.Distance(player.myPosition, nearestEnemyToPlayer))
        //            nearestEnemyToPlayer = patroller.myPosition;
        //        if (nearestEnemyToCompanion == Vector2.Zero)
        //            nearestEnemyToCompanion = patroller.myPosition;
        //        else if (Vector2.Distance(myPosition, patroller.myPosition) < Vector2.Distance(myPosition, nearestEnemyToPlayer))
        //            nearestEnemyToCompanion = patroller.myPosition;
        //    }

        //    foreach (DTEnemy shooter in level.shooters)
        //    {
        //        if (nearestEnemyToPlayer == Vector2.Zero)
        //            nearestEnemyToPlayer = shooter.myPosition;
        //        else if (Vector2.Distance(player.myPosition, shooter.myPosition) < Vector2.Distance(player.myPosition, nearestEnemyToPlayer))
        //            nearestEnemyToPlayer = shooter.myPosition;
        //        if (nearestEnemyToCompanion == Vector2.Zero)
        //            nearestEnemyToCompanion = shooter.myPosition;
        //        else if (Vector2.Distance(myPosition, shooter.myPosition) < Vector2.Distance(myPosition, nearestEnemyToPlayer))
        //            nearestEnemyToCompanion = shooter.myPosition;
        //    }

        //    //Give States New Perceptional Values
        //    angle += time * 1.5f;
        //    circulatingPos = player.myPosition;
        //    circulatingPos.X += (mySize * 2) * (float)Math.Cos(angle);
        //    circulatingPos.Y += (mySize * 2) * (float)Math.Sin(angle);
        //    defaultState.Execute(circulatingPos);

        //    if (nearestEnemyToPlayer != Vector2.Zero)
        //    {
        //        if (KeyMouseReader.keyState.IsKeyDown(Keys.Space))
        //        {
        //            attackState.Execute(nearestEnemyToPlayer);
        //            //approachState.DistanceFromTargetToPlayer = Vector2.Distance(player.myPosition, nearestEnemyToPlayer);
        //            //approachState.Execute(nearestEnemyToPlayer);
        //        }
        //    }

        //    if (nearestEnemyToCompanion != Vector2.Zero)
        //    {
        //        evadeState.Execute(nearestEnemyToCompanion);
        //    }

        //    if(nearestItemToPlayer != Vector2.Zero)
        //    {
        //        speed = startSpeed;
        //        approachState.DistanceFromTargetToPlayer = Vector2.Distance(player.myPosition, nearestItemToPlayer);
        //        approachState.Execute(nearestItemToPlayer);
        //    }

        //    myShoot.Update(time, m_vTarget, bulletSpeed, fireRate, bulletRange, bulletAmount);

        //    direction.Normalize();
        //    myPosition += time * speed * direction;
        //}
    }
}
