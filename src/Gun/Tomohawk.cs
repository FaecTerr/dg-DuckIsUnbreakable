using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Guns")]
    public class Tomohawk : Gun
    {
        public Vec2 SharpPos;
        public bool inObj;
        private float dir;

        public Duck ownerDuck;

        public Tomohawk(float xval, float yval) : base(xval, yval)
        {
            ammo = 4;
            _ammoType = new ATLaser();
            _ammoType.range = 170f;
            _ammoType.accuracy = 0.8f;
            _type = "gun";
            graphic = new Sprite(GetPath("Sprites/Things/Weapons/Tomohawk.png"), 0f, 0f);
            center = new Vec2(7.5f, 12f);
            collisionOffset = new Vec2(-7.5f, -6f);
            collisionSize = new Vec2(15f, 16f);
            _barrelOffsetTL = new Vec2(4f, 1f);
            _fireSound = "smg";
            _fullAuto = true;
            _fireWait = 1f;
            _kickForce = 3f;
            _holdOffset = new Vec2(-4f, -4f);
            weight = 2f;
            _bouncy = 0.5f;
            physicsMaterial = PhysicsMaterial.Metal;
        }
        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSoftImpact(with, from);
            if (with != null && with is Duck && (velocity.length > 5f) && prevOwner != null && owner == null && inObj == false)
            {
                if (with != ownerDuck)
                {
                    with.Destroy(new DTImpact(this));
                }
            }
            if (with != null && with is Holdable && !(with is Duck || with is Equipment || with is Gun || with is RagdollPart))
            {
                if (with != owner)
                {
                    with.Hurt(6f);
                    if (!with.destroyed)
                    {
                        anchor = with;
                        inObj = true;
                        dir = angle;
                    }
                    with.Hurt(7f);
                    if (with.destroyed)
                    {
                        anchor = null;
                        inObj = false;
                    }
                }
            }
            if (with != null && !(with is Duck || with is RagdollPart || with is Equipment) && (with is Door || with is Window))
            {
                with.Destroy();
            }
        }
        public override void Fire()
        {
        }
        public override float angle
        {
            get
            {
                if (!grounded && anchor == null && inObj == false)
                    return base.angle - 0.05f * (float)offDir;
                else if (anchor != null)
                    return 115f*(float)offDir;
                else
                    return base.angle;
            }
            set
            {
                _angle = value;
            }
        }
        public override void Update()
        {
            base.Update();
            if (anchor != null)
            {
                position = new Vec2(anchorPosition.x-4f*(float)offDir, anchorPosition.y - 9f);
            }
            if ((hSpeed > -1f && hSpeed < 1f) || (velocity.x < 1f && velocity.x > -1f))
            {
                gravMultiplier = 1f;
            }
            else
            {
                gravMultiplier = 0.9f;
            }
            if (owner != null)
            {
                inObj = false;
                anchor = null;
                ownerDuck = owner as Duck;
            }

        }
    }
}
