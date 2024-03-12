using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class MonitorPC : PhysicsObject
    {
        SpriteMap _sprite;
        bool hitted;
        public MonitorPC(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/Monitor.png"), 12, 14, false);
            center = new Vec2(6f, 7f);
            collisionSize = new Vec2(10f, 12f);
            collisionOffset = new Vec2(-5f, -6f);
            graphic = _sprite;
            _sprite.AddAnimation("console", 0.02f, true, new int[] {
                0,
                1
            });
            _sprite.AddAnimation("idle", 1f, false, new int[] { 2 });
            _sprite.SetAnimation("console");
            thickness = 9;
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
                thickness = 0;
            }
            if (hitted == false)
            {
                hitted = true;
                _sprite.frame = 2;
                _sprite.SetAnimation("idle");
            }
            return base.Hit(bullet, hitPos);
        }
    }
}
