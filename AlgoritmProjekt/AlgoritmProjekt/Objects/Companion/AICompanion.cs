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

        public bool TimeToShoot
        {
            get { return shoot; }
            set { shoot = value; }
        }

        public AICompanion(Texture2D texture, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.size = size;
            this.speed = 50;
        }

        public override void Update(ref float time)
        {
            shotInterval += time;
            base.Update(ref time);
        }

        public void ApproachTarget(MovingTile target)
        {
            rand = new Random();
            if (Vector2.Distance(position, target.myPosition) > size * 3)
            {
                speed = target.speed * 1.1f;
                SetDirection(new Vector2(target.myPosition.X + rand.Next(-size, size), target.myPosition.Y + rand.Next(-size, size)));
            }
            else if (Vector2.Distance(position, target.myPosition) < size * 2)
                speed = target.speed * 0.25f;
        }

        public bool AttackTarget(MovingTile target)
        {
            if (Vector2.Distance(position, target.myPosition) < size * 6 && shotInterval > 0.5f)
            {
                shotInterval = 0;
                return true;
            }
            return false;
        }

        public void PickUpObject(Tile target)
        {

        }

        public void EvadeTarget(Tile target)
        {

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(myTexture, position, null, Color.Red, (float)Math.PI * 0.25f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Blue, (float)Math.PI * 0.45f, origin, 0.5f, SpriteEffects.None, 1);
            spritebatch.Draw(myTexture, position, null, Color.Green, (float)Math.PI * 0.6f, origin, 0.5f, SpriteEffects.None, 1);
        }
    }
}
