using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stands")]
    public class Emperor : Stand
    {
        private float counter;
        private float SFXplay;
        public Holdable g;
        public Duck d;
        public float smallCooldown = 0f;
        public float Cooldown = 0f;
        public StateBinding _EmperorCD = new StateBinding("Cooldown", -1, false, false);
        public StateBinding _EmperorSCD = new StateBinding("smallCooldown", -1, false, false);
        public StateBinding _itemBinding = new StateBinding("g", -1, false, false);

        public Emperor(float xval, float yval) : base(xval, yval)
        {
            _pickupSprite.frame = 4;
            center = new Vec2(5f, 7.5f);
            collisionOffset = new Vec2(-5f, -7.5f);
            collisionSize = new Vec2(9f, 14f);
            _autoOffset = false;
            _sprite.frame = 4;
            thickness = 0.1f;
            _equippedDepth = 4;
            _punchForce = 0f;
            standName = "EMP";
            _sprite.AddAnimation("idle", 1f, true, new int[1]);
            standColor = new Vec3(40, 0, 153);
            
            usingCharging = true;
        }

        public override void OnSpecialMoveFast()
        {
            base.OnSpecialMoveFast();
            if (d.ragdoll == null && isServerForObject)
            {
                if (d.holdObject == null && Cooldown <= 0f)
                {
                    g = null;
                    g = new Emperor2Gun2(position.x, position.y);
                    if(isServerForObject)
                        Level.Add(g);
                    d.GiveHoldable(g);
                    Cooldown = 2f;
                }
            }
        }
        public override void OnSpecialMoveLong()
        {
            OnSpecialMoveFast();
            base.OnSpecialMoveLong();
        }
        public override void OnSpecialMove()
        {
            OnSpecialMoveFast();
            base.OnSpecialMove();
        }

        public override void OnStandMove()
        {
            base.OnStandMove();
            if (d.ragdoll == null && isServerForObject)
            {
                if (d.holdObject == null && smallCooldown <= 0f)
                {
                    g = null;
                    g = new EmperorGun(position.x, position.y);
                    if(isServerForObject)
                        Level.Add(g);
                    d.GiveHoldable(g);
                    smallCooldown = 0.5f;
                }
            }
        }

        public override void Update()
        {
            base.Update();
            if (owner != null)
            {
                d = owner as Duck;
            }
            smallCooldown -= 0.01666666f;
            Cooldown -= 0.01666666f;
        }
    }
}
