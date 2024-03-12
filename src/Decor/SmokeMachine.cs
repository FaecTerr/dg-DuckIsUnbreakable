using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class SmokeMachine : PhysicsObject
    {
        SpriteMap _sprite;
        public int waitFrames;


        public SmokeMachine(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/Fireplace.png"), 16, 16, false);
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            graphic = _sprite;
            alpha = 0;
            thickness = 0;
            hugWalls = WallHug.Floor;
            _enablePhysics = false;
        }
        public override void Update()
        {
            alpha = 0;

            if (waitFrames <= 0)
            {
                waitFrames = Rando.Int(12, 32);
                Level.Add(SmallSmoke.New(position.x + Rando.Float(-4, 4), position.y + Rando.Float(-8, -3), 0.8f, Rando.Float(0.8f, 1.5f)));
            }
            else
            {
                waitFrames--;
            }


            base.Update();
        }

    }
}
