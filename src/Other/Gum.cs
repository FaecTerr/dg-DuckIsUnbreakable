using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class Gum : PhysicsObject
    {
        public SpriteMap _sprite;
        public Gum(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(6f, 3f);
            collisionOffset = new Vec2(-6f, -3f);
            collisionSize = new Vec2(12f, 6f);
            weight = 0f;
            _sprite = new SpriteMap(GetPath("Sprites/Things/Weapons/Gum.png"), 12, 6, false);
            base.graphic = _sprite;
        }
        public override void Update()
        {
            base.Update();
            _sprite.frame = 1;
            if (grounded)
            {
                _sprite.frame = 2;
            }
            foreach(Duck d in Level.CheckLineAll<Duck>(position + new Vec2(0f, -2f), position + new Vec2(0f, -6f)))
            {
                d.velocity = new Vec2(d.velocity.x * 0.6f, d.velocity.y < 0 ? d.velocity.y * 0f : d.velocity.y);
            }
        }
    }
}
