using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class WoodParts : PhysicsObject
    {
        protected SpriteMap _sprite;
        private float randomSpin = Rando.Float(0.3f);

        public WoodParts(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Blocks/WoodParts.png"), 12, 6, false);
            _sprite.frame = Rando.Int(0, 3);
            graphic = _sprite;
            center = new Vec2(6f, 3f);
            collisionSize = new Vec2(12f, 6f);
            collisionOffset = new Vec2(-6f, -3f);
            base.depth = -0.5f;
            _canFlip = false;
            vSpeed = Rando.Float(-0.2f, 1.2f);
            hSpeed = Rando.Float(-3f, 3f);
            gravMultiplier = 0.6f;
            alpha = Rando.Float(0.8f, 1f);
            thickness = 1f;
            weight = 0f;
            flammable = 1f;
        }
        public override void Update()
        {
            base.Update();
            if (!(grounded))
                angle += randomSpin;
            alpha -= 0.002f;
            if (alpha < 0.05f)
                Level.Remove(this);
            if (grounded)
                alpha -= 0.05f;
        }
    }
}
