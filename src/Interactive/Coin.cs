using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Special")]
    public class SmallCoin : Holdable
    {
        public SpriteMap _sprite;
        public SmallCoin(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Coin.png"), 8, 8, false);
            graphic = _sprite;
            center = new Vec2(4f, 4f);
            collisionOffset = new Vec2(-4f, -4f);
            collisionSize = new Vec2(8f, 8f);
            base.depth = -0.5f;
            weight = 0.5f;
        }
    }
}
