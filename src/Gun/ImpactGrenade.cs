using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Guns")]
    public class ImpactGrenade : Gun
    {
        private SpriteMap _sprite;
        public ImpactGrenade(float xpos, float ypos) : base(xpos, ypos)
        {
            weight = 0.9f;
            ammo = 1;
            _ammoType = new ATShrapnel();
            _type = "gun";
            center = new Vec2(5f, 5f);
            collisionOffset = new Vec2(-5f, -5f);
            collisionSize = new Vec2(10f, 10f);
            _sprite = new SpriteMap(GetPath("Sprites/Things/Weapons/ImpcatGrenade.png"), 10, 10, false);
            base.graphic = _sprite;
        }
        public virtual void Explode()
        {
            Level.Add(new ImpactWave(position.x, position.y, 80f, 1.2f, 1f, Color.White, false));
            foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(position, 80f))
            {
                Vec2 pos = position;
                if (p.active)
                {
                    float num = (float)Math.Atan2((double)p.y - (double)pos.y, (double)p.x - (double)pos.x);
                    p.hSpeed += 7f * (float)(4.0 / Math.Sqrt((double)(p.position - position).length / 2.0) * Math.Cos((double)num));
                    p.vSpeed += 7f * (float)(4.0 / Math.Sqrt((double)(p.position - position).length / 2.0) * Math.Sin((double)num));
                    if (p is Duck)
                    {
                        Duck d = p as Duck;
                        d.GoRagdoll();
                    }
                }            
            }
            Level.Remove(this);
        }
        public override void OnPressAction()
        {
            return;
        }
        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSolidImpact(with, from);
            if (with != null)
            {
                if (with is Block || with is AutoBlock)
                {
                    Explode();
                }
            }
        }
        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSoftImpact(with, from);
            if (with != null)
            {
                if (with is Block && isServerForObject)
                {
                    Explode();
                }
            }
        }
    }
}
