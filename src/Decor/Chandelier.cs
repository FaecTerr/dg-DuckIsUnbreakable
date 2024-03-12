using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [BaggedProperty("canSpawn", false)]
    [EditorGroup("Faecterr's|Stuff|Hanging")]
    public class Chandelier : PhysicsObject
    {
        protected SpriteMap _sprite;

        private bool hitted = false;
        private int time;
        private List<LightOccluder> _occluders = new List<LightOccluder>();

        public Chandelier(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Decorative/Chandelier.png"), 33, 26, false);
            graphic = _sprite;
            center = new Vec2(16.5f, 14f);
            collisionSize = new Vec2(33f, 26f);
            collisionOffset = new Vec2(-16.5f, -13f);
            base.depth = -0.5f;
            enablePhysics = false;
            grounded = false;
            _canFlip = false;
            thickness = 1f;
            base.hugWalls = WallHug.Ceiling;
        }
        public override void Initialize()
        {
            Level.Add(new CeilingA(position.x, position.y - 12f));
            if (!(Level.current is Editor))
            {
                _occluders.Add(new LightOccluder(position + new Vec2(-15f, -3f), position + new Vec2(15f, -3f), new Color(0f, 0f, 0f)));
                _occluders.Add(new LightOccluder(position + new Vec2(-25f, 20f), position + new Vec2(-12f, -4f), new Color(0f, 0f, 0f)));
                _occluders.Add(new LightOccluder(position + new Vec2(12f, -4f), position + new Vec2(25f, 20f), new Color(0f, 0f, 0f)));
                Level.Add(new PointLight(base.x, base.y, new Color(255, 255, 180), 100f, _occluders, false));
            }
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (hitted == false)
            {
                hitted = true;
                _sprite.frame = 1;
                enablePhysics = true;
            }
            return base.Hit(bullet, hitPos);
        }
        public override void Draw()
        {

            base.Draw();
        }
    }
}
