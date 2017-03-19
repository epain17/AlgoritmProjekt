using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Objects.Projectiles;
using AlgoritmProjekt.Utility.AI.DecisionTree.States;
using AlgoritmProjekt.Utility.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.DecisionTree
{
    class DTEnemy : Enemy
    {
        float safetyRange, attackRange;
        Texture2D texBullet;
        Player player;
        DTree tree;
        IShoot myShoot;
        Point initialPoint;

        public DTState CurrentState;

        public Point myStartPoint
        {
            get { return initialPoint; }
        }

        public DTEnemy(Texture2D texture, Vector2 position, int size, Texture2D texBullet, Player player)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.size = size;
            this.texBullet = texBullet;
            this.player = player;
            this.initialPathPoint = new Point((int)position.X / size, (int)position.Y / size);
            initialPoint = initialPathPoint;
            hp = 15;
            startHp = hp;
            speed = 80;
            aggroRange = size * 8;
            attackRange = aggroRange * 0.5f;
            safetyRange = size * 10;

            CurrentState = new DTState(this);
            tree = new DTree(this);
            myShoot = new IShoot(this, texture);
        }

        public void Update(float time, Player player, TileGrid grid)
        {
            tree.Execute();
            CurrentState.UpdatePerception(player, grid, time);
            myShoot.Update(time, player.myPosition);
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            myShoot.Draw(spriteBatch);
            foreach (Vector2 way in waypoints)
                spriteBatch.Draw(myTexture, way, null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 0);
            Color color = new Color(0.25f / HealthPercent(), 1 * HealthPercent(), 1f * HealthPercent());
            spriteBatch.Draw(myTexture, position, null, color, 0, origin, 1, SpriteEffects.None, 1);
            spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);
        }

        public bool ChooseOffensiveStance()
        {
            if (Vector2.Distance(player.myPosition, position) < (attackRange))
                return true;
            return false;
        }

        public bool ChooseDefensiveStance()
        {
            if (hp < 10 && Vector2.Distance(player.myPosition, position) < safetyRange && myPoint != initialPoint)
            {
                speed = 120;
                return true;
            }
            return false;
        }

        public bool ChooseCombatStance()
        {
            return hp > (startHp / 2) ? true : false;
        }
    }
}

