using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [BaggedProperty("canSpawn", false)]
    [EditorGroup("Faecterr's|Details")]
    public class Paper : PhysicsObject
    {
        protected SpriteMap _sprite;
        private int PPlist;
        private float _burnt;
        public EditorProperty<int> style;

        public Paper(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Decorative/Stack.png"), 11, 13, false);
            graphic = _sprite;
            _maxHealth = 1f;
            _hitPoints = 1f;
            center = new Vec2(6f, 6f);
            collisionSize = new Vec2(12f, 12f);
            collisionOffset = new Vec2(-6f, -6f);
            base.depth = -0.5f;
            _editorName = "Stack";
            _canFlip = false;
            thickness = 0f;
            weight = 0f;
            flammable = 3f;
            base.hugWalls = WallHug.Floor;
            style = new EditorProperty<int>(-1, null, -1, 2, 1);
        }
        public override void Update()
        {
            base.Update();
            _sprite.frame = style;
            if (_onFire && _burnt < 0.9f)
            {
                float c = 1f - burnt;
                if (_hitPoints > c * _maxHealth)
                {
                    _hitPoints = c * _maxHealth;
                }
                _sprite.color = new Color(c, c, c);
            }
        }
        public override void EditorPropertyChanged(object property)
        {
            if (style == -1)
            {
                (graphic as SpriteMap).frame = Rando.Int(2);
                return;
            }
            (graphic as SpriteMap).frame = style.value;
        }
        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (base.isServerForObject)
            {
                SFX.Play(GetPath("SFX/PaperStack.wav"), 1f, 0f, 0.0f, false);
                for (int i = 0; i < 8; i++)
                {
                    Level.Add(new PieceOfPaper(x, y, style));
                }
            }
            Level.Remove(this);
            return base.Hit(bullet, hitPos);
        }
    }
}
