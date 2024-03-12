using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class Fireplace : PhysicsObject
    {
        SpriteMap _sprite;
        public int waitFrames;


        public Fireplace(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/Fireplace.png"), 24, 17, false);
            center = new Vec2(12f, 8f);
            collisionSize = new Vec2(22f, 16f);
            collisionOffset = new Vec2(-12f, -8f);
            graphic = _sprite;
            _sprite.AddAnimation("idle", 0.3f, true, new int[] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7
            });
            _sprite.SetAnimation("idle");
            thickness = 0;
            hugWalls = WallHug.Floor;
        }
        public override void Update()
        {
            if (waitFrames <= 0)
            {
                waitFrames = Rando.Int(12, 32);
                Level.Add(SmallSmoke.New(position.x + Rando.Float(-4, 4), position.y + Rando.Float(6, 8), 0.8f, Rando.Float(0.8f, 1.5f)));
            }
            else
            {
                waitFrames--;
            }

            if(Rando.Float(1) > 0.95f)
            {
                for (int i = 0; i < Rando.Int(2, 5); i++)
                {
                    Level.Add(Spark.New(position.x + Rando.Float(-4, 4), position.y + Rando.Float(6, 8),
                        new Vec2(Rando.Float(-0.25f, 0.005f), Rando.Float(0.1f, 0.15f)), Rando.Float(0.01f, 0.03f)));
                }
            }

            base.Update();
        }

    }
}
