using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class SandParticle : PhysicsParticle
    {
        private float _sin;
        private float _moveSpeed = 0.1f;
        private float _sinSize = 0.1f;
        private float _drift;
        private float _size;
        private float move;

        public SandParticle(float xpos, float ypos, Vec2 startVel, bool big = false, float move = 0f) : base(xpos, ypos)
        {
            _gravMult = 0.5f;
            _sin = Rando.Float(7f);
            _moveSpeed = Rando.Float(0.005f, 0.02f);
            _sinSize = Rando.Float(8f, 16f);
            _size = Rando.Float(0.2f, 0.6f);
            if (big)
            {
                _size = Rando.Float(0.8f, 1f);
            }
            move = move*Rando.Float(0.9f, 1.1f);
            base.life = Rando.Float(0.1f, 0.2f);
            onlyDieWhenGrounded = true;
            base.velocity = startVel;
        }
        public override void Update()
        {
            base.Update();
            if (vSpeed > 0.3f)
            {
                vSpeed = 0.3f;
            }
            if (!_grounded)
            {
                float sinOff = (float)Math.Sin((double)_sin) * _sinSize;
                _sin += _moveSpeed;
                base.x += move;
                base.x += sinOff / 60f;
            }
        }
        public override void Draw()
        {
            float num = base.z / 200f;
            float size = _size;
            Graphics.DrawRect(position + new Vec2(-size, -size), position + new Vec2(size, size), Color.Yellow * base.alpha, 0.1f, true, 1f);
        }
    }
}
