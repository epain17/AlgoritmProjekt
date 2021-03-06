﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers.ParticleEngine
{
    class EnemyEmitter : Emitter
    {

        public EnemyEmitter(Vector2 position) 
            : base(position)
        {
            this.position = position;
            nrParticles = 4;
            myLifeTime = 3;
            direction = Vector2.Zero;
        }

        public override void Update(ref float time)
        {
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override Particle GenerateParticle()
        {
            Vector2 velocity = new Vector2(random.Next(-5, 5), random.Next(-5, 5));
            int lifeTime = 50 + random.Next(50);
            float size = 16;
            return new TextureParticle(position, velocity, lifeTime, size);
        }
    }
}
