using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class RattGun : Holdable
    {
        private float SFXplay;
        private SpriteMap _sprite;
        public int ammo = 1;
        public int reload;

        public RattGun(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            _sprite = new SpriteMap((GetPath("Sprites/Stands/RatGun.png")), 17, 7, false);
            center = new Vec2(8.5f, 5f);
            collisionOffset = new Vec2(-8f, -2f);
            collisionSize = new Vec2(16, 4f);
            _editorName = "RG";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            graphic = _sprite;

            
        }
        public override void Update()
        {
            _sprite.frame = 3 - ammo;
            if(_sprite.frame > 2)
            {
                _sprite.frame = 2;
            }
            if(owner == null)
            {
                Level.Remove(this);
            }
            if(owner != null)
            {
                if(owner is Duck)
                {
                    Duck d = owner as Duck;
                    if (d.inputProfile.Down("SHOOT") && !d.immobilized && reload <= 0 && ammo > 0)
                    {
                        Fire();
                    }
                }
            }
            if(reload > 0)
            {
                reload--;
            }
            base.Update();
        }

        public virtual void Fire()
        {
            ammo--;
            reload = 10;


            RatDart dart = new RatDart(position.x + 10 * offDir, position.y) { hSpeed = 10 * (float)Math.Cos(angle) * offDir, vSpeed = 5 * (float)Math.Sin(angle) };
            dart.offDir = offDir;
            dart.d = owner as Duck;
            Level.Add(dart);

        }
    }

    public class RatDart : PhysicsObject
    {
        public SpriteMap _sprite;
        public Duck d;

        public RatDart(float xpos, float ypos) : base(xpos, ypos)
        {
            gravMultiplier = 0;
            _sprite = new SpriteMap(GetPath("Sprites/Stands/RatDart.png"), 12, 8, false);
            graphic = _sprite;
            center = new Vec2(6f, 4f);
            collisionSize = new Vec2(12f, 8f);
            collisionOffset = new Vec2(-6f, -4f);
            weight = 0f;
            thickness = 0f;
        }

        public override void Update()
        {
            base.Update();
            if (Level.CheckPoint<Block>(position) != null || (hSpeed == 0 && vSpeed == 0))
            {
                Level.Remove(this);
            }
            if (d != null)
            {
                if (Level.CheckPoint<Duck>(position) != null)
                {
                    Duck duck = Level.CheckRect<Duck>(topLeft, bottomRight, d);
                    if (duck != null)
                    {
                        if (duck.profile != null && duck != d)
                        {
                            if (duck.profile.localPlayer)
                            {
                                Level.Add(new RattSplash(position.x, position.y) { duck = duck});
                            }
                            Level.Remove(this);
                        }
                    }
                }
            }
        }
    }
}
