using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class CeilingB : Thing
    {
        SpriteMap _sprite;
        public CeilingB(float xpos, float ypos) : base(xpos, ypos, null)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/Ceiling.png"), 9, 10);
            _sprite.frame = 1;
            graphic = _sprite;
            center = new Vec2(4.5f, 5f);
            base.depth = -0.95f;
            _editorCanModify = false;
        }
    }
}
