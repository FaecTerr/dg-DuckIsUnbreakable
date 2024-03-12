using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class ShortTimeBlock : Block
    {
        public float time;
        public ShortTimeBlock(float xpos, float ypos, float t = 5f) : base(xpos, ypos)
        {
            time = t;
            graphic = new SpriteMap(GetPath("empty.png"), 16, 16);
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            base.depth = -0.5f;
            thickness = 4f;
            physicsMaterial = PhysicsMaterial.Metal;
        }
        public override void Update()
        {
            base.Update();
            time -= 0.01666666f;
            if(time < 0)
            {
                foreach (PhysicsObject d in Level.CheckRectAll<PhysicsObject>(topLeft + new Vec2(-2f, -2f), bottomRight + new Vec2(2f,2f)))
                {
                    if(d.enablePhysics == true && d.grounded)
                    {
                         d.vSpeed += 0.1f;
                    }
                }
                Level.Remove(this);
            }
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (bullet.owner == this)
                return false;

            if (!bullet.rebound)
            {
                float angleAdjust = 0f;
                float xHit = position.x - hitPos.x;
                float yHit = position.y - hitPos.y;
                bool isXHit = Math.Abs(xHit) > Math.Abs(yHit);
                float xAdjust = xHit > 0 ? -1f : 1f;
                float yAdjust = yHit > 0 ? -1f : 1f;
                bool xBlocked = Level.CheckPoint<Block>(hitPos.x + xAdjust, hitPos.y) != null;
                bool yBlocked = Level.CheckPoint<Block>(hitPos.x, hitPos.y + yAdjust) != null;
                if (isXHit && xBlocked)
                    isXHit = false;
                if (!isXHit && yBlocked)
                    isXHit = true;
                if (xBlocked && yBlocked)
                    return true;
                if (isXHit)
                    angleAdjust = 180f;
                Bullet reboundBullet = bullet.ammo.GetBullet(hitPos.x, hitPos.y, this, bullet.angle + angleAdjust, bullet.firedFrom, bullet.range - ((bullet.bulletDistance > 0f) ? bullet.bulletDistance : bullet.range / 2f));
                Level.Add(reboundBullet);
            }
            return true;
        }
    }
}
