using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    //[EditorGroup("Faecterr's|Stands")]
    public class SexPistols : Stand
    {
        public int pistons = 2;

        public bool ready;
        public float reload;

        public float Cooldown;

        public SexPistols(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 20;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 20;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            standName = "SP";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(40, 0, 153);
            
        }

        public override void OnStandMove()
        {
            ready = !ready;
            base.OnStandMove();
        }

        public override void OnSpecialMove()
        {
            OnStandMove();
            base.OnSpecialMove();
        }
        public override void OnSpecialMoveFast()
        {
            OnStandMove();
            base.OnSpecialMoveFast();
        }
        public override void OnSpecialMoveLong()
        {
            OnStandMove();
            base.OnSpecialMoveLong();
        }

        public override void Update()
        {
            if (_equippedDuck != null)
            {
                if (pistons < 6)
                {
                    reload += 0.01666666f;
                }
                if (reload > 4)
                {
                    reload = 0;
                    pistons++;
                }
            }
            if (owner != null && ready && pistons >= 6)
            {
                foreach (Bullet b in Level.current.things[typeof(Bullet)])
                {
                    if (b.owner == owner)
                    {
                        foreach(Duck d in Level.current.things[typeof(Duck)])
                        {
                            if (d != (owner as Duck))
                            {
                                Vec2 pos = b.position;
                                if (!b.rebound && Level.CheckLine<Block>(d.position, pos) == null /*&& (d.position - b.currentTravel).length < b.range*/)
                                {
                                    Vec2 vec2 = pos - d.position;
                                    Vec2 vec3 = new Vec2(vec2.x, vec2.y * -1f);
                                    float num2 = (float)Math.Atan((vec3.y / vec3.x));
                                    float angleAdjust = Maths.RadToDeg(2 * (float)Math.PI - num2);
                                    if (d.position.x < pos.x)
                                    {
                                        vec3 = new Vec2(vec2.x * 1, vec2.y * 1);
                                        num2 = (float)Math.Atan((vec3.y / vec3.x));
                                        angleAdjust = 180 - Maths.RadToDeg(2 * (float)Math.PI - num2);
                                    }
                                    pistons -= 6;
                                    Bullet reboundBullet = b.ammo.GetBullet(pos.x, pos.y, owner, angleAdjust, b.firedFrom, b.range);
                                    Level.Add(reboundBullet);
                                    Level.Remove(b);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            base.Update();
        }

        public override void Draw()
        {
            SpriteMap _c = new SpriteMap(GetPath("Sprites/Stands/Count2.png"), 15, 15, false);
            _c.CenterOrigin();
            _c.frame = pistons;
            base.Draw();
            if (_equippedDuck != null && ready)
            {
                Graphics.Draw(_c, position.x, position.y, 0.2f);
            }
        }
    }
}
