using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class Aerosmith : Stand
    {
        public PlaneAS _plane;
        public bool PlaneCreated = false;
        public bool _pressed;
        private bool activate = false;
        public Sprite eyeRadar;
        public StateBinding _planeBind = new StateBinding("_plane", -1, false, false);
        public NetSoundEffect _netLoad1 = new NetSoundEffect(new string[]
{
            GetPath<DuckUnbreakable>("SFX/Aerosmith.wav"),
});

        public Aerosmith(float xval, float yval, PlaneAS plane) : base(xval, yval)
        {
            _pickupSprite.frame = 1;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 1;
            thickness = 0.4f;
            _equippedDepth = 4;
            _punchForce = 0f;
            standName = "AER";
            eyeRadar = new Sprite(GetPath("Sprites/Stands/EyeRadar"), 0f, 0f);
            eyeRadar.center = new Vec2(8, 8);
            _plane = plane;
            standColor = new Vec3(204, 40, 40);
        }
        public override void OnNothing()
        {
            base.OnNothing();
            if (PlaneCreated == true)
            {
                duck.immobilized = false;
                _plane.hSpeed = 0f;
                _plane.vSpeed = 0f;
                FollowCam followCam = Level.current.camera as FollowCam;
                if (followCam != null)
                {
                    followCam.Remove(_plane);
                }
            }
        }
        public override void OnHoldMove()
        {
            base.OnHoldMove();
            if ((PlaneCreated == false || _plane.destroyed) && base.isServerForObject)
            {
                _plane = new PlaneAS(base.x, base.y);
                PlaneCreated = true;
                Level.Add(_plane);
                SFX.Play(GetPath("SFX/Aerosmith.wav"), 1f, 0f, 0f, false);
                Duck duck = owner as Duck;
                _plane.ownerDuck = duck;
            }
            else if (PlaneCreated == true)
            {
                Duck duck = owner as Duck;
                if (duck != null)
                {
                    _plane.ownerDuck = duck;      
                    duck.immobilized = true;
                    _plane.moveLeft = (_plane.moveRight = (_plane.moveUp = false));
                    FollowCam followCam = Level.current.camera as FollowCam;
                    if (followCam != null)
                    {
                        followCam.Add(_plane);
                    }
                    _plane.moveLeft = duck.inputProfile.Down("LEFT");
                    _plane.moveRight = duck.inputProfile.Down("RIGHT");
                    _plane.moveUp = (duck.inputProfile.Down("JUMP") || duck.inputProfile.Down("UP"));
                    _plane.moveDown = duck.inputProfile.Down("DOWN");
                    _plane.fire = duck.inputProfile.Pressed("SHOOT", false);
                }
            }
        }
        /*public override void Update()
        {
            base.Update();
        }*/
        public override void Draw()
        {
            if (_equippedDuck != null)
            {
                Graphics.Draw(eyeRadar, _equippedDuck.x - 3f * offDir + 2f, y - 12f, 0.7f);
            }
            base.Draw();
        }
    }
}
