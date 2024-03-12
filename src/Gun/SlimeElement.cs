using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class SlimeElement : PhysicsObject
    {
        public SpriteMap _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Weapons/GroundedSlime.png"), 16, 16);
        public int loadedType;
        public float rotation = Rando.Float(-10f, 10f);
        public bool place;

        public int lifeTime;

        public SlimeElement(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = _sprite;

            center = new Vec2(8, 4);
            _sprite.center = new Vec2(8, 4);

            collisionSize = new Vec2(16, 8);
            collisionOffset = new Vec2(-8, -4);

            gravMultiplier = 0.7f;

            weight = 0.9f;
            thickness = 0.1f;
            layer = Layer.Foreground;
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSolidImpact(with, from);
            if (with != null)
            {
                place = true;
                if (from == ImpactedFrom.Top)
                {
                    enablePhysics = false;
                    _sprite.angleDegrees = 180;
                    angleDegrees = 180;
                    if (loadedType == 1)
                    {
                        _sprite.frame = 0;
                    }
                    else
                    {
                        _sprite.frame = 1;
                    }
                }
                if (from == ImpactedFrom.Bottom)
                {
                    enablePhysics = false;
                    _sprite.angleDegrees = 0;
                    if (loadedType == 1)
                    {
                        _sprite.frame = 0;
                    }
                    else
                    {
                        _sprite.frame = 1;
                    }
                }
                if (from == ImpactedFrom.Left)
                {
                    enablePhysics = false;
                    _sprite.angleDegrees = 90;
                    angleDegrees = 90;
                    if (loadedType == 1)
                    {
                        _sprite.frame = 0;
                    }
                    else
                    {
                        _sprite.frame = 1;
                    }
                }
                if (from == ImpactedFrom.Right)
                {
                    enablePhysics = false;
                    _sprite.angleDegrees = 270;
                    angleDegrees = 270;
                    if (loadedType == 1)
                    {
                        _sprite.frame = 0;
                    }
                    else
                    {
                        _sprite.frame = 1;
                    }
                }
            }
        }

        public override void Update()
        {
            if (place)
            {
                if (lifeTime < 100)
                {
                    lifeTime++;
                }
                if(loadedType == 1)
                {
                    _sprite.frame = 0;
                    foreach (Duck d in Level.CheckRectAll<Duck>(topLeft, bottomRight))
                    {
                        if (d.inputProfile.Pressed("JUMP"))
                        {
                            d.vSpeed -= 10f;
                        }
                    }
                }
                else
                {
                    _sprite.frame = 1;
                    foreach (Duck d in Level.CheckRectAll<Duck>(topLeft , bottomRight))
                    {
                        d.hSpeed *= 0.98f;
                        d.vSpeed *= d.vSpeed > 0 ? 1f : 0.99f;
                    }
                }
            }
            if (!place)
            {
                if(loadedType == 1)
                {
                    _sprite.frame = 2;
                }
                else
                {
                    _sprite.frame = 3;
                }
                _sprite.angleDegrees += rotation;
                rotation *= 0.99f;
            }
            foreach (SlimeElement s in Level.CheckRectAll<SlimeElement>(topLeft + new Vec2(2, 1), bottomRight - new Vec2(2, 1)))
            {
                if(s.loadedType != loadedType)
                {
                    if (lifeTime <= s.lifeTime)
                    {
                        Level.Remove(s);
                    }
                    else
                    {
                        Level.Remove(this);
                    }
                }
            }
            base.Update();
            if (_grounded)
            {
                place = true;
                enablePhysics = false;
                _sprite.angleDegrees = 0;
                if (loadedType == 1)
                {
                    _sprite.frame = 0;
                }
                else
                {
                    _sprite.frame = 1;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
