﻿using AlgoritmProjekt.Managers.ParticleEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.ParticleEngine.Particles
{
    class Neon : TextureParticle
    {

        public Neon(Vector2 position, Vector2 velocity, float lifeTime, float size)
           :base(position, velocity, lifeTime, size)
        {
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
            this.size = size;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            color = new Color(Color.LightGoldenrodYellow, lifeTime / 2);
        }

        public override void Update(float time)
        {
            if (lifeTime >= startLife * 0.98f)
                color = Color.White;
            else
                color = new Color(0.75f * (lifeTime / startLife), 0.75f * (lifeTime / startLife), 0.4f * (lifeTime / startLife));

            base.Update(time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, origin, size * lifeTime, SpriteEffects.None, 0);
        }
    }
}
