using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff")]
    public class StandArrow : Holdable
    {
        public SpriteMap _sprite;
        private List<Duck> doneDucks = new List<Duck>();
        public StandArrow(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(4.5f, 7f);
            collisionOffset = new Vec2(-3f, -6f);
            collisionSize = new Vec2(6f, 12f);
            _sprite = new SpriteMap(GetPath("Sprites/Stands/StandArrow.png"), 9, 14, false);
            base.graphic = _sprite;
        }
        public override void Update()
        {
            base.Update();
            if(owner != null)
            {
                Duck d = owner as Duck;
                if (d.inputProfile.Pressed("SHOOT"))
                {
                    OnActivate(owner as Duck);
                }
            }
            if(owner == null)
            {
                if(grounded)
                {
                    _sprite.angleDegrees = 0;
                }
                else
                {
                    _sprite.angleDegrees = 90;
                }
            }
        }
        public virtual void OnActivate(Duck d)
        {
            if(d != null)
            {
                if (!doneDucks.Contains(d) && !(d.HasEquipment(typeof(Stand))))
                {
                    int i = Rando.Int(1);
                    if (i == 1)
                    {
                        Stand newstand = null;
                        switch (Rando.Int(17))
                        {
                            case 0:
                                newstand = new CrazyDiamond(0f, 0f);
                                break;
                            case 1:
                                newstand = new DiskSpace(0f, 0f);
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
                            case 17:
                                newstand = new WhiteSnake(0f, 0f);
                                break;
                            case 18:
                                newstand = new BDTH(0f, 0f);
                                break;
                        }
                        if (newstand != null)
                        {
                            newstand.position = d.position;
                            Level.Add(newstand);
                            d.Equip(newstand);
                        }
                        doneDucks.Add(d);
                    }
                    else
                    {
                        d.Kill(new DTFall());
                    }
                }
            }
        }
    }
}
