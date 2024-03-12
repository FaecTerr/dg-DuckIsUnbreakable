using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class TimeStopMarker : Thing, IDrawToDifferentLayers
    {
        public float Time = 0.3f;
        public int fram;
        public void OnDrawLayer(Layer layer)
        {
            if(layer == Layer.Foreground)
            {
                Time -= 0.01f;
                if(Time <= 0)
                {
                    Level.Remove(this);
                }
                else
                {
                    SpriteMap vfx = new SpriteMap(GetPath("Sprites/Stands/TimeStopPlay.png"), 16, 16);
                    vfx.scale = new Vec2(Level.current.camera.size.y * 0.1f * (1- Time), Level.current.camera.size.y * 0.1f * (1 - Time));
                    vfx.alpha = 1f - (0.3f - Time) * 2;
                    vfx.frame = fram;
                    vfx.CenterOrigin();
                    Graphics.Draw(vfx, Level.current.camera.position.x + Level.current.camera.size.x / 2, Level.current.camera.position.y + Level.current.camera.size.y / 2, 1);
                }
            }
        }
    }
}
