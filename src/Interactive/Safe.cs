using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Details")]
    public class Safe : Block
    {
        SpriteMap _sprite;
        bool hitted;
        public Safe(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Blocks/Safe.png"), 16, 16, false);
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(14f, 14f);
            collisionOffset = new Vec2(-7f, -7f);
            graphic = _sprite;
            thickness = 6;
            hugWalls = WallHug.Floor;
        }
        public override void Update()
        {
            base.Update();
            if(Level.CheckRect<ExplosionPart>(topLeft, bottomRight)!= null && hitted == false) {
                {
                    hitted = true;
                    _sprite.frame = 1;
                    Stand newstand = null;
                    if (base.isServerForObject)
                    {
                        switch (Rando.Int(16))
                        {
                            case 0:
                                newstand = new CrazyDiamond(0f, 0f);
                                break;
                            case 1:
                                newstand = new WhiteSnake(0f, 0f);
                                break;
                            case 2:
                                newstand = new Emperor(0f, 0f);
                                break;
                            case 3:
                                newstand = new KillerQueen(0f, 0f);
                                break;
                            case 4:
                                newstand = new Metallica(0f, 0f);
                                break;
                            case 5:
                                newstand = new StarPlatinum(0f, 0f);
                                break;
                            case 6:
                                newstand = new TheWorld(0f, 0f);
                                break;
                            case 7:
                                newstand = new EchoAct3(0f, 0f);
                                break;
                            case 8:
                                newstand = new MagiciansR(0f, 0f);
                                break;
                            case 9:
                                newstand = new HermitPurple(0f, 0f);
                                break;
                            case 10:
                                newstand = new Aerosmith(0f, 0f, null);
                                break;
                            case 11:
                                newstand = new SilverChariot(0f, 0f, null);
                                break;
                            case 12:
                                newstand = new TheHand(0f, 0f);
                                break;
                            case 13:
                                newstand = new HierophantGreen(0f, 0f);
                                break;
                            case 14:
                                newstand = new ManhattanTransfer(0f, 0f, null);
                                break;
                            case 15:
                                newstand = new TheGratefulDead(0f, 0f);
                                break;
                            case 16:
                                newstand = new PurpleHaze(0f, 0f);
                                break;
                        }
                    }
                    if (newstand != null)
                    {
                        newstand.position = position;
                        if (base.isServerForObject)
                            Level.Add(newstand);
                    }
                }
            }
        }
    }
}
