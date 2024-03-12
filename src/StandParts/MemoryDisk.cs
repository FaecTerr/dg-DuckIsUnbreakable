using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class MemoryDisk : Holdable
    {
        public SpriteMap _sprite;

        public MemoryDisk(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Stands/MemoryDisk.png"), 14, 14, false);
            graphic = _sprite;
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-6f, -6f);
            collisionSize = new Vec2(12f, 12f);
            base.depth = -0.5f;
            weight = 0.5f;
        }

        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSoftImpact(with, from);
            if (with is Duck)
            {
                Duck d = with as Duck;
                if (d.immobilized == true)
                {
                    d.immobilized = false;
                    Level.Remove(this);
                }
            }
        }
    }
}
