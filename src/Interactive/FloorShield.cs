using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff")]
    public class FloorShield : Holdable
    {
        public SpriteMap _sprite;
        public FloorShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Tilesets/Shield.png"), 8, 32, false);
            graphic = _sprite;
            center = new Vec2(4f, 28f);
            collisionSize = new Vec2(8f, 32f);
            collisionOffset = new Vec2(-4f, -28f);
            base.depth = -0.5f;
            _sprite.AddAnimation("download", 0.2f, false, new int[]
            {
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
            _sprite.AddAnimation("upload", 0.5f, false, new int[]
{
                8,
                7,
                6,
                5,
                4,
                3,
                2,
                1,
                0
});
            _canFlip = false;
            thickness = 2f;
            base.hugWalls = WallHug.Ceiling;
        }
        public override void Update()
        {
            base.Update();
            if(grounded)
            {
                if (_sprite.currentAnimation != "download")
                {
                    _sprite.SetAnimation("download");
                }
            }
            else
            {
                if(_sprite.currentAnimation != "upload")
                {
                    _sprite.SetAnimation("upload");
                }
            }
            if(_sprite.frame == 0)
            {
                thickness = 0;
                collisionSize = new Vec2(8f, 4f);
                collisionOffset = new Vec2(-4f, 0f);
            }
            if (_sprite.frame > 0 && _sprite.frame < 6)
            {
                thickness = 1f;
                collisionSize = new Vec2(8f, 19f);
                collisionOffset = new Vec2(-4f, -15f);
            }
            if (_sprite.frame > 5 && _sprite.frame < 10)
            {
                thickness = 2f;
                collisionSize = new Vec2(8f, 32f);
                collisionOffset = new Vec2(-4f, -28f);
            }
        }
    }
}
