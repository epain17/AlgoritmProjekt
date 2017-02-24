﻿using AlgoritmProjekt.ParticleEngine.Emitters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Objects.Projectiles
{
    class FireBullet : Projectile
    {
        FireBulletEmitter emitter;

        public FireBullet(Texture2D texture, Vector2 position, int size, Vector2 targetVect)
            : base(texture, position, size, targetVect)
        {
            this.myTexture = texture;
            this.position = position;
            this.alive = true;
            this.size = size;
            this.startPos = position;
            tileRange = 8;
            speed = 125;
            SetDirection(targetVect);
            velocity = direction * speed;
            emitter = new FireBulletEmitter(texture, position, size, speed, targetVect);
        }

        public override void Update(ref float time)
        {
            emitter.Update(ref time);
            base.Update(ref time);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            emitter.Draw(spriteBatch);
            //spriteBatch.Draw(myTexture, position, Color.AliceBlue);
        }

    }
}
