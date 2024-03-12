using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class ShortTimeBackground : Thing
    {
        public SpriteMap _sprite;
        public float time;
        public ShortTimeBackground(float xpos, float ypos, float t = 5f) : base(xpos, ypos)
        {
            time = t;
            _sprite = new SpriteMap(GetPath("Sprites/Stands/RoomTiles.png"), 80, 80);
            //_sprite.CenterOrigin();
            graphic = _sprite;
            center = new Vec2(40f, 40f);
            base.depth = -0.5f;
            solid = false;
        }
        public override void Update()
        {
            base.Update();
            time -= 0.01666666f;
            if (time < 0)
            {
                Level.Remove(this);
            }
        }
    }
}

