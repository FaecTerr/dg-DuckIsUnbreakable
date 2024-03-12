using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class ManhattanTransfer : Stand
    {
        public MTransfer _mt;
        public bool forward;
        public StateBinding _standBind = new StateBinding("_mt", -1, false, false);
        public ManhattanTransfer(float xval, float yval, MTransfer mt) : base(xval, yval)
        {
            _pickupSprite.frame = 10;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 10;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            _mt = mt;
            _editorName = "Manhattan Transfer";
            standName = "MT";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(0, 34, 229);
        }
        public override void OnHoldMove()
        {
            base.OnHoldMove();
            if (owner != null)
            {
                Duck d = owner as Duck;
                if (_mt == null)
                {
                    _mt = new MTransfer(x, y);
                    Level.Add(_mt);
                    _mt._sleeping = true;
                    _mt.gravMultiplier = 0;
                    _mt.ownerDuck = d;
                }
                else if (_mt != null && forward == false)
                {
                    _mt.position -= (_mt.position - owner.position)*0.00444444f;
                }
                else if (_mt != null && forward == true)
                {
                    _mt.position += (_mt.position - owner.position) * 0.00444444f;
                }
            }
        }
        public override void OnStandMove()
        {
            base.OnStandMove();
            forward = !forward;
        }
        public override void OnSpecialMove()
        {
            base.OnSpecialMove();
            OnStandMove();
        }
        public override void OnSpecialMoveFast()
        {
            base.OnSpecialMoveFast();
            OnStandMove();
        }
        public override void OnSpecialMoveLong()
        {
            base.OnSpecialMoveLong();
            OnStandMove();
        }
        public override void Update()
        {
            base.Update();
            if (owner == null && _mt != null)
            {
                _mt.owner = null;
            }
            if (owner != null && _mt != null)
            {
                Duck d = owner as Duck;
                _mt.ownerDuck = d;
            }
        }
    }
}
