using AlgoritmProjekt.Objects.Weapons;
using AlgoritmProjekt.ParticleEngine.Emitters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Environment
{
    class Goal : ObjectTile
    {
        GoalEmitter topLeftEmitter, bottomRightEmitter;
        bool active = false, used = false;
        float timer;
        bool flipTimer;

        public bool IsActive
        {
            get { return active; }
            set { active = value; }
        }

        public Goal(Texture2D texture, Texture2D neon, Vector2 position, int size)
            : base(texture, position, size)
        {
            this.myTexture = texture;
            this.position = position;
            this.size = size;
            this.occupied = false;
            timer = 1;
            texColor = new Color(0.25f, 0.25f, 0.1f);
            topLeftEmitter = new GoalEmitter(neon, new Vector2(position.X - (size / 2), position.Y - (size / 2)), position, size, 0);
            bottomRightEmitter = new GoalEmitter(neon, new Vector2(position.X + (size / 2), position.Y + (size / 2)), position, size, 2);
        }

        public override void Update(ref float time)
        {
            Flashing(time);
            if (active)
            {
                topLeftEmitter.Update(ref time);
                bottomRightEmitter.Update(ref time);
            }
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (active)
            {
                ActivateMe();
                topLeftEmitter.Draw(spritebatch);
                bottomRightEmitter.Draw(spritebatch);
            }
            spritebatch.Draw(myTexture, position, null, texColor * timer, 0, origin, 1, SpriteEffects.None, 1);
        }

        private void Flashing(float time)
        {
            if (active)
            {
                if (flipTimer)
                    timer += time;
                else
                    timer -= time;
                if (timer > 1.5f)
                    flipTimer = false;
                else if (timer < 0.75f)
                    flipTimer = true;
            }
        }

        void ActivateMe()
        {
            if (!used)
            {
                used = true;
                texColor = new Color(0.75f, 0.75f, 0.4f);
            }
        }
    }
}
