using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class KillerQueen : Stand
    {
        private float counter;
        private float SFXplay;
        private bool bombSet;
        private float StandCooldown = 0f;
        private Holdable g;
        private Holdable touchedObject;
        public Duck duckOwner;

        public SpriteMap _mark = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Stands/KQMark.png"), 15, 15);
        
        public float[] posArrayX = new float[280];
        public float[] posArrayY = new float[280];
        public bool init = false;
        public int INTcounter = 0;
        public List<Bullet> firedBullets = new List<Bullet>();
        public NetSoundEffect _netLoad1 = new NetSoundEffect(new string[]
{
            GetPath<DuckUnbreakable>("SFX/Click.wav"),
});

        public KillerQueen(float xval, float yval) : base(xval, yval)
        {
            _mark.CenterOrigin();
            _pickupSprite.frame = 8;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 8;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            _editorName = "Killer Queen";
            standName = "KQ";
            standColor = new Vec3(225, 0, 220);

            usingCharging = true;
        }

        public override void OnSpecialMoveFast()
        {
            if (StandCooldown <= 0f)
            {
                SFX.Play(GetPath("SFX/AOBTD.wav"), 1f, 0f, 0f, false);
                BitesTheDust();
                StandCooldown = 5f;
            }
        }
        public override void OnSpecialMoveLong()
        {
            OnSpecialMoveFast();
            base.OnSpecialMoveLong();

        }
        public override void OnSpecialMove()
        {
            OnSpecialMoveFast();
            base.OnSpecialMove();
        }
        public override void OnStandMove()
        {

            if (duck.holdObject != null && bombSet == false && duck.holdObject != g)
            {
                g = Activator.CreateInstance(duck.holdObject.GetType(), Editor.GetConstructorParameters(duck.holdObject.GetType())) as Holdable;
                touchedObject = duck.holdObject; 
                bombSet = true;
            }
            else if (bombSet == true && duck.holdObject != g)
            {
                FirstBombExplode();
                SFX.Play(GetPath("SFX/Click.wav"), 1f, 0f, 0f, false);
                bombSet = false;
            }
            base.OnStandMove();
        }
        public virtual void FirstBombExplode()
        {
            Level.Add(new ExplosionPart(g.position.x, g.position.y, true));
            int num = 6;
            if (Graphics.effectsLevel < 2)
            {
                num = 3;
            }
            for (int i = 0; i < num; i++)
            {
                float dir = (float)i * 60f + Rando.Float(-10f, 10f);
                float dist = Rando.Float(20f, 20f);
                ExplosionPart ins = new ExplosionPart(g.position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * (double)dist), g.position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * (double)dist), true);
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
                    Bullet bullet = new Bullet(g.position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), g.position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                    Level.Add(bullet);
                    firedBullets.Add(bullet);
                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                    Level.Remove(g);
                }
            }

        }
        public override void Update()
        {
            //duckOwner = owner as Duck;
            if (bombSet == true)
            {
                g.position = touchedObject.position;
            }
            if (_equippedDuck != null && !destroyed)
            {
                duckOwner = owner as Duck;
                if (INTcounter == 280)
                {
                    INTcounter = 0;
                    init = true;
                }
                updateBTD();
            }
            if (StandCooldown > 0f)
            {
                StandCooldown -= 0.02f;
            }
            base.Update();
        }

        public void BitesTheDust()
        {
            if (init == false)
            {
                duckOwner.position.x = posArrayX[0];
                duckOwner.position.y = posArrayY[0];
            }
            else
            {
                if (INTcounter == 279)
                {
                    duckOwner.position.x = posArrayX[0];
                    duckOwner.position.y = posArrayY[0];
                }
                else if (INTcounter < 279)
                {
                    duckOwner.position.x = posArrayX[INTcounter + 1];
                    duckOwner.position.y = posArrayY[INTcounter + 1];
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (duckOwner != null)
            {
                Vec2 SkullPosition = new Vec2(0, 0);
                if (init == false)
                {
                    SkullPosition.x = posArrayX[0];
                    SkullPosition.y = posArrayY[0];
                }
                else
                {
                    if (INTcounter == 279)
                    {
                        SkullPosition.x = posArrayX[0];
                        SkullPosition.y = posArrayY[0];
                    }
                    else if(INTcounter < 279)
                    {
                        SkullPosition.x = posArrayX[INTcounter + 1];
                        SkullPosition.y = posArrayY[INTcounter + 1];
                    }
                }
                Graphics.Draw(_mark, SkullPosition.x, SkullPosition.y);
            }
        }

        public void updateBTD()
        {
            posArrayX[INTcounter] = duckOwner.position.x;
            posArrayY[INTcounter] = duckOwner.position.y;
            INTcounter++;
        }
    }
}