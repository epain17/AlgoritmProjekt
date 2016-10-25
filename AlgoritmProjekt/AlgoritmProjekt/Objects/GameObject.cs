using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Characters
{
    abstract class GameObject
    {
        Texture2D texture;
        Vector2 position;

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }



        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
