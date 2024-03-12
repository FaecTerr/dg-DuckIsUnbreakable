using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class TheHandForce : Thing
    {
        private float _alphaSub;
        private float _speed;
        private float _speedv;
        protected SpriteMap _sprite;
        private bool fake;
        private List<Thing> _hits = new List<Thing>();

        public TheHandForce(float xpos, float ypos, int dir, float alphaSub, float speed, float speedv, Duck own, bool fake) : base(xpos, ypos, null)
        {
            offDir = (sbyte)dir;
            _sprite = new SpriteMap(base.GetPath("Sprites/Stands/handForce.png"), 32, 32, false);
            base.graphic = _sprite;
            center = new Vec2((float)graphic.w, (float)graphic.h);
            _alphaSub = alphaSub;
            _speed = speed;
            _speedv = speedv;
            _sprite.AddAnimation("idle", 0.5f, false, new int[] {
                0,
                1,
                2,
                3
            });
            _sprite.SetAnimation("idle");
            _collisionSize = new Vec2(32f, 32f);
            _collisionOffset = new Vec2(-16f, -16f);
            graphic.flipH = (offDir <= 0);
            owner = own;
            fake = fake;
            base.depth = -0.7f;
        }

        public override void Update()
        {
            base.x += (float)offDir * _speed;
            base.y += _speedv;
            base.alpha -= _alphaSub;
            if (base.alpha <= 0f)
            {
                Level.Remove(this);
            }
            if (base.alpha > 0.1f && fake == false)
            {
                IEnumerable<PhysicsObject> hits = Level.CheckRectAll<PhysicsObject>(base.topLeft, base.bottomRight);
                foreach (PhysicsObject hit in hits)
                {
                    if (!_hits.Contains(hit) && hit != owner)
                    {
                        if (owner != null)
                        {
                            Thing.Fondle(hit, owner.connection);
                        }
                        Grenade g = hit as Grenade;
                        if (g != null)
                        {
                            g.PressAction();
                        }
                        hit.hSpeed = ((_speed - 2.5f) * (float)offDir * 1.5f + (float)offDir * 4f) * base.alpha;
                        hit.vSpeed = (_speedv + -4.5f) * base.alpha;
                        hit.clip.Add(owner as MaterialThing);
                        if (!hit.destroyed && !(hit is Stand) && !(hit is Duck))
                        {
                            Level.Remove(hit);
                        }
                        if (hit is Duck)
                        {
                            Duck d = hit as Duck;
                            if (d.ragdoll != null)
                                d.ragdoll.Unragdoll();
                            d.Kill(new DTImpact(this));
                        }
                        else if (hit is RagdollPart)
                        {
                            RagdollPart r = hit as RagdollPart;
                            r.Destroy(new DTImpact(this));
                        }
                        _hits.Add(hit);
                    }
                }
                IEnumerable<Door> doors = Level.CheckRectAll<Door>(base.topLeft, base.bottomRight);
                foreach (Door hit2 in doors)
                {
                    if (owner != null)
                    {
                        Thing.Fondle(hit2, owner.connection);
                    }
                    if (!hit2.destroyed)
                    {
                        hit2.Destroy(new DTImpact(this));
                    }
                }
                IEnumerable<Window> windows = Level.CheckRectAll<Window>(base.topLeft, base.bottomRight);
                foreach (Window hit3 in windows)
                {
                    if (owner != null)
                    {
                        Thing.Fondle(hit3, owner.connection);
                    }
                    if (!hit3.destroyed)
                    {
                        hit3.Destroy(new DTImpact(this));
                    }
                }
            }
        }
    }
}
