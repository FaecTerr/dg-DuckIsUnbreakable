using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class Gramophone : Holdable
    {
        SpriteMap _sprite;
        public int chance;
        public bool init;

        public Gramophone(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/gramophone.png"), 18, 18, false);
            center = new Vec2(9f, 9f);
            collisionSize = new Vec2(18f, 18f);
            collisionOffset = new Vec2(-9f, -9f);
            graphic = _sprite;
            _sprite.AddAnimation("idle", 0.2f, true, new int[] {
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
            weight = 3;
            hugWalls = WallHug.Floor;
            _enablePhysics = true;
        }
        public override void Update()
        {
            if (!init)
            {
                init = true;
                SFX.Play(GetPath("SFX/Gramophone.wav"), 0.8f, 0, 0, true);
            }
            if (Rando.Float(1) > 0.999f - chance * 0.001f)
            {
                Level.Add(new MusicNote(position.x - 7 * offDir, position.y - 6, new Vec2(offDir * -0.2f * Rando.Float(0.8f, 1.2f), Rando.Float(-1, 1))) { depth = 1.5f});
                chance = 0;
            }
            else
            {
                chance++;
            }
            base.Update();
        }

    }
}
