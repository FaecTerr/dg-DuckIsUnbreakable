using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class Metallica : Stand
    {
        private float counter;
        private float SFXplay;
        private float punchDelay;
        private float StandCooldown = 0f;
        public NetSoundEffect _netLoad1 = new NetSoundEffect(new string[]
{
            GetPath<DuckUnbreakable>("SFX/Metallica.wav"),
});
        public Metallica(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 11;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _sprite.frame = 11;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            standName = "MTL";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(0, 148, 255);


            usingCharging = true;
        }
        public override void OnStandMove()
        {
            base.OnStandMove();
            if (StandCooldown <= 0f && owner != null)
            {
                float range = 120f;
                foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(position, range))
                {
                    Vec2 pos = duck.position;
                    if (p.active)
                    {
                        if (isServerForObject && !(p is Duck) && (!(p is Holdable) || ((p as Holdable).duck == null && (p as Holdable).equippedDuck == null)))
                        {
                            Fondle(p);
                        }
                        float mod = 0.8f;
                        if (p.physicsMaterial == PhysicsMaterial.Metal)
                        {
                            mod = 1.2f;
                        }
                        if (p is Duck)
                        {
                            mod = 1f;
                        }
                        bool flag = true;
                        foreach (Duck d in Level.CheckCircleAll<Duck>(position, 5f))
                        {
                            flag = d == duck;
                        }
                        if (p != duck && flag)
                        {
                            float num = (float)Math.Atan2((double)p.y - pos.y, (double)p.x - pos.x);
                            p.hSpeed += range * mod * 0.04f * (float)(4.0 / Math.Sqrt((double)(p.position - duck.position).length / 2.0) * Math.Cos((double)num));
                            p.vSpeed += range * mod * 0.04f * (float)(4.0 / Math.Sqrt((double)(p.position - duck.position).length / 2.0) * Math.Sin((double)num));
                            if (p is Duck)
                            {
                                Duck dck = p as Duck;
                                if ((position - dck.position).length < 32f && position.y > dck.position.y || (position - dck.position).length < 24f && position.y < dck.position.y)
                                {
                                    dck.GoRagdoll();
                                }
                            }
                        }
                        for (int i = 0; i < 20; i++)
                        {
                            float len = Rando.Float(range * 0.3f);
                            float ang = Rando.Float(0, 360);
                            float angl = Maths.DegToRad(ang);

                            float x1 = len * (float)Math.Cos(angl);
                            float y1 = len * (float)Math.Sin(angl);
                            Level.Add(new thickAirPushed(position.x + x1, position.y + y1) { pushVector = new Vec2(x1, y1).normalized * 6, moveVector = new Vec2(x1, y1).normalized * 9, lifeTime = 9 });
                            Level.Add(new thickAirPushed(position.x - x1, position.y - y1) { pushVector = -new Vec2(x1, y1).normalized * 6, moveVector = -new Vec2(x1, y1).normalized * 9, lifeTime = 9 });
                        }
                    }
                }
                StandCooldown = 1f;
                SFX.Play(GetPath("SFX/Metallica.wav"), 1f, 0f, 0f, false);
            }
        }
        public override void OnSpecialMoveFast()
        {
            base.OnSpecialMoveFast();
            if (StandCooldown <= 0f && owner != null)
            {
                float range = 160f;
                foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(position, range))
                {
                    Vec2 pos = duck.position;
                    if (p.active)
                    {
                        if (isServerForObject && !(p is Duck) && (!(p is Holdable) || ((p as Holdable).duck == null && (p as Holdable).equippedDuck == null)))
                        {
                            Fondle(p);
                        }
                        float mod = 0.8f;
                        if (p.physicsMaterial == PhysicsMaterial.Metal)
                        {
                            mod = 1.2f;
                        }
                        if (p is Duck)
                        {
                            mod = 1f;
                        }
                        bool flag = true;
                        foreach (Duck d in Level.CheckCircleAll<Duck>(position, 5f))
                        {
                            flag = d == duck;
                        }
                        if (p != duck && flag)
                        {
                            float num = (float)Math.Atan2((double)p.y - pos.y, (double)p.x - pos.x);
                            p.hSpeed -= range * mod * 0.04f * (float)(4.0 / Math.Sqrt((double)(p.position - duck.position).length / 2.0) * Math.Cos((double)num));
                            p.vSpeed -= range * mod * 0.06f * (float)(4.0 / Math.Sqrt((double)(p.position - duck.position).length / 2.0) * Math.Sin((double)num));
                            if (p is Duck)
                            {
                                Duck dck = p as Duck;
                                if ((position - dck.position).length < 48f && position.y < dck.position.y || (position - dck.position).length < 32f && position.y > dck.position.y)
                                {
                                    dck.GoRagdoll();
                                }
                            }
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            float len = Rando.Float(range * 0.7f, range);
                            float ang = Rando.Float(0, 360);
                            float angl = Maths.DegToRad(ang);

                            float x1 = len * (float)Math.Cos(angl);
                            float y1 = len * (float)Math.Sin(angl);
                            Level.Add(new thickAirPushed(position.x + x1, position.y + y1) { pushVector = -new Vec2(x1, y1).normalized * 6, moveVector = -new Vec2(x1, y1).normalized * 9, lifeTime = 9 });
                            Level.Add(new thickAirPushed(position.x - x1, position.y - y1) { pushVector = new Vec2(x1, y1).normalized * 6, moveVector = new Vec2(x1, y1).normalized * 9, lifeTime = 9 });
                        }
                    }
                }
                StandCooldown = 1f;
                SFX.Play(GetPath("SFX/Metallica.wav"), 1f, 0f, 0f, false);
            }
        }
        public override void OnSpecialMoveLong()
        {
            base.OnSpecialMoveLong();
            if (StandCooldown <= 0f && owner != null)
            {
                float range = 300f;
                foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(position, range))
                {
                    Vec2 pos = duck.position;
                    if (p.active)
                    {
                        if (isServerForObject && !(p is Duck) && (!(p is Holdable) || ((p as Holdable).duck == null && (p as Holdable).equippedDuck == null)))
                        {
                            Fondle(p);
                        }
                        float mod = 0.8f;
                        if (p.physicsMaterial == PhysicsMaterial.Metal)
                        {
                            mod = 1.2f;
                        }
                        if (p is Duck)
                        {
                            mod = 1f;
                        }
                        bool flag = true;
                        foreach (Duck d in Level.CheckCircleAll<Duck>(position, 5f))
                        {
                            flag = d == duck;
                        }
                        if (p != duck && flag)
                        {
                            float num = (float)Math.Atan2((double)p.y - pos.y, (double)p.x - pos.x);
                            p.hSpeed -= range * mod * 0.06f * (float)(4.0 / Math.Sqrt((double)(p.position - duck.position).length / 2.0) * Math.Cos((double)num));
                            p.vSpeed -= range * mod * 0.06f * (float)(4.0 / Math.Sqrt((double)(p.position - duck.position).length / 2.0) * Math.Sin((double)num));
                            if (p is Duck)
                            {
                                Duck dck = p as Duck;
                                if ((position - dck.position).length < 80f && position.y > dck.position.y || (position - dck.position).length < 60f && position.y < dck.position.y)
                                {
                                    dck.GoRagdoll();
                                }
                            }
                        }
                        for (int i = 0; i < 30; i++)
                        {
                            float len = Rando.Float(range * 0.7f, range);
                            float ang = Rando.Float(0, 360);
                            float angl = Maths.DegToRad(ang);

                            float x1 = len * (float)Math.Cos(angl);
                            float y1 = len * (float)Math.Sin(angl);
                            Level.Add(new thickAirPushed(position.x + x1, position.y + y1) { pushVector = -new Vec2(x1, y1).normalized * 11, moveVector = -new Vec2(x1, y1).normalized * 18, lifeTime = 9 });
                            Level.Add(new thickAirPushed(position.x - x1, position.y - y1) { pushVector = new Vec2(x1, y1).normalized * 11, moveVector = new Vec2(x1, y1).normalized * 18, lifeTime = 9 });
                        }
                    }
                }
                StandCooldown = 1f;
                SFX.Play(GetPath("SFX/Metallica.wav"), 1f, 0f, 0f, false);
            }
        }
        public override void OnSpecialMove()
        {
            base.OnSpecialMove();
            if (StandCooldown <= 0f && owner != null)
            {
                float range = 300f;
                foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(position, range))
                {
                    Vec2 pos = duck.position;
                    if (p.active)
                    {
                        if (isServerForObject && !(p is Duck) && (!(p is Holdable) || ((p as Holdable).duck == null && (p as Holdable).equippedDuck == null)))
                        {
                            Fondle(p);
                        }
                        float mod = 0.8f;
                        if (p.physicsMaterial == PhysicsMaterial.Metal)
                        {
                            mod = 1.2f;
                        }
                        if (p is Duck)
                        {
                            mod = 1f;
                        }
                        bool flag = true;
                        foreach (Duck d in Level.CheckCircleAll<Duck>(position, 5f))
                        {
                            flag = d == duck;
                        }
                        if (p != duck && flag)
                        {
                            float num = (float)Math.Atan2((double)p.y - pos.y, (double)p.x - pos.x);
                            p.hSpeed += range * mod * 0.09f * (float)(4.0 / Math.Sqrt((double)(p.position - duck.position).length / 2.0) * Math.Cos((double)num));
                            p.vSpeed += range * mod * 0.13f * (float)(4.0 / Math.Sqrt((double)(p.position - duck.position).length / 2.0) * Math.Sin((double)num));
                            if (p is Duck)
                            {
                                Duck dck = p as Duck;
                                if ((position - dck.position).length < 80f && position.y < dck.position.y || (position - dck.position).length > 60f && position.y < dck.position.y)
                                {
                                    dck.GoRagdoll();
                                }
                            }
                        }

                        for (int i = 0; i < 30; i++)
                        {
                            float len = Rando.Float(range * 0.3f);
                            float ang = Rando.Float(0, 360);
                            float angl = Maths.DegToRad(ang);

                            float x1 = len * (float)Math.Cos(angl);
                            float y1 = len * (float)Math.Sin(angl);
                            Level.Add(new thickAirPushed(position.x + x1, position.y + y1) { pushVector = new Vec2(x1, y1).normalized * 11, moveVector = new Vec2(x1, y1).normalized * 18, lifeTime = 9 });
                            Level.Add(new thickAirPushed(position.x - x1, position.y - y1) { pushVector = -new Vec2(x1, y1).normalized * 11, moveVector = -new Vec2(x1, y1).normalized * 18, lifeTime = 9 });
                        }
                    }
                }
                StandCooldown = 1f;
                SFX.Play(GetPath("SFX/Metallica.wav"), 1f, 0f, 0f, false);
            }
        }
        public override void Update()
        {
            base.Update();
            if (StandCooldown > 0f)
            {
                StandCooldown -= 0.02f;
            }
        }
    }
}
