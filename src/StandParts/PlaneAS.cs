using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class PlaneAS : PhysicsObject
    {
        private float _maxSpeed = 4.75f;
        public bool moveLeft;
        public bool moveRight;
        public bool moveUp;
        public bool moveDown;
        public bool fire;
        public float _idleSpeed;
        public float _Spin;
        public SpriteMap _sprite;
        public Duck ownerDuck;

        public int stunFrames;

        public StateBinding _leftBind = new StateBinding("moveLeft", -1, false, false);
        public StateBinding _rightBind = new StateBinding("moveRight", -1, false, false);
        public StateBinding _upBind = new StateBinding("moveUp", -1, false, false);
        public StateBinding _downBind = new StateBinding("moveDown", -1, false, false);
        public StateBinding _fireBind = new StateBinding("fire", -1, false, false);
        public StateBinding _spinBind = new StateBinding("_Spin", -1, false, false);
        public StateBinding _ownerBind = new StateBinding("ownerDuck", -1, false, false);

        public PlaneAS(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Stands/AerosmithPlane.png"), 20, 14, false);
            base.graphic = _sprite;
            base.depth = -0.5f;
            weight = 0f;
            center = new Vec2(10f, 7f);
            collisionSize = new Vec2(20, 14f);
            collisionOffset = new Vec2(-10f, -7f);
            gravMultiplier = 0;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (bullet != null)
            {
                stunFrames = 30;
                SFX.Play("ting", 1f, 0f, 0f, false);
                if(ownerDuck  != null)
                {
                    position = ownerDuck.position;
                }
            }
            return base.Hit(bullet, hitPos);
        }

        public override void Update()
        {
            if (stunFrames > 0)
            {
                hSpeed *= 0.1f;
                stunFrames--;
            }
            if(Level.CheckRect<ForceWave>(topLeft, bottomRight) != null)
            {
                stunFrames = 30;
                SFX.Play("ting", 1f, 0f, 0f, false);
                if (ownerDuck != null)
                {
                    position = ownerDuck.position;
                }
            }
            if (Level.CheckRect<StandForceWave>(topLeft, bottomRight) != null)
            {
                stunFrames = 30;
                SFX.Play("ting", 1f, 0f, 0f, false);
                if (ownerDuck != null)
                {
                    position = ownerDuck.position;
                }
            }
            base.Update();
            base.angleDegrees = _Spin;
            _Spin %= 360f;
            if (fire)
            {
                float dir = 0f;
                if (offDir > 0)
                    dir = 360-angleDegrees-30f;
                else
                    dir = 180f-angleDegrees+30f;
                List<Bullet> firedBullets = new List<Bullet>();
                ATPlasmaBlaster shrap = new ATPlasmaBlaster();
                shrap.accuracy = 0.75f;
                for (int i = 0; i < 2; i++)
                {
                    Bullet bullet = new Bullet(position.x + (float)(Math.Cos((double)Maths.DegToRad(dir)) * 6.0), position.y - (float)(Math.Sin((double)Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);
                    Level.Add(bullet);
                    SFX.Play("plasmaFire", 1f, 1f, 0f, false);
                    firedBullets.Add(bullet);
                }
                if (Network.isActive)
                {
                    NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                    firedBullets.Clear();
                }

            }
            if (position.y > Level.current.lowestPoint + 450f)
            {
                position = ownerDuck.position;
            }
            if (moveRight)
            {
                if (hSpeed < _maxSpeed && hSpeed < 0f)
                {
                    hSpeed = -hSpeed * 0.05f;
                }
                if (hSpeed < _maxSpeed)
                {
                    hSpeed += 0.4f;
                }
                else
                {
                    hSpeed = _maxSpeed;
                }
                offDir = 1;
                _Spin = Lerp.Float(_Spin, 0f, 6f);
            }
            if (moveLeft)
            {
                if (hSpeed > -_maxSpeed && hSpeed > 0f)
                {
                    hSpeed = -hSpeed * 0.05f;
                }
                if (hSpeed > -_maxSpeed)
                {
                    hSpeed -= 0.4f;
                }
                else
                {
                    hSpeed = -_maxSpeed;
                }
                offDir = -1;
                _Spin = Lerp.Float(_Spin, 0f, 6f);
            }
            if (moveDown)
            {
                if (vSpeed < _maxSpeed && vSpeed < 0f)
                {
                    vSpeed = -vSpeed * 0.05f;
                }
                if (vSpeed < _maxSpeed)
                {
                    vSpeed += 0.4f;
                }
                else
                {
                    vSpeed = _maxSpeed;
                }
                if (offDir > 0)
                    _Spin = Lerp.Float(_Spin, 90f, 12f);
                else
                    _Spin = Lerp.Float(_Spin, -90f, 12f);
            }
            if (moveUp)
            {
                if (vSpeed > -_maxSpeed && vSpeed > 0f)
                {
                    vSpeed = -vSpeed * 0.05f;
                }
                if (vSpeed > -_maxSpeed)
                {
                    vSpeed -= 0.4f;
                }
                else
                {
                    vSpeed = -_maxSpeed;
                }
                if (offDir < 0)
                    _Spin = Lerp.Float(_Spin, 90f, 12f);
                else
                    _Spin = Lerp.Float(_Spin, -90f, 12f);
            }
        }
    }
}
