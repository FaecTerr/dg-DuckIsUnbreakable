using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class HierophantGreen : Stand
    {
        public float StandCooldown = 0f;
        public float heating = 0f;
        public bool overheating;
        public float overheatTime = 2;
        public float cooloff = 0.01667f;
        public float overheatBoost = 1.8f;

        float time;
        public HierophantGreen(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 7;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 7;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            standName = "HG";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(0, 34, 229);
            _editorName = "Hierophant Green";
        }
        public override void Update()
        {
            base.Update();
            if (StandCooldown > 0f)
            {
                StandCooldown -= cooloff;
            }
            if (heating >= overheatTime && overheating == false)
            {
                overheating = true;
            }
            if (overheating == true && heating > 0f)
            {
                heating -= cooloff * overheatBoost;
            }
            else if (overheating == true && heating <= 0f)
            {
                overheating = false;
            }
        }
        public override void OnHoldMove()
        {
            base.OnHoldMove();
            if (StandCooldown <= 0f && owner != null && overheating == false)
            {
                Duck d = owner as Duck;
                if (d.ragdoll == null)
                {
                    StandCooldown = 0.01f;
                    for (int i = 0; i < 4; i++)
                    {
                        if (isServerForObject)
                        {
                            Level.Add(new EmeraldSplash(x, y, d, new Vec2(Rando.Float(8f, 12f) * owner.offDir, Rando.Float(-1f, 1f))));
                        }
                        heating += 0.1f;
                    }
                }
            }
        }
        public override void Draw()
        {
            if (_equippedDuck != null)
            {
                SpriteMap _em = new SpriteMap(GetPath("Sprites/Stands/Emerald.png"), 14, 7, false);
                _em.center = new Vec2(4, 4);
                float angl = time;
                int iterations = 26;
                if (overheating)
                {
                    _em.color = Color.OrangeRed;
                }
                for (int i = 0; i < (1 - heating / overheatTime) * iterations + 1; i++)
                {
                    float ang = time * 24 + i * (360 / iterations);
                    angl = Maths.DegToRad(ang);
                    _em.angle = angl;
                    Graphics.Draw(_em, duck.position.x + 12 * (float)Math.Cos(angl), duck.position.y + 12 * (float)Math.Sin(angl));
                }

                time += 0.02f;
            }
            base.Draw();
        }
    }
}
