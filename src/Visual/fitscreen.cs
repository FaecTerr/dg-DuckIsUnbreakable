using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    public class fitscreen : Thing
    {
        private bool rain;
        private bool snow;
        private bool sand;
        public SpriteMap _pix;

        public fitscreen(float xval, float yval, bool rai = false, bool sno = false, bool san = false) : base(xval, yval)
        {
            rain = rai;
            snow = sno;
            sand = san;
            layer = Layer.Foreground;
            base.depth = 0.9f;
            Vec3 color = new Vec3(0, 0, 0);
            if (rain)
            {
                color = new Vec3(0, 148, 255);
            }
            if (snow)
            {
                color = new Vec3(0, 255, 255);
            }
            if (sand)
            {
                color = new Vec3(255, 106, 0);
            }
            _pix = new SpriteMap(Graphics.Recolor(GetPath("Sprites/Things/OneWhitePixElement"), color), 1, 1);
        }
        public override void Draw()
        {
            _pix.alpha = 0.3f;
            _pix.scale = new Vec2(Level.current.camera.size + new Vec2(2f, 2f));
            if (rain == true)
            {
                Graphics.Draw(_pix, Level.current.camera.position.x-1, Level.current.camera.position.y - 1, 1f);
                //Graphics.DrawRect(new Vec2(Level.current.camera.left, Level.current.camera.top), new Vec2(Level.current.camera.right, Level.current.camera.bottom), Color.Purple, 1, true, 1f);
            }
            if (snow == true)
            {
                Graphics.Draw(_pix, Level.current.camera.position.x - 1, Level.current.camera.position.y - 1, 1f);
                //Graphics.DrawRect(new Vec2(Level.current.camera.left, Level.current.camera.top), new Vec2(Level.current.camera.right, Level.current.camera.bottom), Color.Aqua, 1, true, 1f);
            }
            if (sand == true)
            {
                Graphics.Draw(_pix, Level.current.camera.position.x - 1, Level.current.camera.position.y - 1, 1f);
                //Graphics.DrawRect(new Vec2(Level.current.camera.left, Level.current.camera.top), new Vec2(Level.current.camera.right, Level.current.camera.bottom), Color.Orange, 1, true, 1f);
            }
            base.Draw();
        }
    }
}
