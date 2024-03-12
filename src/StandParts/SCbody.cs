using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class SCbody : PhysicsObject, IReflect
    {
        private float _maxSpeed = 2.75f;
        public bool moveLeft;
        public bool moveRight;
        public bool jump;
        public bool crouching;
        public bool fire;

        public int stunFrames;
        public int health = 3;

        public string mode = "normal";
        public float _idleSpeed;
        private int skipFrames;
        private int jumpFrames;
        private bool crouchLock;
        public SpriteMap _sprite;
        public Duck ownerDuck;
        public SCsword _scs;
        public StateBinding _standBind = new StateBinding("_scs", -1, false, false);
        public StateBinding _leftBind = new StateBinding("moveLeft", -1, false, false);
        public StateBinding _rightBind = new StateBinding("moveRight", -1, false, false);
        public StateBinding _jumpBind = new StateBinding("jump", -1, false, false);
        public StateBinding _crouchBind = new StateBinding("crouching", -1, false, false);
        public StateBinding _fireBind = new StateBinding("fire", -1, false, false);
        public StateBinding _modeBind = new StateBinding("mode", -1, false, false);
        public StateBinding _ownerBind = new StateBinding("ownerDuck", -1, false, false);

        public SCbody(float xpos, float ypos, SCsword scsword) : base(xpos, ypos)
        {
            thickness = 0.4f;
            _scs = scsword;
            _sprite = new SpriteMap(GetPath("Sprites/Stands/SilverChariotBody.png"), 32, 32, false); 
            base.graphic = _sprite;
            base.depth = -0.5f;
            weight = 0f;
            base.centerx = 16f;
            base.centery = 16f;
            flammable = 1f;
            duck = true;
            friction = 0.1f;
            _sprite.AddAnimation("idle", 1f, true, new int[]
            {
                0,
            });
            _sprite.AddAnimation("run", 0.2f, true, new int[]
            {
                1,
                2,
                3,
                4,
                5,
                6
            });
            _sprite.AddAnimation("jump", 1f, true, new int[]
            {
                7
            });
            _sprite.AddAnimation("slide", 1f, true, new int[]
            {
                8
            });
            _sprite.AddAnimation("crouch", 1f, true, new int[]
            {
                11
            });
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if(bullet != null)
            {
                stunFrames = 30;
                SFX.Play("ting", 1f, 0f, 0f, false);
                health--;
            }
            return base.Hit(bullet, hitPos);
        }

        public override void Update()
        {
            base.Update();
            if(stunFrames > 0)
            {
                hSpeed *= 0.1f;
                stunFrames--;
            }
            if (Level.CheckRect<ForceWave>(topLeft, bottomRight) != null)
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
            if (ownerDuck != null)
            {
                if(health <= 0)
                {
                    position = ownerDuck.position;
                    health = 3;
                }
                if((new Vec2(ownerDuck.position - position)).length > 80 && health > 2)
                {
                    health = 2;
                }
                if ((new Vec2(ownerDuck.position - position)).length > 160 && health > 1)
                {
                    health = 1;
                }
                if ((new Vec2(ownerDuck.position - position)).length < 8 && health < 3)
                {
                    health = 3;
                }
                if (ownerDuck.dead)
                {
                    Level.Remove(this);
                }
                else if (position.y > Level.current.lowestPoint + 499f)
                {
                    position = ownerDuck.position;
                }
                else if (destroyed)
                {
                    ownerDuck.Kill(new DTImpact(this));
                }
            }
            if (position.y > Level.current.lowestPoint + 499f && ownerDuck == null)
            {
                Level.Remove(this);
            }
            foreach (Door d in Level.CheckLineAll<Door>(position + new Vec2(-10f, 0f), position + new Vec2(10f, 0f)))
            {
                if (d != null && !(d is YDoor) && !d.locked)
                {
                    if (position.x > d.position.x)
                    {
                        d._open = 2f;
                        d._openForce = 2f;
                    }
                    else
                    {
                        d._open = 2f;
                        d._openForce = 2f;
                    }
                }
            }
            bool canJump = jumpFrames > 0 && !crouchLock;
            crouchLock = ((mode == "crouch" || mode == "slide") && Level.CheckRect<Block>(new Vec2(base.x - 3f, base.y - 9f), new Vec2(base.x + 3f, base.y), null) != null);
            skipFrames = Maths.CountDown(skipFrames, 1, 0);
            if (jump == true)
            {
                jumpFrames = 4;
                canJump = true;
                if (crouching == true && base.grounded && !(Level.CheckRect<Block>(new Vec2(base.x - 3f, base.y + 9f), new Vec2(base.x + 3f, base.y), null) != null))
                {
                    skipFrames = 10;
                    velocity = new Vec2(0f, 1f);
                    canJump = false;
                }
            }
            else
            {
                jumpFrames = Maths.CountDown(jumpFrames, 1, 0);
            }
            bool nudging = crouchLock && base.grounded && (crouching == true || moveLeft == true || moveRight == true || jump == true || fire == true);
            if (nudging && offDir == -1)
            {
                hSpeed = -1f;
            }
            if (nudging && offDir == 1)
            {
                hSpeed = 1f;
            }
            _skipPlatforms = false;
            if (skipFrames > 0 && crouching == true)
            {
                _skipPlatforms = true;
            }
            if (jump && base.grounded && canJump == true && !nudging && jumpFrames > 0)
            {
                vSpeed -= 4.9f;
            }
            if (mode == "normal")
            {
                collisionSize = new Vec2(8f, 22f);
                collisionOffset = new Vec2(-4f, -7f);
            }
            if (mode == "slide")
            {
                collisionSize = new Vec2(8f, 11f);
                collisionOffset = new Vec2(-4f, 4f);
            }
            if (mode == "crouch")
            {
                collisionSize = new Vec2(8f, 16f);
                collisionOffset = new Vec2(-4f, -1f);
            }
            if (crouching && (hSpeed > 0.1f || hSpeed < -0.1f) && mode == "crouch" && grounded)
            {
                mode = "slide";
            }
            else if ((crouching || crouchLock == true) && mode != "slide")
            {
                mode = "crouch";
            }
            else if (!crouching && crouchLock == false)
            {
                mode = "normal";
            }
            if (mode == "normal" && (vSpeed > 0.01f || vSpeed < -0.01f))
                _sprite.SetAnimation("jump");
            else if (mode == "normal" && hSpeed <= 0.01f && hSpeed >= -0.01f)
                _sprite.SetAnimation("idle");
            else if (mode == "normal" && _sprite.currentAnimation != "run" && (hSpeed > 0.01f || hSpeed < 0.01f))
                _sprite.SetAnimation("run");
            else if (mode == "crouch")
                _sprite.SetAnimation("crouch");
            else if (mode == "slide")
                _sprite.SetAnimation("slide");
            if (moveLeft && mode == "normal")
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
                _idleSpeed += 1f;
            }
            if (moveRight && mode == "normal")
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
                _idleSpeed += 1f;
            }
            if (!moveLeft && !moveRight)
            {
                _idleSpeed -= 1f;
                float mod = 1f;
                if (mode == "normal")
                    mod = 0.6666666f;
                else if (mode == "crouch")
                    mod = 0.999999f;
                else if (mode == "slide")
                    mod = 0.999999f;
                if (hSpeed < -0.01f)
                {
                    hSpeed *= mod;
                }
                else if (hSpeed > 0.01f)
                {
                    hSpeed *= mod;
                }
            }
            if (_idleSpeed > 1f)
            {
                _idleSpeed = 1f;
            }
            if (_idleSpeed < 0f)
            {
                _idleSpeed = 0f;
            }
            if (_scs == null)
            {
                _scs = new SCsword(base.x, base.y);
                Level.Add(_scs);
                _scs.owner = this;
                _scs.anchor = this;
            }
            _scs.onPress = fire;
        }
    }
}
