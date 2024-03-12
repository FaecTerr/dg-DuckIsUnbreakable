using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    public class StandUserOutline : Thing
    {
        public SpriteMap _sprite;
        public Vec3 color = new Vec3(255, 255, 255);
        private int counter = 0;
        private int counter2 = 0;

        public StandUserOutline(float xpos, float ypos, Vec3 _color) : base(xpos, ypos)
        {
            center = new Vec2(16f, 12f);
            depth = -0.6f;
            alpha = 0.5f;
            _sprite = new SpriteMap(Graphics.Recolor(Mod.GetPath<DuckUnbreakable>("Sprites/Stands/StandOutline"), _color), 32, 32);
            graphic = _sprite;
            _sprite.CenterOrigin();
            color = _color;
        }

        public override void Draw()
        {
            base.Draw();
        }
        public override void Update()
        {
            base.Update();
            bool fl = false;
            Duck d = owner as Duck;
            if (owner != null)
            {
                if (offDir > 0)
                 {
                     fl = false;
                 }
                 if (offDir < 0)
                 {
                     fl = true;
                 }
            }
             graphic.flipH = fl;
             _sprite.flipH = fl;
             _flipHorizontal = fl;
             //if (owner != null)
             //   _offDir = owner._offDir;
            //_sprite = new SpriteMap(Graphics.Recolor(Mod.GetPath<DuckUnbreakable>("StandOutline"), _color), 32, 32);
            if (counter == 5)
            {
                if (d.crouch)
                {
                    _sprite.frame = Rando.Int(5, 9);
                }
                else 
                    _sprite.frame = Rando.Int(0, 4);
                counter = 0;
            }
            else
                counter++;
            if (counter2 > 9)
            {
                Level.Add(new SUOpart(position.x + Rando.Float(-16f, 16f), position.y + Rando.Float(-16f, 16f), color) { alpha = alpha });
                counter2 = 0;
            }
            else
                counter2 += Rando.Int(1, 5);
        }
    }
}
