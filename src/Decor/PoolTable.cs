using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class PoolTable : Block
    {
        SpriteMap _sprite;
        bool hitted;
        public PoolTable(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Blocks/pooltable.png"), 40, 16, false);
            center = new Vec2(20f, 8f);
            collisionSize = new Vec2(36f, 14f);
            collisionOffset = new Vec2(-18f, -7f);
            graphic = _sprite;
            hugWalls = WallHug.Floor;
            thickness = 10;
        }
    }
}
