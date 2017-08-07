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
    class Door : Tile
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
            this.myPosition = position;
            this.myWidth = width;
            this.myHeight = height;
            this.iamOccupied = false;
            timer = 1;
            texColor = new Color(0.25f, 0.25f, 0.1f);
        }

        public override void Update(ref float time)
        {
            Flashing(time);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (active)
            {
                ActivateMe();
            }
            spritebatch.Draw(myTexture, myPosition, texColor * timer);
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
