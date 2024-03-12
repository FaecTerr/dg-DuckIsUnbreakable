using System;
using System.Collections.Generic;
using System.Linq;
//using System.Xml.Linq;
using System.Text;

namespace DuckGame.DuckUnbreakable
{
    [BaggedProperty("canSpawn", false)]
    [EditorGroup("Faecterr's|Special")]
    public class CameraFocus : Thing
    {
        private SpriteMap _sprite;
        public EditorProperty<int> wid;
        public EditorProperty<int> hig;
        public EditorProperty<int> CameraXOffset;
        public EditorProperty<int> CameraYOffset;
        public CMOffset _cmoffset;

        public CameraFocus(float xval, float yval, CMOffset offset) : base(xval, yval)
        {
            graphic = new Sprite("swirl", 0f, 0f);
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _canFlip = false;
            _visibleInGame = false;
            wid = new EditorProperty<int>(160, this, 16f, 1920f, 1f, null, false, false);
            hig = new EditorProperty<int>(160, this, 16f, 1920f, 1f, null, false, false);
            CameraXOffset = new EditorProperty<int>(0, this, -640f, 640f, 1f, null, false, false);
            CameraYOffset = new EditorProperty<int>(0, this, -640f, 640f, 1f, null, false, false);
            _cmoffset = offset;
        }

        public override void Draw()
        {
            base.Draw();
            if (Level.current is Editor)
            {
                Graphics.DrawRect(position + new Vec2(-wid / 2f, -hig / 2f), position + new Vec2(wid / 2f, hig / 2f), Color.Blue * 0.5f, 1f, false, 1f);
                Graphics.DrawLine(position, position + new Vec2(CameraXOffset, CameraYOffset), Color.Blue * 0.5f);
            }
        }

        public override void Update()
        {
            _visibleInGame = false;

            Duck d = Level.CheckRect<Duck>(position + new Vec2(-wid / 2f, -hig / 2f), position + new Vec2(wid / 2f, hig / 2f), null);
            FollowCam cam = Level.current.camera as FollowCam;
            if (d != null && !(d is TargetDuck))
            {
                if (_cmoffset == null && !(Level.current is Editor) && isServerForObject)
                {
                    _cmoffset = new CMOffset(x + CameraXOffset, y + CameraYOffset);
                    Level.Add(_cmoffset);
                }
                if (cam != null)
                {
                    cam.Add(_cmoffset);
                }
            }
            else if (d == null && cam != null)
            {
                cam.Remove(_cmoffset);
            }
            base.Update();
        }
    }
}
