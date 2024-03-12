using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class StandForceWave : Thing
    {
        private float _alphaSub;
        private float _speed;
        private float _speedv;
        protected SpriteMap _sprite;
        protected bool recover;
        private List<Thing> _hits = new List<Thing>();

        public StandForceWave(float xpos, float ypos, int dir, float alphaSub, float speed, float speedv, Duck own, bool Recover) : base(xpos, ypos, null)
        {
            offDir = (sbyte)dir;
            _sprite = new SpriteMap(GetPath("Sprites/Stands/standForce.png"), 24, 18, false);
            base.graphic = _sprite;
            center = new Vec2(graphic.w, graphic.h);
            _alphaSub = alphaSub;
            _speed = speed;
            _speedv = speedv;
            _collisionSize = new Vec2(24f, 12f);
            _collisionOffset = new Vec2(-12f, -9f);
            graphic.flipH = (offDir <= 0);
            owner = own;
            recover = Recover;
            base.depth = -0.7f;
        }

        public override void Update()
        {
            if (base.alpha > 0.1f && recover == false)
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
                        hit.hSpeed = ((_speed - 2.5f) * offDir * 1.5f + (float)offDir * 4f) * base.alpha;
                        hit.vSpeed = (_speedv + -4.5f) * base.alpha;
                        hit.clip.Add(owner as MaterialThing);
                        if (!hit.destroyed)
                        {
                            hit.Destroy(new DTImpact(this));
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
            else if (base.alpha > 0.1f && recover == true && base.isServerForObject)
            {  
                IEnumerable<PhysicsObject> hits = Level.CheckRectAll<PhysicsObject>(base.topLeft, base.bottomRight);
                foreach (PhysicsObject hit in hits)
                {
                    PhysicsObject p = hit;
                    p.position = hit.position;
                    p.angle = hit.angle;
                    p.offDir = hit.offDir;
                    if (!_hits.Contains(hit) && hit != owner)
                    {
                        if (owner != null)
                        {
                            Thing.Fondle(hit, owner.connection);
                        }
                        hit.hSpeed = ((_speed - 2.5f) * offDir * 1.5f + (float)offDir * 4f) * base.alpha;
                        hit.vSpeed = (_speedv + -4.5f) * base.alpha;
                        hit.clip.Add(owner as MaterialThing);
                        hit._hitPoints = 50f;
                    }
                    /*if(hit is RagdollPart)
                    {
                        RagdollPart h = hit as RagdollPart;
                        if (h.duck != null)
                        {
                            h.duck.Ressurect();
                        }
                    }*/
                    if (hit is Gun)
                    {
                        Gun g = Activator.CreateInstance(hit.GetType(), Editor.GetConstructorParameters(hit.GetType())) as Gun;
                        g.offDir = hit.offDir;
                        g.position = hit.position;
                        g.angle = hit.angle;
                        if (isServerForObject)
                            Level.Add(g);
                        Level.Remove(hit);
                    }
                }
            }
            x += offDir * _speed;
            y += _speedv;
            alpha -= _alphaSub;
            if (alpha <= 0f)
            {
                Level.Remove(this);
            }
        }
    }
}
