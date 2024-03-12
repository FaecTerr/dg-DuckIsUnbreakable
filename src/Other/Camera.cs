using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class Camera : MaterialThing, IPlatform
    {
        SpriteMap _sprite;
        public Camera(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Decorative/Camera.png"), 16, 16, false);
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            graphic = _sprite;
            depth = -0.9f;
            hugWalls = WallHug.Left | WallHug.Right;
            _sprite.AddAnimation("idle", 1 / 11f, true, new int[] {
                0,
                1,
                2,
                2,
                2,
                2,
                2,
                2,
                2,
                2,
                2,
                2,
                1,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            });
            _sprite.SetAnimation("idle");
        }
        public override void Draw()
        {
            graphic.flipH = flipHorizontal;
            base.Draw();
        }
    }
}
