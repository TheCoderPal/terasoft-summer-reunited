using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class Boss
    {
        private int level;
        private int health;
        private Texture2D texture;
        private Rectangle border;

        public Boss(int level, Texture2D texture, Rectangle border)
        {
            this.level = level;
            this.health = 10 * level;
            this.texture = texture;
            this.border = border;
        }

        public void UpdatePosition(Vector2 value)
        {
            border.X += (int)value.X;
            border.Y += (int)value.Y;
        }

        public void AttackBoss(int damage)
        {
            health -= damage;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, border, null, Color.White);
        }

        public bool BossDied()
        {
            return health <= 0 && health >= -1000;
        }
    }
}
