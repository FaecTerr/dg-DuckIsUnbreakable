using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class Stair : MaterialThing, IPlatform
    {
        public SpriteMap _sprite;
        public Stair(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Empty.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            base.depth = -0.5f;
        }
    }
}
