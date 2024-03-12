using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class EmperorGun : Gun
    {
        private float SFXplay;
        private SpriteMap _sprite;
        public int counter = -3;

        public EmperorGun(float xval, float yval) : base(xval, yval)
        {
            ammo = 1;
            _ammoType = new ATMagnum();
            _type = "gun";
            _sprite = new SpriteMap((GetPath("Sprites/Stands/EmperorGun")), 24, 11, false);
            center = new Vec2(11f, 5.5f);
            collisionOffset = new Vec2(-20f, -2f);
            collisionSize = new Vec2(22, 10f);
            _fullAuto = false;
            _barrelOffsetTL = new Vec2(22f, 2f);
            _fireWait = 2f;
            _kickForce = 2f;
            _holdOffset = new Vec2(-1f, 3f);
            _numBulletsPerFire = 1;
            _editorName = "E1";
            ammoType.range = 200f;
            ammoType.rangeVariation = 0f;
            ammoType.penetration = 1.5f;
            _fireSound = "pistolFire";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            graphic = _sprite;
        }
        public override void Update()
        {
            _sprite.SetAnimation("idle");
            if(counter >= 0)
                Fire(); 
            if (counter >= 2)
            {
                Level.Remove(this);
            }
            counter++;
            base.Update();
        }
    }
}
