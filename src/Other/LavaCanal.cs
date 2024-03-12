using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [BaggedProperty("canSpawn", false)]
    [EditorGroup("Faecterr's|Special")]
    public class LavaPipe : Thing
    {
        private SpriteMap _sprite;
        public EditorProperty<float> rate;
        public float time;
        public bool floating = false;
        public EditorProperty<float> initialTime; //0.5, 1.05, 1.6
        public bool _init = false;
        public LavaPipe(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/LavaFloat.png"), 8, 16, false);
            _sprite.frame = 0;
            graphic = _sprite;
            center = new Vec2(4f, 8f);
            collisionSize = new Vec2(8f, 16f);
            collisionOffset = new Vec2(-4f, -8f);
            base.depth = -0.7f;
            _canFlip = true;
            _sprite.AddAnimation("idleBottom", 1/4f, true, new int[] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7
            });
            _sprite.AddAnimation("idleTop", 1/4f, true, new int[] {
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15
            });
            _sprite.AddAnimation("nullBottom", 1/4f, false, new int[] {
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23
            });
            _sprite.AddAnimation("nullTop", 1/4f, false, new int[] {
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31
            });
            _sprite.AddAnimation("startBottom", 1 / 4f, false, new int[] {
                32,
                33,
                34,
                35,
                36,
                37,
                38,
                39
            });
            _sprite.AddAnimation("startTop", 1 / 4f, false, new int[] {
                40,
                41,
                42,
                43,
                44,
                45,
                46,
                47
            });
            rate = new EditorProperty<float>(10f, null, 0.5f, 50f, 0.1f);
            initialTime = new EditorProperty<float>(10f, null, 0.5f, 50f, 0.1f);
        }
        public override void Update()
        {
            base.Update();
            if (_init == false)
            {
                time = initialTime;
                _init = true;
            }
            if (time <= 0f)
            {
                time = rate;
                floating = !floating;
            }
            else
            {
                time -= 0.01666666f;
                if (floating == true)
                {
                    foreach (Duck d in Level.CheckRectAll<Duck>(topLeft, bottomRight))
                    {
                        d.DoHeatUp(5.2f);
                        d.onFire = true;
                        d.heat = 5;
                        d.UpdateOnFire();
                        d.AddFire();
                    }
                    foreach (RagdollPart d in Level.CheckRectAll<RagdollPart>(topLeft, bottomRight))
                    {
                        d.DoHeatUp(5.2f);
                        d.onFire = true;
                        d.UpdateOnFire();
                        d.heat = 5;
                        d.AddFire();
                        if (d.duck != null)
                        {
                            d.duck.onFire = true;
                            d.duck.DoHeatUp(5.2f);
                            d.duck.heat = 5;
                            d.duck.AddFire();
                            d.duck.UpdateOnFire();
                        }
                    }
                }
                if (Level.CheckPoint<LavaPipe>(position + new Vec2(0f, -12f)) != null)
                {
                    if (floating == true && _sprite.currentAnimation != "idleBottom" && _sprite.currentAnimation != "startBottom")
                    {
                        _sprite.SetAnimation("startBottom");
                    }
                    if (floating == true && _sprite.currentAnimation != "idleBottom" && _sprite.currentAnimation == "startBottom" && _sprite.finished)
                    {
                        _sprite.SetAnimation("idleBottom");
                    }
                    if (floating == false && _sprite.currentAnimation != "nullBottom")
                    {
                        _sprite.SetAnimation("nullBottom");
                    }
                }
                else
                {
                    if (floating == true && _sprite.currentAnimation != "idleTop" && _sprite.currentAnimation != "startTop")
                    {
                        _sprite.SetAnimation("startTop");
                    }
                    if (floating == true && _sprite.currentAnimation != "idleTop" && _sprite.currentAnimation == "startTop" && _sprite.finished)
                    {
                        _sprite.SetAnimation("idleTop");
                    }
                    if (floating == false && _sprite.currentAnimation != "nullTop")
                    {
                        _sprite.SetAnimation("nullTop");
                    }
                }
            }
        }
        public override void Draw()
        {
            graphic.flipH = flipHorizontal;
            base.Draw();
        }
    }
}
