using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class SUOpart : Thing
    {
        public SpriteMap _sprite;
        private float time = Rando.Float(1f,2f);
        private int i = 0;
        private SinWave _pulse = (SinWave)0.2f;
        public SUOpart(float xpos, float ypos, Vec3 _color) : base(xpos, ypos)
        {
            center = new Vec2(3f, 3f);
            depth = -0.6f;
            alpha = 0.5f;
            _sprite = new SpriteMap(Graphics.Recolor(Mod.GetPath<DuckUnbreakable>("Sprites/Stands/SUOpart"), _color), 6, 6);
            graphic = _sprite;
            _sprite.CenterOrigin();
            _sprite.frame = Rando.Int(0, 4);
        }

        public override void Draw()
        {
            base.Draw();
        }
        public override void Update()
        {
            base.Update();
            xscale = (yscale += _pulse*0.1f);
            if (i > 6)
            {
                i = 0;
                if (_sprite.frame <= 4 && _sprite.frame >= 0)
                    _sprite.frame = Rando.Int(5, 9);
                else if (_sprite.frame <= 9 && _sprite.frame >= 5)
                    _sprite.frame = Rando.Int(10, 14);
                else if (_sprite.frame <= 14 && _sprite.frame >= 10)
                    _sprite.frame = Rando.Int(15, 19);
                else if (_sprite.frame <= 19 && _sprite.frame >= 15)
                    _sprite.frame = Rando.Int(20, 24);
                else if (_sprite.frame <= 24 && _sprite.frame >= 20)
                    _sprite.frame = Rando.Int(20, 24);
            }
            i++;
            position.y -= Rando.Float(0.1f, 0.2f);

            if (time > 0f)
            {
                time -= 0.15f;
            }
            else
                alpha -= Rando.Float(0.05f, 0.1f);
            if (alpha < 0f)
            {
                Level.Remove(this);
            }
        }
    }
}
