using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class FGblank : PhysicsParticle
    {
        public FGblank(float xpos, float ypos, string bounceSound = "metalBounce") : base(xpos, ypos)
        {
            hSpeed = -4f - Rando.Float(3f);
            vSpeed = -(Rando.Float(1.5f) + 1f);
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Weapons/FGShell"), 6, 10, false);
            graphic = _sprite;
            center = new Vec2(3f, 5f);
            _bounceSound = bounceSound;
            base.depth = 0.3f + Rando.Float(0f, 0.1f);
        }
        public override void Update()
        {
            base.Update();
            _angle = Maths.DegToRad(-_spinAngle);
        }

        private SpriteMap _sprite;
    }
}