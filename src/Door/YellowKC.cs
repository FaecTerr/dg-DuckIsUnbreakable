using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Doors")]
    public class YellowKC : KeyCard
    {
        private SpriteMap _sprite;

        public YellowKC(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/KCDoors/KeyCardYellow.png"), 10, 12, false);
            graphic = _sprite;
            center = new Vec2(5f, 6f);
            _collisionSize = new Vec2(10f, 12f);
            _collisionOffset = new Vec2(-5f, -6f);
            _sprite.frame = 0;
            base.depth = 0.9f;
            weight = 0f;
        }

        public override void Update()
        {
            _sprite.flipH = (offDir < 0);
            if (owner == null && _sprite.frame == 0)
            {
                using (IEnumerator<Thing> enumerator = Level.current.things[typeof(YellowPort)].GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Thing thing = enumerator.Current;
                        YellowPort socket = (YellowPort)thing;
                        if ((socket.position - position).length < 8f)
                        {
                            _sprite.frame = 1;
                            _enablePhysics = false;
                            position = socket.position;
                            base.depth = -0.8f;
                            //SFX.Play(GetPath("SFX/OpenKCDoor.wav"), 1f, 0f, 0.0f, true);
                            return;
                        }
                    }
                    goto IL_11C;
                }
            }
            if (owner != null && _sprite.frame == 1)
            {
                _sprite.frame = 0;
                _enablePhysics = true;
            }
            IL_11C:
            base.Update();
        }
    }
}

