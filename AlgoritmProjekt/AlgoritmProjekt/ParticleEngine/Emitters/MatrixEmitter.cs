using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Managers.ParticleEngine.Emitters
{
    class MatrixEmitter : Emitter
    {

        public MatrixEmitter(Vector2 position)
            : base(position)
        {
            this.timeLimit = 3;
            this.myLifeTime = 10;
            this.direction.Y = 4;
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
            int lifeTime = 250 + random.Next(50);
            return new FontParticle(position, Vector2.Zero, lifeTime, 16);
        }

        protected override void EmitParticles()
        {
            particles.Add(GenerateParticle());
        }
    }
}
