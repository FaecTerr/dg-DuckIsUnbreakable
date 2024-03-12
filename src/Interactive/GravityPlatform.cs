using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    //[EditorGroup("Faecterr's|decor")]
    /*public class GravityPlatform : Thing
    {
        public SpriteMap _sprite;
        public EditorProperty<bool> flipV;
        public GravityPlatform(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("GravityFlipper.png"), 32, 16, false);
            base.graphic = _sprite;
            base.depth = -0.7f;
            flipV = new EditorProperty<bool>(false);
            _editorName = "Gravity Changer";
            center = new Vec2(16f, 8f);
            collisionSize = new Vec2(32, 16f);
            collisionOffset = new Vec2(-16f, -8f);
        }
        public override void Draw()
        {
            base.Draw();
            flipVertical = flipV;
            _sprite.flipV = flipV;
        }
        public override void Update()
        {
            base.Update();
            foreach (Duck d in Level.CheckRectAll<Duck>(topLeft, bottomRight))
            {
                if (d != null)
                {
                    if (d.gravMultiplier > 0f && flipV == false)
                    {
                        d.gravMultiplier *= -1f;
                        d.angleDegrees = 180f;
                        d.flipHorizontal = true;
                        d.velocity += new Vec2(0.01f, 1f);
                    }
                    else if (d.gravMultiplier < 0f && flipV == true)
                    {
                        d.gravMultiplier *= -1f;
                        d.angleDegrees = 0f;
                        d.flipHorizontal = false;
                        d.velocity += new Vec2(0.01f, -1f);
                    }
                }
            }
        }
    }*/
}
