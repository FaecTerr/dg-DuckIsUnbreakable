using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class SittingDuck : PhysicsObject
    {
        SpriteMap _sprite;
        public int waitFrames;
        public int increasedChance;

        public SittingDuck(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/armchair.png"), 32, 32, false);
            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(22f, 30f);
            collisionOffset = new Vec2(-12f, -15f);
            graphic = _sprite;            
            _sprite.SetAnimation("idle");
            thickness = 0;
            hugWalls = WallHug.Floor;
        }
        public override void Update()
        {
            base.Update();

            if (waitFrames <= 0)
            {
                int power = 1;
                if (Rando.Float(1) > 0.99f - increasedChance * 0.01f)
                {
                    power = 1;
                    _sprite.frame = 1;
                    increasedChance = 0;
                }
                else
                {
                    power = 3;
                    _sprite.frame = 0;
                    increasedChance++;
                }
                waitFrames = Rando.Int(2, 6) * power;
            }
            else
            {
                waitFrames--;
            }
        }
    }
}
