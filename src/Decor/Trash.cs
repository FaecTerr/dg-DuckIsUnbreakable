using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class Trash : Block
    {
        SpriteMap _sprite;
        bool hitted;
        public Trash(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/Trash.png"), 14, 20, false);
            center = new Vec2(8f, 10f);
            collisionSize = new Vec2(12f, 18f);
            collisionOffset = new Vec2(-6f, -9f);
            graphic = _sprite;
            thickness = 3;
            hugWalls = WallHug.Floor;
        }
        public override void Update()
        {
            base.Update();
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if(hitted == true)
            {
                if (base.isServerForObject)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        Level.Add(new TrashParticles(x, y));
                        Level.Remove(this);
                    }
                }
            }
            if (hitted == false)
            {
                hitted = true;
                _sprite.frame = 1;
            }

            return base.Hit(bullet, hitPos);
        }
    }
}
