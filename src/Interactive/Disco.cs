using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class Disco : Holdable, IPlatform
    {
        SpriteMap _sprite;
        public Disco(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Blocks/Disco.png"), 16, 16, false);
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(14f, 14f);
            collisionOffset = new Vec2(-7f, -7f);
            graphic = _sprite;
            thickness = 1;
        }
        public override void Update()
        {
            if(owner == null)
            {
                angle += hSpeed * 3.14159f * 0.1f;
            }
            base.Update();
        }
    }
}
