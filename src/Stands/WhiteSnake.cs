using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class WhiteSnake : Stand
    {  
        public WSbody _ws;
        public bool WScreated = false;
        public bool _pressed;
        private bool activate = false;

        public WhiteSnake(float xval, float yval, WSbody ws = null) : base(xval, yval)
        {
            _pickupSprite.frame = 18;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _sprite.frame = 18;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            _editorName = "Whitesnake";
            standName = "WS";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(255, 178, 127);
            _ws = ws;
        }
        public override void OnHoldMove()
        {
            base.OnHoldMove();
            if (WScreated == false && isServerForObject)
            {
                _ws = new WSbody(base.x, base.y);
                WScreated = true;
                Level.Add(_ws);
                //SFX.Play(GetPath("SFX/SilvarChariot.wav"), 1f, 0f, 0f, false);
            }
            else if (WScreated == true)
            {
                Duck duck = owner as Duck;
                if (duck != null)
                {
                    duck.immobilized = true;
                    _ws.moveLeft = (_ws.moveRight = (_ws.jump = false));
                    FollowCam followCam = Level.current.camera as FollowCam;
                    if (followCam != null)
                    {
                        followCam.Add(_ws);
                    }
                }
            }
        }
        public override void OnNothing()
        {
            base.OnNothing();
            if (WScreated == true)
            {
                duck.immobilized = false;
                FollowCam followCam = Level.current.camera as FollowCam;
                if (followCam != null)
                {
                    followCam.Remove(_ws);
                }
            }
        }
        public override void Update()
        {
            base.Update();
            Duck duck = owner as Duck;
            if (WScreated == true && _ws != null)
            {
                if (owner != null)
                {
                    _ws.ownerDuck = duck;
                }
                else
                    _ws.ownerDuck = null;
            }
            if (_ws != null && duck != null && owner != null)
            {
                Duck d = owner as Duck;
                if (d.HasEquipment(this))
                {
                    _ws.moveLeft = duck.inputProfile.Down("LEFT");
                    _ws.moveRight = duck.inputProfile.Down("RIGHT");
                    _ws.jump = duck.inputProfile.Pressed("JUMP", false);
                    _ws.fire = duck.inputProfile.Down("SHOOT");
                    _ws.crouching = duck.inputProfile.Down("DOWN");
                }
            }
        }
        /*public override void Update()
        {
            base.Update();
            radius = 16f + StandoPower * 8f;
            if (StandCooldown > 0f)
            {
                StandCooldown -= 0.01666666f;
            }
        }

        public override void OnShortHold()
        {
            base.OnShortHold();
            foreach (Duck d in Level.CheckCircleAll<Duck>(position, radius))
            {
                foreach (Stand s in Level.current.things[typeof(Stand)])
                {
                    if (d != owner && s != null && StandCooldown <= 0f)
                    {
                        if (d.HasEquipment(s) && d != owner)
                        {
                            d.Unequip(s);
                            StandoPower = 0f;
                            StandCooldown = 2f;
                        }
                        else if (d != owner && d.immobilized == false && !(d.HasEquipment(typeof(Stand))))
                        {
                            d.immobilized = true;
                            StandoPower = 0f;
                            StandCooldown = 8f;
                            if (base.isServerForObject)
                            {
                                Level.Add(new MemoryDisk(d.position.x, d.position.y));
                            }
                        }
                    }
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
            if (owner != null)
            {
                Duck d = Level.CheckCircle<Duck>(owner.position, radius, owner);
                if (d != null)
                {
                    Graphics.Draw(_target, d.position.x, d.position.y);
                    _target.angleDegrees += 2;
                }
            }
             
        }*/
    }
}