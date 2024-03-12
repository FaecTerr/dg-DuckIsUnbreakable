using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class SilverChariot : Stand
    {
        public SCbody _sc;
        public bool SCcreated = false;
        public bool _pressed;
        private bool activate = false;
        public StateBinding _standBind = new StateBinding("_sc", -1, false, false);
        public NetSoundEffect _netLoad1 = new NetSoundEffect(new string[]
{
            GetPath<DuckUnbreakable>("SFX/SilvarChariot.wav"),
});

        public SilverChariot(float xval, float yval, SCbody SC = null) : base(xval, yval)
        {
            _pickupSprite.frame = 13;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _sprite.frame = 13;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            _editorName = "Silver Chariot";
            standName = "SC";
            _sc = SC;
            standColor = new Vec3(192, 192, 192);
        }
// РАБОТАЕТ, НО НЕ СТАБИЛЬНО (МЕЧ)
        public override void OnHoldMove()
        {
            base.OnHoldMove();
            if (SCcreated == false && base.isServerForObject)
            {
                _sc = new SCbody(base.x, base.y, null);
                SCcreated = true;
                Level.Add(_sc);
                SFX.Play(GetPath("SFX/SilvarChariot.wav"), 1f, 0f, 0f, false);
            }
            else if (SCcreated == true)
            {
                Duck duck = owner as Duck;
                if (duck != null)
                {
                    duck.immobilized = true;
                    _sc.moveLeft = (_sc.moveRight = (_sc.jump = false));
                    FollowCam followCam = Level.current.camera as FollowCam;
                    if (followCam != null)
                    {
                        followCam.Add(_sc);
                    }
                }
            }
        }
        public override void OnNothing()
        {
            base.OnNothing();
            if (SCcreated == true)
            {
                duck.immobilized = false;
                FollowCam followCam = Level.current.camera as FollowCam;
                if (followCam != null)
                {
                    followCam.Remove(_sc);
                }
            }
        }
        public override void Update()
        {
            base.Update();
            Duck duck = owner as Duck;
            if (SCcreated == true && _sc != null)
            {
                if (owner != null)
                {
                    _sc.ownerDuck = duck;
                }
                else
                    _sc.ownerDuck = null;
            }
            if (_sc != null && duck != null && owner != null)
            {
                Duck d = owner as Duck;
                if (d.HasEquipment(this))
                {
                    _sc.moveLeft = duck.inputProfile.Down("LEFT");
                    _sc.moveRight = duck.inputProfile.Down("RIGHT");
                    _sc.jump = duck.inputProfile.Pressed("JUMP", false);
                    _sc.fire = duck.inputProfile.Down("SHOOT");
                    _sc.crouching = duck.inputProfile.Down("DOWN");
                }
            }
        }
    }
}

