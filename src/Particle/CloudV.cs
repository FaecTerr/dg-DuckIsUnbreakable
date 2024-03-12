using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class CloudV : PhysicsObject
    {
        protected SpriteMap _sprite;

        public CloudV(float xpos, float ypos, sbyte offDir) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/VFX/Cloud.png"), 16, 16, false);
            _sprite.frame = 0;
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            base.depth = -0.5f;
            gravMultiplier = -0.1f;
            thickness = 0f;
            weight = 1f;
            vSpeed = Rando.Float(6f, 10f) * offDir;
            hSpeed = Rando.Float(-0.1f, 0.1f);
        }
        public override void Update()
        {
            base.Update();
            if (vSpeed > 8f || vSpeed < -8f)
            {
                _sprite.frame = 3;
            }
            else if (vSpeed > 5f || vSpeed < -5f)
            {
                _sprite.frame = 2;
            }
            else if (vSpeed > 2f || vSpeed < -2f)
            {
                _sprite.frame = 1;
            }
            else
            {
                _sprite.frame = 0;
            }
            xscale = yscale = 2f - alpha;
            alpha -= 0.02f;
            if (alpha <= 0f)
            {
                Level.Remove(this);
            }
        }
    }
}
