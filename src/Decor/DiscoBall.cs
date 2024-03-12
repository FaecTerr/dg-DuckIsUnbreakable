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
    public class DiscoBall : PhysicsObject
    {
        protected SpriteMap _sprite;
        private bool hitted = false;
        private int col = Rando.Int(5);
        private bool init = false;
        private PointLight light;
        public float time;
        public EditorProperty<int> BPM;
        public DiscoBall(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Blocks/DiscoBall.png"), 16, 36, false);
            graphic = _sprite;
            center = new Vec2(8f, 18f);
            collisionSize = new Vec2(14f, 34f);
            collisionOffset = new Vec2(-7f, -17f);
            _editorName = "Disco Ball";
            base.depth = -0.5f;
            enablePhysics = false;
            grounded = false;
            _canFlip = false;
            _sprite.AddAnimation("idle", 0.15f, true, new int[]
            {
                0,
                1
            });
            thickness = 1f;
            base.hugWalls = WallHug.Ceiling;
            _sprite.SetAnimation("idle");
            BPM = new EditorProperty<int>(60, this, 15, 240, 1);
        }
        public virtual void ReloadColor()
        {
            col = Rando.Int(5);
            Color color = new Color(0, 0, 0);
            if (col == 0)
            {
                color = new Color(255, 0, 110);
            }
            else if (col == 1)
            {
                color = new Color(255, 20, 147);
            }
            else if (col == 2)
            {
                color = new Color(255, 110, 110);
            }
            else if (col == 3)
            {
                color = new Color(255, 140, 0);
            }
            else if (col == 4)
            {
                color = new Color(255, 255, 0);
            }
            else if (col == 5)
            {
                color = new Color(0, 255, 255);
            }
            light = new PointLight(base.x, base.y, color, 100f, null, true);
            Level.Add(light);
        }
        public override void Initialize()
        {
            Level.Add(new CeilingB(position.x-0.5f, position.y - 12.5f));
            if (!(Level.current is Editor))
            {
                ReloadColor();
            }
        }
        public override void Update()
        {
            base.Update();
            light.position = position;
            
            if(time < 0)
            {
                Level.Remove(light);
                ReloadColor();
                if (BPM != 0)
                    time = 60f / BPM;
                else
                    time = 1f;
            }
            else
            {
                time -= 0.01666666f;
            }
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (hitted == false)
            {
                if (isServerForObject)
                    Level.Add(new Disco(position.x, position.y + 6f) { vSpeed = 0.5f });
                Level.Remove(this);
                Level.Remove(light);
            }
            return base.Hit(bullet, hitPos);
        }
    }
}