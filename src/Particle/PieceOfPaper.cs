using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class PieceOfPaper : PhysicsObject
    {
        protected SpriteMap _sprite;
        private float randomSpin = Rando.Float(0.3f);

        public PieceOfPaper(float xpos, float ypos, int style=0) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Decorative/PaperPiece.png"), 10, 6, false);
            graphic = _sprite;
            if(style > 0)
            {
                style = 1;
            }
            _sprite.frame = Rando.Int(2) + style*4;
            center = new Vec2(5f, 3f);
            collisionSize = new Vec2(8f, 4f);
            collisionOffset = new Vec2(-4f, -2f);
            base.depth = -0.5f;
            _canFlip = false;
            vSpeed = Rando.Float(-0.1f, -2f);
            hSpeed = Rando.Float(-4f, 4f);
            gravMultiplier = 0.1f;
            alpha = Rando.Float(0.8f, 1f);
            thickness = 0.6f;
            weight = 0f;
            flammable = 1f;
        }
        public override void Update()
        {
            base.Update();
            if (!(grounded))
                angle += randomSpin;
            else
            {
                _sprite.frame = 3;
                angle = 0;
            }
            alpha -= 0.002f;
            if (alpha < 0.05f)
                Level.Remove(this);
            if (grounded)
                alpha -= 0.015f;
        }
    }
}
