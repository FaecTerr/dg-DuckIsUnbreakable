using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Special|Traps")]
    public class WireSaw : Thing, IWirePeripheral
    {
        public SpriteMap _sprite;
        public float rise;
        public EditorProperty<bool> swap;
        public int mFrame;
        public bool act;
        public StateBinding _act = new StateBinding("act", -1, false, false);
        public WireSaw(float xpos, float ypos) : base(xpos, ypos, null)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Tilesets/WireSaw.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            _sprite.AddAnimation("idle", 0.1f, true, new int[] {
                0
            });
            _sprite.AddAnimation("sawing", 0.1f, true, new int[] {
                0,
                1,
                2,
                3
                
            });
            base.depth = -0.5f;
            _sprite.SetAnimation("idle");
            xscale = yscale *= 1.5f;
            _editorName = "Wire Saw";
            base.layer = Layer.Foreground;
            _canFlip = true;
            swap = new EditorProperty<bool>(false);
        }
        public override float angle
        {
            get
            {
                return base.angle;
            }
            set
            {
                _angle = value;
            }
        }
        public override void Update()
        {
            base.Update();
            if(mFrame > 0)
            {
                mFrame--;
            }
            if (rise > 0.1f)
            {
                _sprite.SetAnimation("sawing");
                if (mFrame < 1 && act == true && rise < 0.3f)
                {
                    SFX.Play(GetPath("SFX/SawFadeIn.wav"), 1f, 0f, 0.0f, false);
                    mFrame = 68;
                }
                if (mFrame < 1 && act == false)
                {
                    SFX.Play(GetPath("SFX/SawFadeOut.wav"), 1f, 0f, 0.0f, false);
                    mFrame = 68;
                }
            }
            else
            {
                _sprite.SetAnimation("idle");     
            }
            if (act == true)
            {
                if (mFrame < 1)
                {
                    SFX.Play(GetPath("SFX/SawMiddle.wav"), 1f, 0f, 0.0f, false);
                    mFrame = 60;
                }
                if (swap == false)
                {
                    if (rise < 5f)
                    {
                        rise += 0.1f;
                    }
                    foreach (Duck d in Level.CheckCircleAll<Duck>(position, 16f))
                    {
                        if (d != null)
                        {
                            d.Kill(new DTCrush(null));
                        }
                    }
                    foreach (Holdable h in Level.CheckCircleAll<Holdable>(position, 16f))
                    {
                        if (h != null)
                        {
                            h.Hurt(0.2f);
                        }
                    }
                }
            }
            else
            {
                if (rise > 0f)
                {
                    rise *= 0.9f;
                }
            }
            angle += rise;
        }
        public void Pulse(int type, WireTileset wire)
        {
            if (type == 0)
            {
                if (swap == false)
                {
                    act = true;
                    rise += 10f;
                    foreach (Duck d in Level.CheckCircleAll<Duck>(position, 16f))
                    {
                        if (d != null)
                        {
                            d.Kill(new DTCrush(null));
                        }
                    }
                    foreach (Holdable h in Level.CheckCircleAll<Holdable>(position, 16f))
                    {
                        if (h != null)
                        {
                            h.Hurt(0.6f);
                        }
                    }
                    act = false;
                }
                else
                    return;
            }
            if (type == 1)
            {
                if (swap == false)
                {
                    act = true;
                }
                else
                    act = false;
                return;
            }
            if (type == 2)
            {
                if (swap == false)
                    act = false;
                else
                {
                    act = true;
                }
            }
        }
    }
}
