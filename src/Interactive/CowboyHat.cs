using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Equipment")]
    public class CowboyHat : Hat
    {
        public CowboyHat(float xpos, float ypos) : base(xpos, ypos)
        {
            _pickupSprite = new Sprite(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Equipment/Cowboy.png"), 0f, 0f);
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/Equipment/Cowboy.png"), 32, 32, false);
            graphic = _pickupSprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-5f, -2f);
            collisionSize = new Vec2(12f, 8f);
            _sprite.CenterOrigin();
            _equippedThickness = 3f;
        }

        public override void Update()
        {
            base.Update();
            Duck d = owner as Duck;
            if (d != null)
            {
                if (d.holdObject != null)
                {
                    if (d.holdObject is Gun)
                    {
                        Gun g = d.holdObject as Gun;
                        if (g.ammoType != null)
                        {
                            if (g.ammoType.range < 500f)
                            {
                                g.ammoType.range = 500f;
                            }
                            if (g._fireWait > 0.8f)
                            {
                                g._fireWait -= 0.02f;
                            }
                            if (g._accuracyLost > 0.2f)
                            {
                                g._accuracyLost -= 0.06f;
                            }
                            if (g.ammoType.accuracy < 0.8f)
                            {
                                g.ammoType.accuracy += 0.02f;
                            }
                        }
                    }
                }
            }
        }
    }
}
