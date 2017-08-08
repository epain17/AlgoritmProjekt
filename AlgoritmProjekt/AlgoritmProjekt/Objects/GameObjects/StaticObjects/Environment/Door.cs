using AlgoritmProjekt.Grid;
using AlgoritmProjekt.Objects.GameObjects;
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
    class Door : GameObject
    {
        bool active = false, used = false;
        float timer;
        bool flipTimer;

        public bool IsActive
        {
            get { return active; }
            set { active = value; }
        }

        public Door(Vector2 position, int width, int height)
            : base(position, width, height)
        {
            timer = 1;
        }

        public override void Update(float time, TileGrid grid)
        {
            base.Update(time, grid);
            Flashing(time);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (active)
            {
                ActivateMe();
            }
            base.Draw(spritebatch);
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
                myColor = new Color(0.75f, 0.75f, 0.4f);
            }
        }
    }
}
