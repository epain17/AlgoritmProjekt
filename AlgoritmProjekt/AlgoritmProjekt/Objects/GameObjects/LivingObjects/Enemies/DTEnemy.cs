using AlgoritmProjekt.Characters;
using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Utility.AI.DecisionTree.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI.DecisionTree
{
    class DTEnemy
    {
        float safetyRange, attackRange;
        Player player;
        DTree tree;
        Point initialPoint;


        public DTState CurrentState;

        public Point myStartPoint
        {
            get { return initialPoint; }
        }

        //public DTEnemy(Vector2 position, int size, Player player)
        //    : base(position, size)
        //{
        //    //this.myTexture = texture;
        //    this.myPosition = position;
        //    this.mySize = size;
        //    this.player = player;
        //    this.initialPathPoint = new Point((int)position.X / size, (int)position.Y / size);
        //    initialPoint = initialPathPoint;
        //    hp = 15;
        //    startHp = hp;
        //    speed = 80;
        //    aggroRange = size * 8;
        //    attackRange = aggroRange * 0.5f;
        //    safetyRange = size * 10;

        //    CurrentState = new DTState(this);
        //    tree = new DTree(this);
        //}

        //public void Update(float time, Player player, TileGrid grid)
        //{
        //    tree.Execute();
        //    CurrentState.UpdatePerception(player, grid, time);
        //    myShoot.Update(time, player.myPosition, 180, 1, aggroRange, 1);
        //    base.Update(ref time);
        //}

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    myShoot.Draw(spriteBatch);
        //    foreach (Vector2 way in waypoints)
        //        spriteBatch.Draw(myTexture, way, null, Color.White, 0, origin, 0.5f, SpriteEffects.None, 0);
        //    Color color = new Color(0.25f / HealthPercent(), 1 * HealthPercent(), 1f * HealthPercent());
        //    spriteBatch.Draw(myTexture, myPosition, null, color, 0, origin, 1, SpriteEffects.None, 1);
        //    spriteBatch.Draw(myTexture, HealthBar(), null, Color.ForestGreen, 0, origin, SpriteEffects.None, 1);
        //}

        //public void UpdateColor()
        //{

        //}

        //public bool ChooseOffensiveStance()
        //{
        //    if (Vector2.Distance(player.myPosition, myPosition) < (attackRange))
        //        return true;
        //    return false;
        //}

        //public bool ChooseDefensiveStance()
        //{
        //    if (hp < 10 && Vector2.Distance(player.myPosition, myPosition) < safetyRange && myPoint != initialPoint)
        //    {
        //        speed = 120;
        //        return true;
        //    }
        //    return false;
        //}

        //public bool ChooseCombatStance()
        //{
        //    return hp > (startHp / 2) ? true : false;
        //}
    }
}

