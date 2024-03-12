using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class WallLedge : Thing
    {
        public WallLedge(float xval, float yval) : base(xval, yval)
        {
            graphic = new Sprite(GetPath("Sprites/Things/Weapons/Tomohawk.png"), 0f, 0f);
            center = new Vec2(7.5f, 12f);
            collisionOffset = new Vec2(-7.5f, -6f);
            collisionSize = new Vec2(15f, 16f);
        }
    }
}
