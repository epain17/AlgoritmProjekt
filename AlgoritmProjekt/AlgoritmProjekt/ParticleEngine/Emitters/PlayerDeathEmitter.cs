﻿using AlgoritmProjekt.Managers.ParticleEngine;
using AlgoritmProjekt.ParticleEngine.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.ParticleEngine.Emitters
{
    class PlayerDeathEmitter : Emitter
    {

        public PlayerDeathEmitter(Vector2 position)
            : base(position)
        {
            this.position = position;
            nrParticles = 1;
            myLifeTime = 6;
            direction = Vector2.Zero;
            particles.Add(GenerateParticle());

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
            return new PlayerParticle(position, direction, 100, 32);
        }
    }
}
