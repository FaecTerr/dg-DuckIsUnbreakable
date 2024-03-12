using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class CeilingA : Thing
    {
        SpriteMap _sprite;
        public CeilingA(float xpos, float ypos) : base(xpos, ypos, null)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/Ceiling.png"), 9, 10);
            graphic = _sprite;
            center = new Vec2(4.5f, 3f);
            base.depth = -0.95f;
            _editorCanModify = false;
        }
    }
}
