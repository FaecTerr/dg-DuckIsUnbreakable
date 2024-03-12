using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class Ratt : Stand
    {
        public float Cooldown;

        public Ratt(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 21;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 21;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            standName = "RAT";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(40, 0, 153);

            usingCharging = true;
        }

        public override void OnSpecialMoveFast()
        {
            if (Cooldown <= 0 && _equippedDuck != null)
            {
                if (_equippedDuck.holdObject == null)
                {
                    Cooldown = 4;
                    RattGun gun = new RattGun(position.x, position.y) { ammo = 1 };
                    Level.Add(gun);
                    _equippedDuck.GiveHoldable(gun);
                }
            }
            base.OnSpecialMoveFast();
        }
        public override void OnSpecialMoveLong()
        {
            if (Cooldown <= 0 && _equippedDuck != null)
            {
                if (_equippedDuck.holdObject == null)
                {
                    Cooldown = 8;
                    RattGun gun = new RattGun(position.x, position.y) { ammo = 3 };
                    Level.Add(gun);
                    _equippedDuck.GiveHoldable(gun);
                }
            }
            base.OnSpecialMoveLong();
        }
        public override void OnSpecialMove()
        {
            if(Cooldown <= 0 && _equippedDuck != null)
            {
                if (_equippedDuck.holdObject == null)
                {
                    Cooldown = 12;
                    RattGun gun = new RattGun(position.x, position.y) { ammo = 2 };
                    Level.Add(gun);
                    _equippedDuck.GiveHoldable(gun);
                }
            }
            base.OnSpecialMove();
        }

        public override void Update()
        {
            if(Cooldown > 0)
            {
                Cooldown -= 0.01666666f;
            }
            base.Update();
        }
    }
}
