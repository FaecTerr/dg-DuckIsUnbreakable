using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class FGCloud : Thing
    {
        protected SpriteMap _sprite;
        public Vec2 move;

        public FGCloud(float xpos, float ypos, int style = 0) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/VFX/Clouds.png"), 49, 10, false);
            _sprite.frame = Rando.Int(3);
            center = new Vec2(5f, 3f);
            collisionSize = new Vec2(8f, 4f);
            collisionOffset = new Vec2(-4f, -2f);
            base.depth = -0.5f;
            _canFlip = false;
            move = new Vec2(Rando.Float(-0.06f, 0.06f), Rando.Float(-0.06f, 0.06f));
            xscale = Rando.Float(11f, 1.2f);
            yscale = xscale;
            layer = Layer.Foreground;
            alpha = Rando.Float(0.2f,0.5f);
            graphic = _sprite;
        }
        public override void Update()
        {
            base.Update();
            position += move;
        }
    }
}
