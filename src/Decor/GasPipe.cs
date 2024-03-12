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
    public class GasPipe : Thing
    {
        private SpriteMap _sprite;
        public EditorProperty<float> rate;
        public EditorProperty<bool> steam;
        public EditorProperty<bool> vertical;
        public EditorProperty<int> style;
        public float time; 

        public GasPipe(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Tilesets/GasPipe.png"), 16, 16, false);
            _sprite.frame = 0;
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            base.depth = -0.7f;
            _canFlip = true;
            base.hugWalls = (WallHug.Floor | WallHug.Ceiling | WallHug.Left | WallHug.Right);
            rate = new EditorProperty<float>(10f, null, 0.5f, 50f, 0.1f);
            steam = new EditorProperty<bool>(true);
            vertical = new EditorProperty<bool>(false);
            style = new EditorProperty<int>(20, null, 0f, 24f, 1f);
        }
        public override void Update()
        {
            base.Update();
            _sprite.frame = style;
            if (time <= 0f && steam == true && vertical == false)
            {
                time = rate;
                if (base.isServerForObject)
                    Level.Add(new Cloud(base.x, base.y, offDir));
            }
            else if (time <= 0f && steam == true && vertical == true)
            {
                time = rate;
                if (base.isServerForObject)
                    Level.Add(new CloudV(base.x, base.y, offDir));
            }
            else
            {
                time -= 0.01f;
            }
        }
        public override void Draw()
        {
            _sprite.frame = style;
            graphic.flipH = flipHorizontal;
            base.Draw();
        }
    }
}
