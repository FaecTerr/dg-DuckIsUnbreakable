using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    internal class DuckUnbreakableMain : IAutoUpdate
    {
        private static string[] blacklist_levels = new string[]
        {
            "DuckGame.TitleScreen",
            "DuckGame.HighlightLevel",
            "DuckGame.RockIntro",
            "DuckGame.TeamSelect2",
            "DuckGame.ArcadeLevel"
        };

        private Mod _parentmod;
        private Level lastlevel;
        private bool give_stands;
        private List<Duck> doneDucks;

        public DuckUnbreakableMain(Mod parent)
        {
            _parentmod = parent;
            AutoUpdatables.Add(this);
            doneDucks = new List<Duck>();
        }
        public void Update()
        {
            if (Level.current != lastlevel)
            {
                string lstring = Level.current.ToString();
                bool validLevel = true;
                foreach (string mstring in blacklist_levels)
                {
                    if (lstring == mstring)
                    {
                        validLevel = false;
                        break;
                    }
                }
                if (TeamSelect2.Enabled("STANDS", false) && validLevel)
                {
                    give_stands = true;
                    doneDucks.Clear();
                }
                lastlevel = Level.current;
            }
            if (give_stands)
            {
                int dcount = 0;
                foreach (Thing thing in Level.current.things[typeof(Duck)])
                {
                    Duck d = (Duck)thing;
                    dcount++;
                    if (d.localSpawnVisible && !doneDucks.Contains(d))
                    {
                        if (!d.isServerForObject || InputProfile.core._virtualProfiles.ContainsValue(d.inputProfile))
                        {
                            doneDucks.Add(d);
                        }
                        else
                        {
                            Stand newstand = null;
                            switch (Rando.Int(20))
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
                                case 19:
                                    newstand = new Ratt(0f, 0f);
                                    break;
                                case 20:
                                    newstand = new SexPistols(0f, 0f);
                                    break;
                            }
                            if (newstand != null)
                            {
                                newstand.position = d.position;
                                Level.Add(newstand);
                                d.GiveHoldable(newstand);
                            }
                            doneDucks.Add(d);
                        }
                    }
                }
                if (doneDucks.Count == dcount && dcount > 0)
                {
                    give_stands = false;
                }
            }
        }
    }
}
