using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class NMhazeSmoke : NMEvent
    {
        public Vec2 position;
        public Duck d;
        public Vec2 dir;

        public NMhazeSmoke()
        {
        }
        public NMhazeSmoke(Vec2 pos, Duck duck, Vec2 direction)
        {
            position = pos;
            d = duck;
            dir = direction;
        }
        public override void Activate()
        {
            if (d != null)
            {
                Level.Add(new HazeSmoke(position.x, position.y, d, dir));
            }
        }
    }
}