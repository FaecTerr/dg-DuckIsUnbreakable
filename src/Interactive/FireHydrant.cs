using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [BaggedProperty("canSpawn", false)]
    [EditorGroup("Faecterr's|Special")]
    public class FireHydrant : PhysicsObject
    {
        private SpriteMap _sprite;
        public EditorProperty<float> delay;
        public float time;
        public bool watering;
        public int counter = 0;
        public SpriteMap _water;
        private int i;

        public FireHydrant(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Blocks/firehydrant.png"), 12, 20, false);
            _sprite.frame = 0;
            graphic = _sprite;
            _canFlip = true;
            collisionSize = new Vec2(12f, 20f);
            collisionOffset = new Vec2(-6f, -10f);
            base.depth = -0.7f;
            thickness = 0f;
            base.hugWalls = WallHug.Floor;
            _water = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/WaterUp.png"), 16, 48, false);
            _water.frame = 0;
            center = new Vec2(6f, 10f);
            _water.AddAnimation("idle", 1f, false, new int[]
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16
            });
            _water.AddAnimation("loop", 1f, true, new int[]
            {
                15,
                16
            });
            _water.SetAnimation("idle");
            delay = new EditorProperty<float>(2f, null, 0.1f, 50f, 0.1f);
        }
        public override void Update()
        {
            base.Update();
            if (enablePhysics == true)
                enablePhysics = false;
            if (_water.frame == 16 && _water.currentAnimation == "idle")
            {
                _water.SetAnimation("loop");
            }
            if (time > 0f && watering == true)
            {
                time -= 0.01f;
                i++;
                _water.alpha = 1f;
                if (i == 1)
                {
                    Level.Add(new Fluid(base.x, base.y + Rando.Float(-4f, 4f), new Vec2(Rando.Float(-1f, 1f), Rando.Float(-6f, -3f)), Fluid.Water, null, 15f));
                    i = 0;
                }
                foreach (PhysicsObject d in Level.CheckLineAll<PhysicsObject>(new Vec2(position.x, position.y), new Vec2(position.x, position.y - 74f)))
                {
                    if (d != null)
                    {
                        d.vSpeed = -1f;
                        d.velocity += new Vec2(0f, -1f);
                    }
                }
            }
            else if (time <= 0f && watering == true)
            {
                time = delay;
                watering = false;
                _water.alpha = 1f;
                _water.SetAnimation("idle");
            }
            else if (time > 0f && watering == false)
            {
                time -= 0.01f;
                _water.alpha = 0f;
            }
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            counter++;
            time = 3f;
            watering = true;
            _water.alpha = 1f;
            _water.SetAnimation("idle");
            if (counter >= 3)
            {
                for (int i = 0; i < 10; i++)
                {
                    Level.Add(new Fluid(base.x + Rando.Float(-4f, 4f), base.y + Rando.Float(-4f, 4f), new Vec2(Rando.Float(-4f, 4f), Rando.Float(-12f, -3f)), Fluid.Water, null, 15f));
                }
                Level.Remove(this);
            }
            return base.Hit(bullet, hitPos);
        }
        public override void Draw()
        {
            base.Draw();
            _water.position.y = position.y - 10f;
        }
    }
}
