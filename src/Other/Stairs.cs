using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff")]
    public class Stairs : Thing
    {
        public bool _init = false;
        public SpriteMap _sprite;
        public EditorProperty<int> stairsnum;
        public EditorProperty<int> style;
        public Stairs(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Tilesets/Stairs.png"), 16, 16, false);
            base.graphic = _sprite;
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            center = new Vec2(8f, 8f);
            stairsnum = new EditorProperty<int>(16, this, 2f, 32f, 2f, null);
            style = new EditorProperty<int>(0, this, 0, 1, 1, null);
        }
        public override void EditorPropertyChanged(object property)
        {
            if (style == -1)
            {
                (graphic as SpriteMap).frame = Rando.Int(1);
                return;
            }
            (graphic as SpriteMap).frame = style.value;
        }
        public override void Update()
        {
            base.Update();
            if (_init == false)
            {
                _init = true;
                int j = 1;
                _sprite.frame = style;
                if (flipHorizontal == true || graphic.flipH == true)
                    j = -1;
                for (int i = 0; i < 17 ; i++)
                {
                    Level.Add(new Stair(x + (16 - i*2)*j/ stairsnum * i, y + 16f - i));
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
            graphic.flipH = flipHorizontal;
        }
    }
}
