using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    //[EditorGroup("Faecterr's|Stands")]
    public class DiskSpace : Stand
    {
        private float counter;
        private float SFXplay;
        private float punchDelay;
        private float StandCooldown = 0f;
        public Holdable disk1;
        public Holdable disk2;
        public Holdable disk3;
        public bool disk1Setted = false;
        public bool disk2Setted = false;
        public bool disk3Setted = false;
        public bool disk1delete = false;
        public bool disk2delete = false;
        public bool disk3delete = false;
        public StateBinding _disk1Setted = new StateBinding("disk1Setted", -1, false, false);
        public StateBinding _disk2Setted = new StateBinding("disk2Setted", -1, false, false);
        public StateBinding _disk3Setted = new StateBinding("disk3Setted", -1, false, false);
        public StateBinding _disk1 = new StateBinding("disk1", -1, false, false);
        public StateBinding _disk2 = new StateBinding("disk2", -1, false, false);
        public StateBinding _disk3 = new StateBinding("disk3", -1, false, false);
        public StateBinding _disk1delete = new StateBinding("disk1delete", -1, false, false);
        public StateBinding _disk2delete = new StateBinding("disk2delete", -1, false, false);
        public StateBinding _disk3delete = new StateBinding("disk3delete", -1, false, false);
        public List<Bullet> firedBullets = new List<Bullet>();

        public DiskSpace(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 5;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _sprite.frame = 5;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            //_editorName = "Disk Space";
            standName = "DS";
            chargemod = 1.5f;
            standColor = new Vec3(0, 255, 144);

            usingCharging = true;
        }
        public override void OnSpecialMove()
        {
            if (owner != null && base.isServerForObject)
            {
                Duck d = owner as Duck;
                if (StandCooldown <= 0f && disk1Setted == false)
                {
                    if (duck.holdObject == null)
                    {
                        disk1 = new GaletteDisk(position.x, position.y, d);
                        Level.Add(disk1);
                        duck.GiveHoldable(disk1);
                        duck.holdObject = disk1;
                        disk1Setted = true;
                    }
                }
                else if (disk1Setted == true && disk1.grounded == true && disk1 != null)
                {
                    duck.position.x = disk1.position.x;
                    duck.position.y = disk1.position.y - 14f;
                    if (Network.isActive)
                        disk1delete = true;
                    else
                    {
                        Level.Remove(disk1);
                        disk1 = null;
                    }
                    disk1Setted = false;
                }
                else if ((disk1.y > Level.current.lowestPoint + 499f || disk1.destroyed == true) && disk1 != null && duck.holdObject == null)
                {
                    disk1Setted = false;
                    disk1 = null;
                }
                else
                    return;
            }
            base.OnSpecialMove();
        }
        public override void OnSpecialMoveLong()
        {
            if (owner != null && base.isServerForObject)
            {
                if (disk1Setted == true && disk1 != null)
                {
                    FirstDiskExplode();
                    disk1Setted = false;
                }
                if (disk2Setted == true && disk2 != null)
                {
                    SecondDiskExplode();
                    disk2Setted = false;
                }
                if (disk3Setted == true && disk3 != null)
                {
                    ThirdDiskExplode();
                    disk3Setted = false;
                }
                else
                    return;
            }
            base.OnSpecialMoveLong();
        }
        public override void OnSpecialMoveFast()
        {
            if (owner != null && base.isServerForObject)
            {
                Duck d = owner as Duck;
                if (StandCooldown <= 0f && disk2Setted == false)
                {
                    if (duck.holdObject == null)
                    {
                        disk2 = new GaletteDisk(position.x, position.y, d);
                        Level.Add(disk2);
                        duck.GiveHoldable(disk2);
                        duck.holdObject = disk2;
                        disk2Setted = true;
                    }
                }
                else if (disk2Setted == true && disk2.grounded == true && disk2 != null)
                {
                    duck.position.x = disk2.position.x;
                    duck.position.y = disk2.position.y - 14f;
                    if (Network.isActive)
                        disk2delete = true;
                    else
                    {
                        Level.Remove(disk2);
                        disk2 = null;
                    }
                    disk2Setted = false;
                }
                else if ((disk2.y > Level.current.lowestPoint + 499f || disk2.destroyed == true) && disk2 != null && duck.holdObject == null)
                {
                    disk2Setted = false;
                    disk2 = null;
                }
                else
                    return;
            }
            base.OnSpecialMoveFast();
        }
        public override void OnStandMove()
        {
            if (owner != null && base.isServerForObject)
            {
                Duck d = owner as Duck;
                if (StandCooldown <= 0f && disk3Setted == false)
                {
                    if (duck.holdObject == null)
                    {
                        disk3 = new GaletteDisk(position.x, position.y, d);
                        Level.Add(disk3);
                        duck.GiveHoldable(disk3);
                        duck.holdObject = disk3;
                        disk3Setted = true;
                    }
                }
                else if (disk3Setted == true && disk3.grounded == true && disk3 != null)
                {
                    duck.position.x = disk3.position.x;
                    duck.position.y = disk3.position.y - 14f;
                    if (Network.isActive)
                        disk3delete = true;
                    else
                    {
                        Level.Remove(disk3);
                        disk3 = null;
                    }
                    disk3Setted = false;
                }
                else if ((disk3.y > Level.current.lowestPoint + 499f || disk3.destroyed == true) && disk3 != null && duck.holdObject == null)
                {
                    disk3Setted = false;
                    disk3 = null;
                }
                else
                    return;
            }
            base.OnStandMove();
        }
        public override void Update()
        {
            if (disk1delete == true && disk1 != null)
            {
                disk1delete = false;
                Level.Remove(disk1);
                disk1 = null;
            }
            if (disk2delete == true && disk2 != null)
            {
                disk2delete = false;
                Level.Remove(disk2);
                disk2 = null;
            }
            if (disk3delete == true && disk3 != null)
            {
                disk3delete = false;
                Level.Remove(disk3);
                disk3 = null;
            }
            if (StandCooldown > 0f)
            {
                StandCooldown -= 0.01f;
            }
            base.Update();
        }
        public virtual void FirstDiskExplode()
        {
            Level.Add(new ExplosionPart(disk1.position.x, disk1.position.y, true));
            int num = 6;
            if (Graphics.effectsLevel < 2)
            {
                num = 3;
            }
            for (int i = 0; i < num; i++)
            {
                float dir = (float)i * 60f + Rando.Float(-10f, 10f);
                float dist = Rando.Float(20f, 20f);
                ExplosionPart ins = new ExplosionPart(disk1.position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * (double)dist), disk1.position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * (double)dist), true);
                Level.Add(ins);
            }
            Graphics.FlashScreen();
            SFX.Play("explode", 1f, 0f, 0f, false);
            if (base.isServerForObject)
            {
                for (int i = 0; i < 20; i++)
                {
                    float dir = (float)i * 18f - 5f + Rando.Float(10f);
                    ATShrapnel shrap = new ATShrapnel();
                    shrap.range = 20f + Rando.Float(8f);
                    Bullet bullet = new Bullet(disk1.position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), disk1.position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                    Level.Add(bullet);
                    firedBullets.Add(bullet);
                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                }
            }
            if (isServerForObject == true)
                disk1delete = true;
            else
            {
                Level.Remove(disk1);
                disk1 = null;
            }
            disk1Setted = false;
        }
        public virtual void SecondDiskExplode()
        {
            Level.Add(new ExplosionPart(disk2.position.x, disk2.position.y, true));
            int num = 6;
            if (Graphics.effectsLevel < 2)
            {
                num = 3;
            }
            for (int i = 0; i < num; i++)
            {
                float dir = (float)i * 60f + Rando.Float(-10f, 10f);
                float dist = Rando.Float(20f, 20f);
                ExplosionPart ins = new ExplosionPart(disk2.position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * (double)dist), disk2.position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * (double)dist), true);
                Level.Add(ins);
            }
            Graphics.FlashScreen();
            SFX.Play("explode", 1f, 0f, 0f, false);
            if (base.isServerForObject)
            {
                for (int i = 0; i < 20; i++)
                {
                    float dir = (float)i * 18f - 5f + Rando.Float(10f);
                    ATShrapnel shrap = new ATShrapnel();
                    shrap.range = 20f + Rando.Float(8f);
                    Bullet bullet = new Bullet(disk2.position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), disk2.position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                    Level.Add(bullet);
                    firedBullets.Add(bullet);
                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                }
            }
            if (isServerForObject == true)
                disk2delete = true;
            else
            {
                Level.Remove(disk2);
                disk2 = null;
            }
            disk2Setted = false;
        }
        public virtual void ThirdDiskExplode()
        {
            Level.Add(new ExplosionPart(disk3.position.x, disk3.position.y, true));
            int num = 6;
            if (Graphics.effectsLevel < 2)
            {
                num = 3;
            }
            for (int i = 0; i < num; i++)
            {
                float dir = (float)i * 60f + Rando.Float(-10f, 10f);
                float dist = Rando.Float(20f, 20f);
                ExplosionPart ins = new ExplosionPart(disk3.position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * (double)dist), disk3.position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * (double)dist), true);
                Level.Add(ins);
            }
            Graphics.FlashScreen();
            SFX.Play("explode", 1f, 0f, 0f, false);
            if (base.isServerForObject)
            {
                for (int i = 0; i < 20; i++)
                {
                    float dir = (float)i * 18f - 5f + Rando.Float(10f);
                    ATShrapnel shrap = new ATShrapnel();
                    shrap.range = 20f + Rando.Float(8f);
                    Bullet bullet = new Bullet(disk3.position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), disk3.position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                    Level.Add(bullet);
                    firedBullets.Add(bullet);
                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                }
            }
            if (isServerForObject == true)
                disk3delete = true;
            else
            {
                Level.Remove(disk3);
                disk3 = null;
            }
            disk3Setted = false;
        }
    }
}

