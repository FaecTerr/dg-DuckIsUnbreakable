using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class FreezedDuck : Thing
    {
        public static Dictionary<Duck, bool> FreezeDucks = new Dictionary<Duck, bool>();
        public Duck duck;
        public float Timer;

        public FreezedDuck(float xval, float yval, Duck duck) : base(0f, 0f)
        {
            graphic = new Sprite(Mod.GetPath<DuckUnbreakable>("Sprites/Things/VFX/IceDuck.png"), 0f, 0f);
            center = new Vec2(16f, 16f);
            Timer = 3f;
            if (duck != null)
            {
                this.duck = duck;
                position = duck.position;
                FreezeDucks[duck] = true;
            }
        }

        public override void Update()
        {
            base.Update();
            if (duck != null)
            {
                if (duck.inputProfile.Pressed("RIGHT") && duck.grounded)
                {
                    duck.velocity = new Vec2(1.5f, -1f);
                }
                if (duck.inputProfile.Pressed("LEFT") && duck.grounded)
                {
                    duck.velocity = new Vec2(-1.5f, -1f);
                }
                if (duck.inputProfile.Pressed("JUMP") && duck.grounded)
                {
                    duck.velocity = new Vec2(0f, -2f);
                }
                position = duck.position; 
                if (duck.onFire == true)
                {
                    duck.onFire = false;
                    unFreezeDuck();
                }
                if (duck.dead)
                {
                    Level.Remove(this);
                    duck.visible = true;
                }
                else
                {
                    duck.visible = false;
                    duck.immobilized = true;
                    if (Timer > 0)
                    {
                        Timer -= 0.01666666f;
                    }
                    else
                    {
                        unFreezeDuck();
                    }
                }
            }
                
        }

        public virtual void unFreezeDuck()
        {
            if (duck != null)
            {
                duck.visible = true;
                duck.immobilized = false;
            }
            Level.Remove(this);
        }
    }
}
