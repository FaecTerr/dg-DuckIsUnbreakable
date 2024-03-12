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
    public class Fan : PhysicsObject
    {
        protected SpriteMap _sprite;

        private bool hitted = false;
        private float speed = Rando.Float(0.5f, 1.5f);
        private bool init = false;
        public EditorProperty<float> Speed;

        public Fan(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Decorative/Fan.png"), 32, 13, false);
            graphic = _sprite;
            center = new Vec2(16f, 7.5f);
            collisionSize = new Vec2(32f, 13f);
            collisionOffset = new Vec2(-16f, -6.5f);
            _editorName = "Ceiling Fan";
            base.depth = -0.5f;
            enablePhysics = false;
            grounded = false;
            _canFlip = false;
            Speed = new EditorProperty<float>(speed, null, 0.05f, 2f, 0.05f);
            _sprite.AddAnimation("idle", Speed, true, new int[]
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
                9
});
            _sprite.AddAnimation("broken", 1f, true, new int[]
{
                10
});
            thickness = 1f;
            base.hugWalls = WallHug.Ceiling;
            _sprite.SetAnimation("idle");
        }
        public override void Initialize()
        {
            Level.Add(new CeilingA(position.x-0.5f, position.y - 4f));
        }
        public override void Update()
        {
            base.Update();
            if (init == false)
            {
                _sprite.speed = Speed;
                init = true;
            }
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (hitted == false)
            {
                hitted = true;
                enablePhysics = true;
                _sprite.SetAnimation("broken");
                _sprite.frame = 10;
            }
            return base.Hit(bullet, hitPos);
        }
    }
}