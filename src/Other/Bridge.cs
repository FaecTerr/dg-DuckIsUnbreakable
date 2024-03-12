using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class Bridge : Thing
    {
        public SpriteMap _sprite;
        public Bridge(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(16f, 12f);
            collisionOffset = new Vec2(-16f, -4f);
            collisionSize = new Vec2(32f, 8f);
            depth = -0.6f;
            _sprite = new SpriteMap(GetPath("Sprites/Tilesets/Minecart.png"), 32, 16, false);
            base.graphic = _sprite;
        }
    }
}
