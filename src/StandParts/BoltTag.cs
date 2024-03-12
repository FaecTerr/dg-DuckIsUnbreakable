using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class BoltTag : Thing
    {
        public Duck duck;
        public int maxHealth = 3;
        public int Health = 3;
        public Duck causedBy;
        public float time;

        public BoltTag(Duck duck, Duck cause) : base()
        {
            duck = duck;
            causedBy = cause;
        }
        public override void Update()
        {
            base.Update();

            if(duck != null)
            {
                if(Health <= 0)
                {
                    duck.Kill(new DTCrush(duck));
                }
                else
                {
                    if(duck.gravMultiplier > 0.2f)
                    {
                        duck.gravMultiplier -= 0.004f;
                    }
                }

                if(duck.holdObject != null)
                {
                    duck.holdObject.gravMultiplier = duck.gravMultiplier;
                }
                if(causedBy != null)
                {
                    if((causedBy.position - duck.position).length > 16 * 25)
                    {
                        if(duck.gravMultiplier < 1)
                        {
                            duck.gravMultiplier = 1;
                        }
                        Level.Remove(this);
                    }
                }
            }           

        }

        public override void Draw()
        {
            if (duck != null)
            {
                SpriteMap _bolt = new SpriteMap(GetPath("Sprites/Stands/Bolt.png"), 8, 8);
                _bolt.center = new Vec2(4, 4);
                float angl = time;
                int iterations = maxHealth;
                for (int i = 0; i < Health; i++)
                {
                    float ang = time * 24 + i * (360 / iterations);
                    angl = Maths.DegToRad(ang);
                    //angl = time + 2 * (float)Math.PI * (i / maxHealth);
                    
                    _bolt.angle = angl;
                    //float ang = Maths.DegToRad(angl);
                    Graphics.Draw(_bolt, duck.position.x + 12 * (float)Math.Cos(angl), duck.position.y + 12 * (float)Math.Sin(angl));
                }

                time += 0.03f;
            }
            base.Draw();
        }
    }
}
