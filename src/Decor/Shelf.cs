using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class Shelf : MaterialThing, IPlatform
    {
        SpriteMap _sprite;
        public Shelf(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/Shelf.png"), 16, 16, false);
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            graphic = _sprite;
            depth = -0.9f;
            hugWalls = WallHug.Floor;
        }
        public override void Update()
        {
            if(Level.CheckPoint<Shelf>(position+new Vec2(0f, -12f)) != null)
            {
                _sprite.frame = 1;
            }
            base.Update();
        }
    }
}
