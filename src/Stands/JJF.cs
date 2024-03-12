using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class JumpinJackFlash : Stand
    {
        public float StandCooldown = 0f;
        public int bolts = 16;
        public int maxbolts = 16;
        public bool overheating;

        public float time;

        public JumpinJackFlash(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 23;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 23;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            standName = "JJF";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(255, 163, 102);
            _editorName = "Jumpin'Jack Flash";

            usingCharging = true;
        }
        public override void Update()
        {
            base.Update();
            if (bolts <= 0 && !overheating)
            {
                overheating = true;
                StandCooldown = 6f;
            }
            if (overheating && bolts <= 0)
            {
                StandCooldown -= 0.01666666f;
                if(StandCooldown <= 0)
                {
                    bolts = maxbolts;
                    overheating = false;
                }
            }
        }
        public override void OnPressMove()
        {
            base.OnPressMove();
            if (StandCooldown <= 0f && owner != null && !overheating && bolts > 0)
            {
                if (_equippedDuck.ragdoll == null)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        bolts--;
                        Level.Add(new JJFBolts(x, y, _equippedDuck, new Vec2(Rando.Float(8f, 12f) * owner.offDir, Rando.Float(-0.5f, 0.5f))));                        
                    }
                }
            }
        }

        public override void OnSpecialMoveFast()
        {
            foreach (BoltTag tag in Level.current.things[typeof(BoltTag)])
            {
                if (tag.duck != null)
                {
                    if (tag.duck.gravMultiplier < 1)
                    {
                        tag.duck.gravMultiplier = 1;
                    }
                    Level.Remove(tag);
                }
            }
            base.OnSpecialMoveFast();
        }

        public override void OnSpecialMove()
        {
            bolts = 16;
            StandCooldown = 0f;
            overheating = false;
            //OnSpecialMoveFast();
            base.OnSpecialMove();
        }

        public override void OnSpecialMoveLong()
        {
            if (_equippedDuck != null)
            {
                float angl = time;
                int iterations = maxbolts;
                for (int i = 0; i < bolts; i++)
                {
                    float ang = time * 24 + i * (360 / iterations);
                    angl = Maths.DegToRad(ang);

                    Level.Add(new JJFBolts(x, y, _equippedDuck, new Vec2(12 * (float)Math.Cos(angl), 12 * (float)Math.Sin(angl))));
                }
                bolts = 0;
            }
            base.OnSpecialMoveLong();
        }

        public override void Draw()
        {
            if (_equippedDuck != null)
            {
                SpriteMap _bolt = new SpriteMap(GetPath("Sprites/Stands/Bolt.png"), 8, 8);
                _bolt.center = new Vec2(4, 4);
                float angl = time; 
                int iterations = maxbolts;
                for (int i = 0; i < bolts; i++)
                {
                    float ang = time * 24 + i * (360 / iterations);
                    angl = Maths.DegToRad(ang);
                    //angl = time + 2 * (float)Math.PI * (i / maxbolts);
                    
                    _bolt.angle = angl;
                    //float ang = Maths.DegToRad(angl);
                    Graphics.Draw(_bolt, duck.position.x + 12 * (float)Math.Cos(angl), duck.position.y + 12 * (float)Math.Sin(angl));
                }

                time += 0.02f;
            }
            base.Draw();
        }
    }
}
