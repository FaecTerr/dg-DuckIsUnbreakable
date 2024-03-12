using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class JJFBolts : PhysicsObject
    {
        public SpriteMap _sprite;
        public Duck ownerDuck;

        public Vec2 spawnPos;

        public JJFBolts(float xpos, float ypos, Duck own, Vec2 velocity) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Stands/Bolt.png"), 8, 8, false);
            base.graphic = _sprite;
            depth = -0.5f;
            weight = 0f;
            ownerDuck = own;
            velocity = velocity;
            _sprite.CenterOrigin();
            center = new Vec2(4, 4);
            xscale = yscale = 0.8f;
            collisionSize = new Vec2(8, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            gravMultiplier = 0;

            spawnPos = new Vec2(xpos, ypos);
        }
        public override void Update()
        {
            base.Update();
            if((spawnPos - position).length > 25 * 16)
            {
                Level.Remove(this);
            }
            angle += _hSpeed * 0.1f;

            foreach (Duck d in Level.CheckCircleAll<Duck>(position, 3f))
            {
                if (d != ownerDuck)
                {
                    BoltTag tag = null;
                    foreach (BoltTag t in Level.current.things[typeof(BoltTag)])
                    {
                        if (t.duck == d)
                        {
                            tag = t;
                        }
                    }

                    if (tag != null)
                    {
                        tag.Health--;
                        Level.Remove(this);
                    }
                    else
                    {
                        Level.Add(new BoltTag(d, ownerDuck) { Health = 3 });
                        Level.Remove(this);
                    }
                }
            }

            if(Math.Abs(hSpeed) < 0.01f)
            {
                Level.Remove(this);
            }
        }
    }
}
