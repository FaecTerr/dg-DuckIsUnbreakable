using System;
using System.Collections.Generic;


namespace DuckGame.DuckUnbreakable
{
    public class NMRandomStandBox : NMEvent
    {
        public int i;
        public StandBox s;

        public NMRandomStandBox()
        {
        }

        public NMRandomStandBox(int id, StandBox box)
        {
            i = id;
            s = box;
        }

        public override void Activate()
        {
            if(s != null)
            {
                switch (i)
                {
                    case 0:
                        s.contains = typeof(CrazyDiamond);
                        break;
                    case 1:
                        s.contains = typeof(WhiteSnake);
                        break;
                    case 2:
                        s.contains = typeof(Emperor);
                        break;
                    case 3:
                        s.contains = typeof(KillerQueen);
                        break;
                    case 4:
                        s.contains = typeof(Metallica);
                        break;
                    case 5:
                        s.contains = typeof(StarPlatinum);
                        break;
                    case 6:
                        s.contains = typeof(TheWorld);
                        break;
                    case 7:
                        s.contains = typeof(EchoAct3);
                        break;
                    case 8:
                        s.contains = typeof(MagiciansR);
                        break;
                    case 9:
                        s.contains = typeof(HermitPurple);
                        break;
                    case 10:
                        s.contains = typeof(Aerosmith);
                        break;
                    case 11:
                        s.contains = typeof(SilverChariot);
                        break;
                    case 12:
                        s.contains = typeof(TheHand);
                        break;
                    case 13:
                        s.contains = typeof(HierophantGreen);
                        break;
                    case 14:
                        s.contains = typeof(ManhattanTransfer);
                        break;
                    case 15:
                        s.contains = typeof(TheGratefulDead);
                        break;
                    case 16:
                        s.contains = typeof(PurpleHaze);
                        break;
                    case 17:
                        s.contains = typeof(BDTH);
                        break;
                    case 18:
                        s.contains = typeof(DiskSpace);
                        break;
                    case 19:
                        s.contains = typeof(Ratt);
                        break;
                    case 20:
                        s.contains = typeof(SexPistols);
                        break;
                    case 21:
                        s.contains = typeof(BeachBoy);
                        break;
                }
            }
        }
    }
}