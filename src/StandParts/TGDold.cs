using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{

    public class TGDold : Thing
    {
        public static Dictionary<Duck, bool> OldingDucks = new Dictionary<Duck, bool>();
        public Duck duck;
        public float Timer;
        public Duck causedBy;

        public TGDold(Duck duck, Duck cause) : base(0f, 0f)
        {
            Timer = 30f;
            causedBy = cause;
            if (duck != null)
            {
                this.duck = duck;
                OldingDucks[duck] = true;
            }
        }
        public override void Update()
        {
            base.Update();
            if (duck != null)
            {
                if (Timer > 0f)
                {
                    if (duck.holdObject is IceBlock)
                    {
                        Timer = 30f;
                    }
                    else
                    {
                        Timer -= 0.01666666f;
                    }
                }
                else if (!duck.dead)
                {
                    duck.Kill(new DTImpact(this));
                }
                if (Timer < 30 * 0.75f)
                {
                    duck.velocity = new Vec2(duck.velocity.x, duck.velocity.y < 0 ? duck.velocity.y * 0.95f : duck.velocity.y);
                }
                if (Timer < 15f)
                {
                    duck.velocity = new Vec2(duck.velocity.x * 0.9f, duck.velocity.y);
                }
                if (Timer < 30 * 0.25f)
                {
                    Duck d = duck as Duck;
                    if (d != null)
                    {
                        if (d.holdObject != null)
                        {
                            if (d.holdObject is Gun)
                            {
                                Gun g = d.holdObject as Gun;
                                if (g._fireWait < 1f)
                                {
                                    g._fireWait = 1.5f;
                                }
                                else if (g._fireWait < 2f)
                                {
                                    g._fireWait = 2.5f;
                                }
                                else if (g._fireWait < 4f)
                                {
                                    g._fireWait = 6.5f;
                                }
                            }
                        }
                    }
                }
                if (Timer < 8f)
                {
                    duck.velocity = new Vec2(duck.velocity.x * 0.9f, duck.velocity.y < 0 ? duck.velocity.y * 0.95f : duck.velocity.y);
                }
            }
        }

        public override void Draw()
        {
            if(duck != null)
            {
                if (duck.profile.localPlayer)
                {
                    SpriteMap _clock = new SpriteMap(GetPath("Sprites/Stands/TGDClocks.png"), 17, 17);
                    SpriteMap _arrow = new SpriteMap(GetPath("Sprites/Stands/TGDArrow.png"), 5, 11);

                    _clock.CenterOrigin();
                    _arrow.center = new Vec2(2.5f, 9.5f);
                    _arrow.angleDegrees = -360 * (Timer - 30) / 30;

                    Graphics.Draw(_clock, duck.position.x, duck.position.y - 24f, 0.99f);
                    Graphics.Draw(_arrow, duck.position.x, duck.position.y - 24f, 0.99f);
                }
            }
            base.Draw();
        }
    }
}