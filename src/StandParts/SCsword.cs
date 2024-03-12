using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class SCsword : Gun
    {
        public bool onPress;
        private bool _drawing;
        public float _hold;
        public SpriteMap _sprite;
        public float _Spin;
        public StateBinding _fireBind = new StateBinding("_Spin", -1, false, false);

        public SCsword(float xval, float yval) : base(xval, yval)
        {
            _ammoType = new ATLaser();
            _ammoType.range = 170f;
            _ammoType.accuracy = 0.8f;
            _type = "gun";
            //infinite = true;
            _sprite = new SpriteMap(GetPath("Sprites/Stands/SCsword.png"), 10, 24, false);
            base.graphic = _sprite;
            _fullAuto = true;
            _fireWait = 1f;
            _kickForce = 3f;
            weight = 0.9f;
            physicsMaterial = PhysicsMaterial.Wood;
            center = new Vec2(4f, 21f);
            collisionOffset = new Vec2(-2f, -16f);
            collisionSize = new Vec2(10f, 24f);
            _barrelOffsetTL = new Vec2(4f, 1f);
        }

        public override void Draw()
        {
            base.Draw();
        }



        public override void Update()
        {
            base.Update();
            base.angleDegrees = _Spin;
            _Spin %= 360f;
            offDir = owner.offDir;
            SCbody scb = owner as SCbody;
            if (scb.ownerDuck != null)
            {
                offDir = scb.offDir;
                if (scb.mode == "normal")
                    position = new Vec2(anchorPosition.x - 1f * offDir, anchorPosition.y + 6f);
                if (scb.mode == "crouch")
                    position = new Vec2(anchorPosition.x - 1f * offDir, anchorPosition.y + 9f);
                if (scb.mode == "slide")
                    position = new Vec2(anchorPosition.x, anchorPosition.y + 10f);
                offDir = offDir;
                if (onPress == true)
                {
                    _Spin = Lerp.Float(_Spin, 75f*offDir, 24f);
                    foreach (Duck d in Level.CheckLineAll<Duck>(new Vec2(position + (Offset(base.barrelOffset) - position).normalized * 2f), base.barrelPosition))
                    {
                        if (d != null && d != scb.ownerDuck)
                        {
                            d.Kill(new DTImpale(this));
                        }
                    }
                }
                else if (onPress == false)
                {
                    _Spin = Lerp.Float(_Spin, -15f*offDir, 24f);
                }
            }
        }
    }
}
