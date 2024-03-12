using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class BeachBoy : Stand
    {
        public float Cooldown;

        public Vec2 Rod;
        public Duck target;
        public float Countdown;
        public float a;

        public BeachBoy(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 22;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 22;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            standName = "BB";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(40, 0, 153);
        }

        public override void OnHoldMove()
        {      
            if(Rod == null)
            {
                Rod = position;
            }
            else
            {
                if (target == null)
                {
                    Duck d = Level.Nearest<Duck>(Rod.x, Rod.y, _equippedDuck);
                    if (d != null && d != _equippedDuck)
                    {
                        Rod += (d.position - Rod) * 0.01f;
                    }
                }
                else
                {
                    Vec2 pos = (Rod - position);
                    if(target.position.x > Rod.x + 2)
                    {
                        a = Maths.LerpTowards(a, -0.5f, 0.02f);
                    }
                    if (target.position.x < Rod.x - 2)
                    {
                        a = Maths.LerpTowards(a, 0.5f, 0.02f);
                    }
                    target.velocity -= pos.normalized * new Vec2(0.15f, 0.05f);
                    Rod = target.position;
                    if(target.ragdoll != null)
                    {
                        Rod = target.ragdoll.position;
                        target.ragdoll.velocity -= pos.normalized * new Vec2(0.15f, 0.05f);
                    }
                    if (Countdown > 0)
                    {
                        Countdown -= 0.01666666f;
                    }
                    else
                    {
                        target.Kill(new DTCrush(this));
                        target = null;
                    }
                }
            }
            
            base.OnHoldMove();
        }

        public override void Update()
        {
            if (_equippedDuck != null)
            {
                Vec2 pos = _equippedDuck.position;
                if (_equippedDuck.ragdoll != null)
                {
                    pos = _equippedDuck.ragdoll.position;
                }
                if (Cooldown > 0)
                {
                    Cooldown -= 0.01666666f;
                }
                if ((Rod - pos).length > 120)
                {
                    Rod += (pos - Rod) * (((Rod - pos).length - 120) / 320);
                }
                if (target == null)
                {
                    target = Level.CheckCircle<Duck>(Rod, 6f, _equippedDuck);
                    Countdown = 12;
                }
                else
                {
                    Vec2 posi = (Rod - position);
                    target.velocity -= new Vec2(Maths.NormalizeSection(posi.x, -1, 1), Maths.NormalizeSection(posi.y, -1, 1)) * 0.3f;
                    Rod = target.position;
                    if (target.ragdoll != null)
                    {
                        Rod = target.ragdoll.position;
                        target.ragdoll.velocity -= new Vec2(Maths.NormalizeSection(posi.x, -1, 1), Maths.NormalizeSection(posi.y, -1, 1)) * 0.3f;
                    }
                }
            }
            else
            {
                Rod = position;
            }
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (!(Level.current is Editor))
            {
                SpriteMap _float = new SpriteMap(GetPath("Sprites/Stands/Float.png"), 13, 18);
                _float.CenterOrigin();
                _float.scale = new Vec2(0.5f, 0.5f);
                Graphics.DrawLine(position, Rod, Color.White, 2f, 0.9f);
                if (_equippedDuck != null)
                {
                    _float.angle = a;
                    Graphics.Draw(_float, Rod.x, Rod.y, 1f);
                }
            }
        }
    }
}
