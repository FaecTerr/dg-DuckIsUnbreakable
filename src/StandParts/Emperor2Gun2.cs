using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class Emperor2Gun2 : Gun
    {
        private float SFXplay;
        private SpriteMap _sprite;
        public int counter = 0;
        public float cooldown = 0f;
        public float rise;
        public float _angleOffset;

        public Emperor2Gun2(float xval, float yval) : base(xval, yval)
        {
            ammo = 8;
            _ammoType = new ATMagnum();
            _type = "gun";
            _sprite = new SpriteMap((GetPath("Sprites/Stands/EmperorGun")), 24, 11, false);
            center = new Vec2(11f, 5.5f);
            collisionOffset = new Vec2(-20f, -2f);
            collisionSize = new Vec2(22, 10f);
            _barrelOffsetTL = new Vec2(22f, 2f);
            _fullAuto = false;
            _fireWait = 0f;
            _kickForce = 2f;
            _holdOffset = new Vec2(-1f, 3f);
            _numBulletsPerFire = 1;
            _editorName = "E2";
            ammoType.range = 300f;
            ammoType.rangeVariation = 0f;
            ammoType.penetration = 2.5f;
            _fireSound = "pistolFire";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            graphic = _sprite;
        }
        public override void Update()
        {
            cooldown -= 0.1f;
            _sprite.SetAnimation("idle");
            if (counter <= 7 && cooldown <= 0f)
            {
                Fire();
                counter++;
                cooldown = 0.4f;
                rise += 0.2f;
            }
            else if (counter >= 8)
            {
                Level.Remove(this);
            }
            if (owner != null)
            {
                if (offDir < 0)
                {
                    _angleOffset = -Maths.DegToRad(-rise * 65f);
                }
                else
                {
                    _angleOffset = -Maths.DegToRad(rise * 65f);
                }
            }
            else
            {
                _angleOffset = 0f;
            }
            if (rise > 0f)
            {
                rise -= 0.013f;
            }
            else
            {
                rise = 0f;
            }
            if (_raised)
            {
                _angleOffset = 0f;
            }
            base.Update();
        }
        public override float angle
        {
            get
            {
                return base.angle + _angleOffset;
            }
            set
            {
                _angle = value;
            }
        }
    }
}