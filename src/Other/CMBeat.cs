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
    public class CMBeat : Thing
    {
        public EditorProperty<string> beat;
        public EditorProperty<int> bpm;

        public float perMinute;
        public int length;

        private int tenthSec;
        private int bit;

        public CMBeat(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new Sprite("swirl", 0f, 0f);
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _canFlip = false;
            _visibleInGame = false;
            beat = new EditorProperty<string>("0001");
            bpm = new EditorProperty<int>(120, null, 60, 360, 10);
        }
        public override void Update()
        {
            base.Update();
            if(!(Level.current is Editor))
            {
                if(perMinute == 0)
                {
                    perMinute = bpm;
                }
                tenthSec++;

                if(tenthSec >= 3600/bpm)
                {
                    tenthSec = 0;
                    bit++;
                    if(bit >= beat.value.ToString().Length)
                    {
                        bit = 0;
                    }
                }

                int power = Convert.ToInt32(beat.value.ToString()[bit]) - 48;

                DevConsole.Log(Convert.ToString(power));
                DevConsole.Log(beat.value.ToString());
                Level.current.camera.position += Level.current.camera.size.x * new Vec2(0.03f * power, 0.03f * power)/2;
                Level.current.camera.size *= new Vec2(1 - 0.03f*power, 1 - 0.03f*power);
            }
        }
    }
}