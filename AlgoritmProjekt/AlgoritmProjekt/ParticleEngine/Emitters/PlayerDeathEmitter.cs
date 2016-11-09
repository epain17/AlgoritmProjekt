using AlgoritmProjekt.Managers.ParticleEngine;
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
        Texture2D texture;

        public PlayerDeathEmitter(Texture2D texture, Vector2 position) 
            : base(position)
         {
            this.texture = texture;
            this.position = position;
            nrParticles = 1;
            myLifeTime = 6;
            velocity = Vector2.Zero;
        }

    public override void Update(float time)
    {
        base.Update(time);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    protected override Particle GenerateParticle()
    {
        return new PlayerParticle(texture, position, velocity, 100, 32);
    }
}
}
