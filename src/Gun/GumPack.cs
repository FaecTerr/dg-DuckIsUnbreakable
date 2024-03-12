using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Guns")]
    public class GumPack : Gun
    {
        public SpriteMap _sprite;
        public GumPack(float xpos, float ypos) : base(xpos, ypos)
        {
            ammo = 4;
            _ammoType = new ATShrapnel();
            _type = "gun";
            center = new Vec2(6f, 3f);
            collisionOffset = new Vec2(-6f, -3f);
            collisionSize = new Vec2(12f, 6f);
            _sprite = new SpriteMap(GetPath("Sprites/Things/Weapons/Gum.png"), 12, 6, false);
            base.graphic = _sprite;
        }
        public override void OnPressAction()
        {
            if (ammo > 0)
            {
                ammo--;
                SFX.Play("smallSplat", 1f, Rando.Float(-0.6f, 0.6f), 0f, false);
                Duck duckOwner = owner as Duck;
                if (duckOwner != null)
                {
                    float addSpeedX = 0f;
                    float addSpeedY = 0f;
                    if (duckOwner.inputProfile.Down("LEFT"))
                    {
                        addSpeedX -= 3f;
                    }
                    if (duckOwner.inputProfile.Down("RIGHT"))
                    {
                        addSpeedX += 3f;
                    }
                    if (duckOwner.inputProfile.Down("UP"))
                    {
                        addSpeedY -= 3f;
                    }
                    if (duckOwner.inputProfile.Down("DOWN"))
                    {
                        addSpeedY += 3f;
                    }
                    if (base.isServerForObject)
                    {
                        Gum b = new Gum(base.barrelPosition.x, base.barrelPosition.y);
                        if (!duckOwner.crouch)
                        {
                            b.hSpeed = (float)offDir * Rando.Float(3f, 3.5f) + addSpeedX;
                            b.vSpeed = -1.5f + addSpeedY + Rando.Float(-0.5f, -1f);
                        }
                        b.clip.Add(duckOwner);
                        duckOwner.clip.Add(b);
                        Level.Add(b);
                    }
                }
            }
        }
    }
}
