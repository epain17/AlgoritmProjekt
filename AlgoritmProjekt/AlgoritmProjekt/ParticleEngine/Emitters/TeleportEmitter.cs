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
    class TeleportEmitter : Emitter
    {
        enum MoveEmitter
        {
            Right = 0,
            Down = 1,
            Left= 2,
            Up = 3,
        }
        MoveEmitter moveMe;


        int orbitingCircum;
        Vector2 teleportPos;
        Texture2D texture;
        float speed;

        public TeleportEmitter(Texture2D texture, Vector2 position, Vector2 pos, int size, int initializeMe)
            : base(position)
        {
            this.texture = texture;
            this.position = position;
            this.orbitingCircum = size;
            this.teleportPos = pos;
            moveMe = (MoveEmitter)initializeMe;
            nrParticles = 1;
            myLifeTime = 1;
            speed = 70;
        }

        public override void Update(ref float time)
        {
            RemoveParticles(time);
            timer++;
            if (timer > timeLimit)
            {
                timer = 0;
                EmitParticles();
            }
            velocity = orbitTeleporter(teleportPos);
            position += velocity * time;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private Vector2 orbitTeleporter(Vector2 pos)
        {
            switch (moveMe)
            {
                case MoveEmitter.Right:
                    velocity = new Vector2(speed, 0);
                    if (myPosition.X >= pos.X + (orbitingCircum / 2) && myPosition.Y <= pos.Y - (orbitingCircum / 2))
                        moveMe = MoveEmitter.Down;
                    break;
                case MoveEmitter.Down:
                    velocity = new Vector2(0, speed);

                    if (myPosition.X >= pos.X + (orbitingCircum / 2) && myPosition.Y >= pos.Y + (orbitingCircum / 2))
                        moveMe = MoveEmitter.Left;
                    break;
                case MoveEmitter.Left:
                    velocity = new Vector2(-speed, 0);
                    if (myPosition.X <= pos.X - (orbitingCircum / 2) && myPosition.Y >= pos.Y + (orbitingCircum / 2))
                        moveMe = MoveEmitter.Up;
                    break;
                case MoveEmitter.Up:
                    velocity = new Vector2(0, -speed);
                    if (myPosition.X <= pos.X - (orbitingCircum / 2) && myPosition.Y <= pos.Y - (orbitingCircum / 2))
                        moveMe = MoveEmitter.Right;
                    break;
            }

            return velocity;
        }

        protected override void EmitParticles()
        {
            for (int i = 0; i < nrParticles; i++)
            {
                particles.Add(GenerateParticle());
            }
        }

        protected override Particle GenerateParticle()
        {
            return new Neon(texture, position, Vector2.Zero, 40, 0.01f);
        }
    }
}
