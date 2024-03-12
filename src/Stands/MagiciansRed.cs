using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class MagiciansR : Stand, IDrawToDifferentLayers
    {
        private float StandCooldown = 0f;
        private bool overheating = false;
        private float heating;

        public float overheatTime = 3;
        public float passiveRestoration = 0.025f;
        public float passiveRestorationPerc = 0.15f;

        public bool isDown;

        public MagiciansR(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 9;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 9;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            _editorName = "Magicians Red";
            standName = "MR";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(255, 63, 0);
        }
        public override void OnHoldMove()
        {
            base.OnHoldMove();
            if (owner != null)
            {
                if (overheating == false)
                {
                    Level.Add(new FireParticle(Offset(barrelOffset).x, Offset(barrelOffset).y, this, (offDir > 0 ? angleDegrees : angleDegrees + 180f) + Rando.Float(-12f, 12f)));
                    heating += 0.01666666f;
                    if (_equippedDuck.onFire)
                    {
                        _equippedDuck.onFire = false;
                    }
                    isDown = true;
                }
                else if (overheating == true)
                {
                    return;
                }
            }
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
                }
                if (heating > overheatTime && overheating == false)
                {
                    overheating = true;
                }
                else if (heating <= 0f && overheating == true)
                {
                    overheating = false;
                }
                if (heating > 0f && overheating == false && !isDown)
                {
                    heating -= passiveRestoration * passiveRestorationPerc;
                }
            }
        }
        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Foreground)
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
                        Graphics.Draw(_pix, _equippedDuck.position.x + distance * (float)Math.Cos(angl), position.y + distance * (float)Math.Sin(angl));
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
