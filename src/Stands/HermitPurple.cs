using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class HermitPurple : Stand
    {
        protected Vec2 _wallPoint = default(Vec2);
        protected Sprite _sightHit;
        private Tex2D _laserTex;
        private float StandCooldown = 0f;
        private float StandHeat = 0f;
        private bool overheat = false;
        private int _lagFrames;
        protected Vec2 _grappleTravel = default(Vec2);
        private Vec2 _lastHit = Vec2.Zero;
        private float floatMod;
        public bool _canGrab;

        public HermitPurple(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 6;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 6;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            base.graphic = _sprite;
            _editorName = "Hermit Purple";
            standName = "HP";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            _laserTex = Content.Load<Tex2D>("pointerLaser");
            standColor = new Vec3(40, 0, 153);
        }
        
        public override void OnHoldMove()
        {
            base.OnHoldMove();

        }

        public override void OnStandMove()
        {
            base.OnStandMove();
            floatMod = 1f;
            if(_canGrab == true && StandCooldown <= 0f && overheat == false)
                DoGrab();
        }

        public void DoGrab()
        {
            //owner.velocity = new Vec2(offDir*(16f + StandoPower*2.5f), offDir*(24f + StandoPower*3f));
            StandCooldown = 0.4f;
            owner.hSpeed -= (owner.position.x - _wallPoint.x) * floatMod *0.1f;
            owner.vSpeed -= (owner.position.y - _wallPoint.y) * floatMod * 0.5f;
            PhysicsObject obj = Level.CheckCircle<PhysicsObject>(_wallPoint, 1f);
            if (obj != null)
            {
                obj.vSpeed += (owner.position.y - _wallPoint.y) * floatMod * 0.25f;
                obj.hSpeed += (owner.position.x - _wallPoint.x) * floatMod * 0.05f;
            }
            Duck d = Level.CheckCircle<Duck>(_wallPoint, 8f);
            if (d != null)
            {
                d.vSpeed -= -(owner.position.y - _wallPoint.y) * floatMod * 0.4f;
                d.hSpeed -= -(owner.position.x - _wallPoint.x) * floatMod * 0.08f;
                if (d.holdObject != null)
                {
                    d.doThrow = true;
                }
            }
            if (owner.position.y > Level.current.lowestPoint - 80f)
            {
                StandHeat += 1.5f;
            }
        }

        public override void OnSpecialMove()
        {
            OnStandMove();
            floatMod = 5f;
            base.OnSpecialMove();
        }
        public override void OnSpecialMoveFast()
        {
            OnStandMove();
            floatMod = 3f;
            base.OnSpecialMoveFast();
        }
        public override void OnSpecialMoveLong()
        {
            OnStandMove();
            floatMod = 7f;
            base.OnSpecialMoveLong();
        }


        public override void Update()
        {
            base.Update();
            if (StandCooldown > 0f)
                StandCooldown -= 0.01f;
            if (StandHeat > 0f)
                StandHeat -= 0.01f;
            if (StandHeat <= 0f && overheat == true)
                overheat = false;
            Vec2 pos = Offset(barrelOffset);
            ATTracer tracer = new ATTracer();
            float dist = tracer.range = 160f + StandoPower*25;
            tracer.penetration = 0f;
            if (StandHeat > 4f)
                overheat = true;
            Duck d = owner as Duck;
            float a = 35f;
            if (offDir < 0)
            {
                a = 145f;
            }
            if (owner != null)
            {
                if (duck.grounded)
                {
                    a = 0f;
                    if (offDir < 0)
                    {
                        a = 180f;
                    }
                }
            }
            Bullet b = new Bullet(pos.x+16f*offDir, pos.y-2f, tracer, a, owner, false, -1f, true, true);
            _wallPoint = b.end;
            _grappleTravel = b.travelDirNormalized;
            dist = (pos - _wallPoint).length;
            if (dist < 160f + StandoPower*25)
            {
                _lastHit = _wallPoint;
                _canGrab = true;
            }
            else if (_lagFrames == 0)
            {
                _lagFrames = 6;
                _wallPoint = _lastHit;
            }
            else
                _canGrab = false;
        }
        public override void DrawGlow()
        {
            if (_equippedDuck != null)
            {
                Graphics.DrawTexturedLine(_laserTex, position, _wallPoint, Color.Magenta, 0.5f, base.depth - 1);
                if (_sightHit != null && _canGrab == true)
                {
                    _sightHit.color = Color.Magenta;
                    Graphics.Draw(_sightHit, _wallPoint.x, _wallPoint.y);
                }
            }
            base.DrawGlow();
        }
    } 
}
