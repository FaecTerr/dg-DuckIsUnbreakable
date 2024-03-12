using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class RattSplash : Thing
    {
        protected SpriteMap _sprite;

        public List<Vec2> pos = new List<Vec2>(); // from 0 to 1
        public int count;
        public float cooldown;
        public Duck duck;
        public RattSplash(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap((GetPath("Sprites/Stands/RattSplash.png")), 32, 32, false);
            _sprite.CenterOrigin();
            layer = Layer.Foreground;
        }

        public override void Update()
        {
            base.Update();
            if(cooldown <= 0)
            {
                cooldown = 1;
                count++;
                pos.Add(new Vec2(Rando.Float(0.1f, 0.9f) * 320, Rando.Float(0.1f, 0.9f) * 160));
            }
            else
            {
                cooldown -= 0.01666666f;
            }

            if(count > 20 && duck != null)
            {
                duck.Kill(new DTImpact(this));
            }
        }

        public override void Draw()
        {
            _sprite.scale = Level.current.camera.size / new Vec2(240, 120);
            base.Draw();

            for (int i = 0; i < count; i++)
            {
                _sprite.angle = 0.5f * i;
                position = Level.current.camera.position + pos[i] * _sprite.scale;
                Graphics.Draw(_sprite, position.x, position.y, 0.999f + 0.00001f*i);
            }
        }
    }
}
