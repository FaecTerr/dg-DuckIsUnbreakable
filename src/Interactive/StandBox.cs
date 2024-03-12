using System;
using System.Collections.Generic;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff|Blocks")]
    public class StandBox : ItemBox
    {
        public StandBox(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Things/Blocks/StandBlock.png"), 16, 16);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _sprite.AddAnimation("idle", 1f, false, new int[] { 0 });
            editorTooltip = "Spawns a random stand each time it's used. Recharges after a short duration.";
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update()
        {
            if (_hit)
            {
                _sprite.frame = 1;
            }
            else
            {
                _sprite.frame = 0;

            }
            if (isServerForObject && ((Network.isActive && Network.isServer) || (!Network.isActive)))
            {
                switch (Rando.Int(21))
                {
                    case 0:
                        contains = typeof(CrazyDiamond);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(0, this));
                        break;
                    case 1:
                        contains = typeof(WhiteSnake);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(1, this));
                        break;
                    case 2:
                        contains = typeof(Emperor);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(2, this));
                        break;
                    case 3:
                        contains = typeof(KillerQueen);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(3, this));
                        break;
                    case 4:
                        contains = typeof(Metallica);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(4, this));
                        break;
                    case 5:
                        contains = typeof(StarPlatinum);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(5, this));
                        break;
                    case 6:
                        contains = typeof(TheWorld);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(6, this));
                        break;
                    case 7:
                        contains = typeof(EchoAct3);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(7, this));
                        break;
                    case 8:
                        contains = typeof(MagiciansR);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(8, this));
                        break;
                    case 9:
                        contains = typeof(HermitPurple);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(9, this));
                        break;
                    case 10:
                        contains = typeof(Aerosmith);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(10, this));
                        break;
                    case 11:
                        contains = typeof(SilverChariot);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(11, this));
                        break;
                    case 12:
                        contains = typeof(TheHand);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(12, this));
                        break;
                    case 13:
                        contains = typeof(HierophantGreen);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(13, this));
                        break;
                    case 14:
                        contains = typeof(ManhattanTransfer);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(14, this));
                        break;
                    case 15:
                        contains = typeof(TheGratefulDead);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(15, this));
                        break;
                    case 16:
                        contains = typeof(PurpleHaze);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(16, this));
                        break;
                    case 17:
                        contains = typeof(BDTH);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(17, this));
                        break;
                    case 18:
                        contains = typeof(DiskSpace);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(18, this));
                        break;
                    case 19:
                        contains = typeof(Ratt);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(19, this));
                        break;
                    case 20:
                        contains = typeof(SexPistols);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(20, this));
                        break;
                    case 21:
                        contains = typeof(BeachBoy);
                        DuckNetwork.SendToEveryone(new NMRandomStandBox(21, this));
                        break;

                }
            }
            base.Update();
        }
        
    }
}
