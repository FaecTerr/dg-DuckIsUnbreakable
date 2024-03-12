using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Special")]
    public class Weather : Thing
    {
        public SpriteMap _sprite;
        public bool init;
        public EditorProperty<int> intensivity;
        public EditorProperty<bool> snow;
        public EditorProperty<bool> rain;
        public EditorProperty<bool> sandstorm;
        public EditorProperty<bool> fog;
        public EditorProperty<bool> big;
        public EditorProperty<bool> fitscreen;
        public Weather(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/weather.png"), 16, 16, false);
            _sprite.frame = 0;
            graphic = _sprite;
            _canFlip = true;
            _visibleInGame = false;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            base.depth = -0.7f;
            rain = new EditorProperty<bool>(false);
            snow = new EditorProperty<bool>(false);
            sandstorm = new EditorProperty<bool>(false);
            fog = new EditorProperty<bool>(false);
            big = new EditorProperty<bool>(false);
            fitscreen = new EditorProperty<bool>(false);
            intensivity = new EditorProperty<int>(1, this, 0f, 16f, 1f, null);
        }
        public override void Update()
        {
            base.Update();

            if (init == false)
            {
                if (fitscreen == true)
                {
                    Level.Add(new fitscreen(position.x, position.y, rain, snow, sandstorm));
                }
                init = true;
                if (fog == true)
                {
                    float size = (Level.current.topLeft.x - Level.current.bottomRight.x) / 64 * (Level.current.topLeft.y - Level.current.bottomRight.y) / 64;
                    for (int i = 0; i < intensivity * size; i++)
                    {
                        if (base.isServerForObject)
                            Level.Add(new FGCloud(Rando.Float(Level.current.topLeft.x, Level.current.bottomRight.x), Rando.Float(Level.current.topLeft.y, Level.current.bottomRight.y)));
                    }
                }
            }
            if (rain == true)
            {
                float size = 4f;
                if (big == true)
                {
                    size = 10f;
                }
                for (int i = 0; i < intensivity; i++)
                {
                    Level.Add(new Fluid(Rando.Float(Level.current.topLeft.x, Level.current.bottomRight.x), position.y, new Vec2(Rando.Float(-4f, 4f), 0f), Fluid.Water, null, size));
                }
            }
            if (snow == true)
            {
                for (int i = 0; i < intensivity; i++)
                {
                    Level.Add(new SnowFallParticle(Rando.Float(Level.current.topLeft.x, Level.current.bottomRight.x), position.y, new Vec2(Rando.Float(-4f, 4f), 0f), big));
                }
            }
            if (sandstorm == true)
            {
                for (int i = 0; i < intensivity; i++)
                {
                    Level.Add(new SandParticle(Rando.Float(Level.current.bottomRight.x + 80f, Level.current.bottomRight.x + 80f), Rando.Float(position.y - 160f, Level.current.bottomRight.y), new Vec2(Rando.Float(-4f, 4f), 0f), big, -5f));
                }

            }

        }
    }
}
