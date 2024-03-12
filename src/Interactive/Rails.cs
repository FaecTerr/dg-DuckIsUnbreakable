using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff|Trails")]
    public class Rails : Thing
    {
        public SpriteMap _sprite;
        public bool isEnd;
        public bool isDiagonal;
        public bool isRight;

        public Rails(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Tilesets/Rails.png"), 16, 20);
            base.graphic = _sprite;
            center = new Vec2(8f, 14f);
            collisionSize = new Vec2(16f, 20f);
            collisionOffset = new Vec2(-8f, -14f);
            base.depth = -0.5f;
            hugWalls = WallHug.Floor;
        }
        public override void Update()
        {
            base.Update();
            Rails leftR = Level.CheckLine<Rails>(new Vec2(x - 16f, y), new Vec2(x, y), this);
            Rails rightR = Level.CheckLine<Rails>(new Vec2(x + 16f, y), new Vec2(x, y), this);
            Rails leftTop = Level.CheckLine<Rails>(new Vec2(x - 16f, y - 20f), new Vec2(x - 16f, y), this);
            Rails rightTop = Level.CheckLine<Rails>(new Vec2(x + 16f, y - 20f), new Vec2(x + 16f, y), this);
            Rails leftBottom = Level.CheckLine<Rails>(new Vec2(x - 16f, y + 10f), new Vec2(x - 16f, y), this);
            Rails rightBottom = Level.CheckLine<Rails>(new Vec2(x + 16f, y + 10f), new Vec2(x + 16f, y), this);
            if (leftR == null && rightR == null && rightBottom == null && leftBottom == null || leftBottom != null && rightBottom  != null || 
                leftR != null && rightBottom !=null || rightR != null && leftBottom!= null|| rightR != null && leftR != null)
            {
                _sprite.frame = 2;
                isEnd = false;
                isDiagonal = false;
            }
            else if (leftR == null && rightR != null && leftBottom == null)
            {
                _sprite.frame = 0;
                isEnd = true;
                isRight = false;
                isDiagonal = false;
            }
            else if (leftR != null && rightR == null && rightBottom == null)
            {
                _sprite.frame = 4;
                isEnd = true;
                isRight = true;
                isDiagonal = false;
            }
            if (leftR == null && leftTop != null)
            {
                _sprite.frame = 1;
                isEnd = false;
                isDiagonal = true;
                isRight = false;
            }
            if (rightR == null && rightTop != null)
            {
                _sprite.frame = 3;
                isEnd = false;
                isDiagonal = true;
                isRight = true;
            }
        }
    }   
}
