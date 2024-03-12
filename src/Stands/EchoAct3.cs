using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class EchoAct3 : Stand, IDrawToDifferentLayers
    {
        private bool overheating = false;
        private float heating;

        public float overheatTime = 8;
        public float passiveRestoration = 0.017f;
        public float passiveRestorationPerc = 0.45f;

        public bool isDown;
        public float radius = 96;

        public NetSoundEffect _netLoad1 = new NetSoundEffect(new string[]
{
            GetPath<DuckUnbreakable>("SFX/TreeFreeze.wav"),
});
        public NetSoundEffect _netLoad2 = new NetSoundEffect(new string[]
{
            GetPath<DuckUnbreakable>("SFX/HardItem.wav"),
});
        public EchoAct3(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 3;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 3;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            _editorName = "Echoes Act III";
            standName = "EA";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(255, 242, 0);
        }
        public override void OnHoldMove()
        {
            base.OnHoldMove();
            if (owner != null)
            {
                if (overheating == false)
                {
                    if (heating > 0.01f && heating < 0.03f)
                    {
                        SFX.Play(GetPath("SFX/TreeFreeze.wav"), 1f, 0f, 0f, false);
                    }
                    float range = radius;
                    foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(position, range))
                    {
                        Vec2 pos = duck.position;
                        if (p.active)
                        {
                            if (isServerForObject && !(p is Duck) && (!(p is Holdable) || ((p as Holdable).duck == null && (p as Holdable).equippedDuck == null)))
                            {
                                Fondle(p);
                            }
                            if (p != duck)
                            {
                                p.velocity = new Vec2(p.velocity.x*0.8f, p.velocity.y < 0 ? p.velocity.y * 0.1f : p.velocity.y*5f);
                            }
                            if (p.framesSinceGrounded == 1)
                            {
                                SFX.Play(GetPath("SFX/HardItem.wav"), 1f, 0f, 0f, false);
                            }
                            if(p is Duck)
                            {
                                (p as Duck).CancelFlapping();
                            }
                        }
                    }
                    heating += 0.01666666f;
                }
            }
            isDown = true;
        }
        public override void Update()
        {
            isDown = false;
            base.Update();
            if (owner != null)
            {
                if (overheating == true)
                {
                    heating -= passiveRestoration;
                    OnStandMove();
                }
                if (heating > 0f && overheating == false && !isDown)
                {
                    heating -= passiveRestoration * passiveRestorationPerc;
                }
                if (heating > overheatTime && overheating == false)
                {
                    overheating = true;
                }
                else if (heating <= 0f && overheating == true)
                {
                    overheating = false;
                }
            }
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Foreground)
            {
                if (_equippedDuck != null && _equippedDuck.profile.localPlayer) 
                {
                    Sprite _pix = new Sprite(GetPath("Sprites/Things/OneWhitePixElement.png"));
                    _pix.CenterOrigin();

                    int iterations = 100;

                    float distance = 18;
                    Color c = Color.White;
                    if (isDown)
                    {
                        c = Color.Gray;

                        if (Rando.Float(1) > 0.5f && heating < overheatTime && !overheating)
                        {
                            float len = Rando.Float(radius);
                            float ang = Rando.Float(0, 360);
                            float angl = Maths.DegToRad(ang);

                            float x1 = len * (float)Math.Cos(angl);
                            float y1 = len * (float)Math.Sin(angl);
                            Level.Add(new thickAirPushed(position.x + x1, position.y + y1));
                            Level.Add(new thickAirPushed(position.x - x1, position.y - y1));
                        }
                    }
                    if (overheating)
                    {
                        c = Color.OrangeRed;
                    }

                    //First circle
                    for (int i = 0; i < iterations; i++)
                    {
                        float ang = i * (360 / iterations);
                        float angl = Maths.DegToRad(ang);
                        _pix.scale = new Vec2(1, 2.5f);
                        _pix.color = ((c.ToVector3() + new Vec3(-20, -20, -20)).ToColor());
                        _pix.depth = 1.02f;
                        _pix.angleDegrees = ang;
                        Graphics.Draw(_pix, position.x + distance * (float)Math.Cos(angl), position.y + distance * (float)Math.Sin(angl));
                    }
                    //Seconds circle
                    for (int i = 0; i < (1 - heating / overheatTime) * iterations; i++)
                    {
                        float ang = i * (360 / iterations);
                        float angl = Maths.DegToRad(ang);
                        _pix.scale = new Vec2(1, 1.5f);
                        _pix.color = c;
                        _pix.depth = 1.03f;
                        _pix.angleDegrees = ang;
                        Graphics.Draw(_pix, position.x + distance * (float)Math.Cos(angl), position.y + distance * (float)Math.Sin(angl));
                    }
                }
            }
        }
    }
}
