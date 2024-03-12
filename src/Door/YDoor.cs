using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class YDoor  : Door
    {
        private Sprite _bottom;
        private Sprite _top;
        private float _desiredOpen;
        private bool _opened;
        private Vec2 _topLeft;
        private Vec2 _topRight;
        private Vec2 _bottomLeft;
        private Vec2 _bottomRight;
        private bool _cornerInit;
        public bool getOpen;
        private DUDoorFrame _fram;
        private bool _fucked;
        public DUDoorOffHinges _dudoorInstance;
        public StateBinding _openBind = new StateBinding("getOpen", -1, false, false);

        public YDoor(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/KCDoors/LockedCardDoor.png"), 32, 32, false);
            graphic = _sprite;
            center = new Vec2(16f, 25f);
            collisionSize = new Vec2(6f, 32f);
            collisionOffset = new Vec2(-3f, -25f);
            base.depth = -0.5f;
            _maxHealth = 9990f;
            _hitPoints = 9990f;
            thickness = 1f;
            physicsMaterial = PhysicsMaterial.Metal;
            _bottom = new Sprite("verticalDoorBottom", 0f, 0f);
            _bottom.CenterOrigin();
            _top = new Sprite("verticalDoorTop", 0f, 0f);
            _top.CenterOrigin();
        }
        public override void Initialize()
        {
            base.sequence.isValid = objective.value;
            _lockDoor = locked;
            _fram = new DUDoorFrame(base.x, base.y - 1f);
            Level.Add(_fram);
        }
        protected override bool OnDestroy(DestroyType type = null)
        {
            if (_lockDoor || base._destroyed)
            {
                return false;
            }
            _hitPoints = 0f;
            Level.Remove(this);
            if (base.sequence != null && base.sequence.isValid)
            {
                base.sequence.Finished();
                if (ChallengeLevel.running)
                {
                    ChallengeLevel.goodiesGot++;
                }
            }
            DUDoorOffHinges door = null;
            if (Network.isActive)
            {
                if (_dudoorInstance != null)
                {
                    door = _dudoorInstance;
                    door.visible = true;
                    door.active = true;
                    door.solid = true;
                    Thing.Fondle(this, DuckNetwork.localConnection);
                    Thing.Fondle(door, DuckNetwork.localConnection);
                }
            }
            else
            {
                door = new DUDoorOffHinges(base.x, base.y - 8f);
            }
            if (door != null)
            {
                DTShot shot = type as DTShot;
                if (shot != null && shot.bullet != null)
                {
                    door.hSpeed = shot.bullet.travelDirNormalized.x * 2f;
                    door.vSpeed = shot.bullet.travelDirNormalized.y * 2f - 1f;
                    door.offDir = ((shot.bullet.travelDirNormalized.x > 0f) ? (sbyte)1 : (sbyte)-1);
                }
                else
                {
                    door.hSpeed = (float)offDir * 2f;
                    door.vSpeed = -2f;
                    door.offDir = offDir;
                }
                if (!Network.isActive)
                {
                    Level.Add(door);
                    door.MakeEffects();
                }
            }
            return true;
        }
        public override void Update()
        {
            if (!_fucked && _hitPoints < _maxHealth / 2f)
            {
                //_sprite = new SpriteMap("doorFucked", 32, 32, false);
                //graphic = _sprite;
                _fucked = true;
            }
            _maxHealth = 9990f;
            _hitPoints = 9990f;
            if (!_cornerInit)
            {
                _topLeft = base.topLeft;
                _topRight = base.topRight;
                _bottomLeft = base.bottomLeft;
                _bottomRight = base.bottomRight;
                _cornerInit = true;
            }

            if (getOpen == true)
            {
                Duck hit = Level.CheckRect<Duck>(_topLeft - new Vec2(18f, 0f), _bottomRight + new Vec2(18f, 0f), null);
                if (hit != null)
                {
                    _desiredOpen = 1f;
                }
                if (_desiredOpen > 0.5f && !_opened)
                {
                    _opened = true;
                    SFX.Play("slideDoorOpen", 0.6f, 0f, 0f, false);
                }
                if (_desiredOpen < 0.5f && _opened)
                {
                    _opened = false;
                    SFX.Play("slideDoorClose", 0.6f, 0f, 0f, false);
                }
                _open = Maths.LerpTowards(_open, _desiredOpen, 0.15f);
                _sprite.frame = (int)(_open * 32f);
                _collisionSize.y = (1f - _open) * 32f;
            }
            else
            {
                _desiredOpen = 0f;
                collisionSize = new Vec2(6f, 32f);
                collisionOffset = new Vec2(-3f, -25f);
                _opened = false;
            }
            if (_desiredOpen < 0.5f && _opened)
            {
                _opened = false;
                SFX.Play("slideDoorClose", 0.6f, 0f, 0f, false);
            }
            _open = Maths.LerpTowards(_open, _desiredOpen, 0.15f);
            _sprite.frame = (int)(_open * 32f);
            _collisionSize.y = (1f - _open) * 32f;
            if (Level.CheckRect<PhysicsObject>(new Vec2(base.x - 2f, base.y - 24f), new Vec2(base.x + 2f, base.y + 8f), this) == null)
            {
                _desiredOpen = 0f;
            }
        }

        public override void Draw()
        {
            base.Draw();
            _top.depth = base.depth + 1;
            _bottom.depth = base.depth + 1;
            Graphics.Draw(_top, base.x, base.y - 27f);
            Graphics.Draw(_bottom, base.x, base.y + 5f);
        }

    }
}