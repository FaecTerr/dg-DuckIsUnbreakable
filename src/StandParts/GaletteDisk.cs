using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class GaletteDisk : Holdable
    {
        private SpriteMap _sprite;
        private Duck Own;
        public List<Bullet> firedBullets = new List<Bullet>();

        public GaletteDisk(float xval, float yval, Duck own) : base(xval, yval)
        {
            Own = own;
            center = new Vec2(10f, 2f);
            collisionOffset = new Vec2(-10f, -2f);
            collisionSize = new Vec2(20f, 4f);
            _sprite = new SpriteMap(GetPath("Sprites/Stands/GaletteDisk"), 20, 4, false);
            thickness = 0.1f;
            _equippedDepth = 4;
            base.graphic = _sprite;
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            //base.material = new MaterialGlitch(this);
            weight = 0f;
        }
        public override void Update()
        {
            base.Update();
            Duck d = Level.CheckLine<Duck>(position, position - new Vec2(0f, 8f), Own);
            if(d != null && owner == null)
            {
                 Explode();      
            }
        }
        public virtual void Explode()
        {
            Level.Add(new ExplosionPart(position.x, position.y, true));
            int num = 6;
            if (Graphics.effectsLevel < 2)
            {
                num = 3;
            }
            for (int i = 0; i < num; i++)
            {
                float dir = (float)i * 60f + Rando.Float(-10f, 10f);
                float dist = Rando.Float(20f, 20f);
                ExplosionPart ins = new ExplosionPart(position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * (double)dist), position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * (double)dist), true);
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
                    Bullet bullet = new Bullet(position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
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
        }
    }
}
