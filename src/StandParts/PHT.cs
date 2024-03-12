using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class PHT : Thing
    {
        public static Dictionary<Duck, bool> ToxicDucks = new Dictionary<Duck, bool>();
        public Duck duck;
        public float Timer;

        public PHT(Duck duck) : base(0f, 0f)
        {
            Timer = 3f;
            if (duck != null)
            {
                this.duck = duck;
                ToxicDucks[duck] = true;
            }
        }

        public override void Update()
        {
            base.Update();

            if (duck != null)
            {
                if (duck.dead)
                {
                    Level.Remove(this);
                }
                else
                {
                    if (Timer > 0)
                    {
                        Timer -= 0.01666666f;
                        if (Timer > 1.01f && Timer < 1.03f || Timer > 2.01f && Timer < 2.03f)
                        {
                            Level.Add(new HazeSmoke(duck.position.x, duck.position.y, null, new Vec2(0f, 0f)));
                        }
                    }
                    else
                    {
                        KillDuck();
                    }
                    duck.velocity = new Vec2(duck.velocity.x * 0.7f, duck.velocity.y < 0 ? duck.velocity.y * 0.9f : duck.velocity.y);
                }
            }
        }

        public virtual void KillDuck()
        {
            if (duck != null)
            {
                if (!duck.dead)
                {
                    Level.Add(new HazeSmoke(duck.position.x, duck.position.y, null, new Vec2(0f, 0f)));
                    duck.Kill(new DTImpact(this));
                }
            }
        }
    }
}
