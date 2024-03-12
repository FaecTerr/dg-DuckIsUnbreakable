using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    class NMChangeSong : NMDuckNetworkEvent
    {
        public int musicnum;
        public NMChangeSong()
        {
        }

        public NMChangeSong(int i)
        {
            musicnum = i;
        }
        public override void Activate()
        {
            if (musicnum == 1)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAmegaman.wav"), 1f, 0f, 0.0f, true);
            }
            else if (musicnum == 2)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAducktales.wav"), 1f, 0f, 0.0f, true);
            }
            else if (musicnum == 3)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAMegadriveRick.wav"), 1f, 0f, 0.0f, true);
            }
            else if (musicnum == 4)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAchase.wav"), 1f, 0f, 0.0f, true);
            }
            else if (musicnum == 5)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAstandproud.wav"), 1f, 0f, 0.0f, true);
            }
            else if (musicnum == 6)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAtraitorsrequiem.wav"), 1f, 0f, 0.0f, true);
            }
            else if (musicnum == 7)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAfinalcountdown.wav"), 0.7f, 0f, 0.0f, true);
            }
            else if (musicnum == 8)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAspacedandy.wav"), 1f, 0f, 0.0f, true);
            }
            else if (musicnum == 9)
            {

                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAhitthefloor.wav"), 1f, 0f, 0.0f, true);

            }
            else if (musicnum == 10)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAastronomia.wav"), 1f, 0f, 0.0f, true);
            }
            else if (musicnum == 11)
            {
                SFX.StopAllSounds();
                SFX.Play(Mod.GetPath<DuckUnbreakable>("SFX/MAbeavisbutthead.wav"), 1f, 0f, 0.0f, true);
            }
            base.Activate();
        }
    }
}
