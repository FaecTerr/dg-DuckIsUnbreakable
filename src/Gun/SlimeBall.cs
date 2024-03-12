using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class SlimeBall : PhysicsObject
    {
        public SpriteMap _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Weapons/Slime.png"), 16, 16);
        public int loadedType;

        public SlimeBall(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite.scale = new Vec2(0.5f, 0.5f);
            graphic = _sprite;

            center = new Vec2(8, 8);
            _sprite.center = new Vec2(8, 8);

            collisionSize = new Vec2(8, 8);
            collisionOffset = new Vec2(-4, -4);

            gravMultiplier = 0.7f;

            weight = 0.9f;
            thickness = 0.1f;

            _sprite.AddAnimation("PinkIdle", 0.3f, true, new int[] { 0, 1, 2, 3});
            _sprite.AddAnimation("GreenIdle", 0.3f, true, new int[] { 4, 5, 6, 7 });
            grounded = false;
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSolidImpact(with, from);
            if (with != null)
            {
                if (with is Block || with is Platform)
                {
                    if (from == ImpactedFrom.Top)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Level.Add(new SlimeElement(position.x, position.y + 4) { hSpeed = -2f * (i - 1), vSpeed = -2f, loadedType = loadedType });
                        }
                    }
                    if (from == ImpactedFrom.Bottom)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Level.Add(new SlimeElement(position.x, position.y - 4) { hSpeed = -2f * (i - 1), vSpeed = -1f, loadedType = loadedType });
                        }
                    }
                    if (from == ImpactedFrom.Left)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Level.Add(new SlimeElement(position.x, position.y) { hSpeed = 1f + 1 * (i - 1), vSpeed = -1f + 1 * (i - 1), loadedType = loadedType, angleDegrees = 90 });
                        }
                    }
                    if (from == ImpactedFrom.Right)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Level.Add(new SlimeElement(position.x, position.y) { hSpeed = -1f - 1 * (i - 1), vSpeed = -1f + 1 * (i - 1), loadedType = loadedType, angleDegrees = 270 });
                        }
                    }
                }
            }
            Level.Remove(this);

        }

        public override void Update()
        {
            if(loadedType == 1)
            {
                _sprite.SetAnimation("PinkIdle");
            }
            if (loadedType == -1)
            {
                _sprite.SetAnimation("GreenIdle");
            }
            base.Update();
            if (grounded)
            {
                for (int i = 0; i < 3; i++)
                {
                    Level.Add(new SlimeElement(position.x, position.y - 4) { hSpeed = -3f * (i - 1), vSpeed = -2f, loadedType = loadedType });
                }
                Level.Remove(this);
            }
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
