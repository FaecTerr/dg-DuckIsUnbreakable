using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Guns")]
    public class FreezeGun : Holdable
    {
        public SpriteMap _sprite;
        private float charge = 1f;
        private int reloadammo = 3;
        private bool loadState = false;
        public int mFrame;
        public FreezeGun()
        {
            center = new Vec2(7f, 7f);
            _sprite = new SpriteMap(GetPath("Sprites/Things/Weapons/FreezeGun.png"), 24, 12);
            graphic = _sprite;
            collisionOffset = new Vec2(-10f, -7f);
            collisionSize = new Vec2(20f, 10f);
            weight = 0.9f;
        }
        public override void Update()
        {
            base.Update();
            if (mFrame > 0)
            {
                mFrame--;
            }
            if (loadState == true && owner != null)
            {
                if (charge<1f)
                {
                    charge += 0.008888888f;
                    angleDegrees = 10f*offDir;
                }
                else
                {
                    loadState = false;
                    charge = 1f;
                    _sprite.frame = 0;
                    angleDegrees = 0f;
                }
            }
            if (charge <= 0)
            {
                _sprite.frame = 1;
                if (loadState == false && reloadammo > 0)
                {
                    loadState = true;
                    reloadammo--;
                }
                else if(reloadammo <= 0 && loadState == false)
                {
                    if(owner == null && grounded)
                    {
                        Level.Remove(this);
                        Level.Add(SmallSmoke.New(base.x, base.y));
                        Level.Add(SmallSmoke.New(base.x + 4f, base.y));
                        Level.Add(SmallSmoke.New(base.x - 4f, base.y));
                        Level.Add(SmallSmoke.New(base.x, base.y + 4f));
                        Level.Add(SmallSmoke.New(base.x, base.y - 4f));
                    }
                }
            }
            if(owner != null)
            {
                Duck d = owner as Duck;
                if (d.inputProfile.Down("SHOOT") && charge > 0f && loadState == false)
                {
                    if (mFrame < 1)
                    {
                        SFX.Play(GetPath("SFX/SteamFadeOut.wav"), 1f, 0f, 0.0f, false);
                        mFrame = 21;
                    }
                    Level.Add(new FreezedAir(x + 12f*offDir, y-3f, new Vec2(5f, 0f) * d.offDir, 0.5f, 0.3f, 0.01f));
                    charge -= 0.01666666f;
                    if(charge <= 0f)
                    {
                        Level.Add(new FGblank(x, y));
                    }
                }
                if (d.inputProfile.Released("SHOOT") && charge > 0f && loadState == false)
                {
                    charge = 0f;
                    if (charge <= 0f)
                    {
                        Level.Add(new FGblank(x, y));
                    }
                }
            }
        }
    }
}
