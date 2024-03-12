using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class ThemePlayer : Thing
    {
        public static string standname = "";
        public bool init = false;
    
        public NetSoundEffect _netLoad1 = new NetSoundEffect(new string[]
        {
            GetPath<DuckUnbreakable>("SFX/JotaroTheme.wav"),
        });

        public NetSoundEffect _netLoad2 = new NetSoundEffect(new string[]
        {
            GetPath<DuckUnbreakable>("SFX/JosukeTheme.wav"),
        });

        public NetSoundEffect _netLoad3 = new NetSoundEffect(new string[]
        {
            GetPath<DuckUnbreakable>("SFX/GiornoTheme.wav"),
        });

        public NetSoundEffect _netLoad4 = new NetSoundEffect(new string[]
        {
            GetPath<DuckUnbreakable>("SFX/JolyneTheme.wav"),
        });


        public ThemePlayer(string StandName) : base(0f, 0f)
        {
            standname = StandName;
        }

        public override void Update()
        {
            base.Update();
            if (init == false)
            {
                if (standname == "EMP" || standname == "HP" || standname == "HG" || standname == "MR" || standname == "SC"
                    || standname == "SP" || standname == "TW")
                {
                    SFX.Play(GetPath("SFX/JotaroTheme.wav"), 1f, 0f, 0f, false);
                }
                else if (standname == "CD" || standname == "EA" || standname == "KQ" || standname == "TH" || standname == "DS")
                {
                    SFX.Play(GetPath("SFX/JosukeTheme.wav"), 1f, 0f, 0f, false);
                }
                else if (standname == "AER" || standname == "MTL" || standname == "PH" || standname == "TGD")
                {
                    SFX.Play(GetPath("SFX/GiornoTheme.wav"), 1f, 0f, 0f, false);
                }
                else if (standname == "MT" || standname == "WS")
                {
                    SFX.Play(GetPath("SFX/JolyneTheme.wav"), 1f, 0f, 0f, false);
                }
                init = true;
            }
        }
    }
}
