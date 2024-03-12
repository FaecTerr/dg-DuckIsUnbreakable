using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class EmeraldSplash : PhysicsObject
    {
        public SpriteMap _sprite;
        public Duck ownerDuck;

        public EmeraldSplash(float xpos, float ypos, Duck own, Vec2 velocity) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Stands/Emerald.png"), 14, 7, false);
            base.graphic = _sprite;
            base.depth = -0.5f;
            weight = 0f;
            ownerDuck = own;
            velocity = velocity;
            center = new Vec2(7f, 3.5f);
            xscale = yscale = 0.8f;
            collisionSize = new Vec2(14, 7f);
            collisionOffset = new Vec2(-7f, -3.5f);
            angleDegrees += Rando.Float(-15f, 15f);
            gravMultiplier = 0;
        }
        public override void Update()
        {
            base.Update();
            if (ownerDuck != null)
            {
                Duck d = ownerDuck as Duck;
                if (d != null)
                {
                    if ((hSpeed > -1f && hSpeed < 1f) || d.ragdoll != null)
                    {
                        alpha *= hSpeed*hSpeed;
                    }
                    if (alpha < 0.35f)
                    {
                        Level.Remove(this);
                    }
                }
                else
                {
                    Level.Remove(this);
                }

                alpha -= 0.03f;

                foreach (PhysicsObject hit in Level.CheckRectAll<PhysicsObject>(base.topLeft, base.bottomRight))
                {
                    if (hit != ownerDuck)
                    {
                        if (d.ragdoll == null)
                        {
                            Grenade g = hit as Grenade;
                            if (g != null)
                            {
                                g.PressAction();
                            }
                            if (ownerDuck != null)
                            {
                                hit.clip.Add(ownerDuck as MaterialThing);
                            }
                            if (!hit.destroyed)
                            {
                                hit.Hurt(0.6f);
                                hit.Destroy(new DTImpact(this));
                            }
                        }
                    }
                }

                foreach (Door hit in Level.CheckRectAll<Door>(base.topLeft, base.bottomRight))
                {
                    if (owner != null)
                    {
                        Fondle(hit, owner.connection);
                    }
                    if (!hit.destroyed)
                    {
                        hit.Hurt(5f);
                    }
                }
                foreach (Window hit in Level.CheckRectAll<Window>(base.topLeft, base.bottomRight))
                {
                    if (owner != null)
                    {
                        Fondle(hit, owner.connection);
                    }
                    if (!hit.destroyed)
                    {
                        hit.Destroy(new DTImpact(this));
                    }
                }
            }
            else
            {
                Level.Remove(this);
            }
        }
    }
}
