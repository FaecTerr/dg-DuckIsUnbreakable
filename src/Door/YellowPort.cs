using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Doors")]
    public class YellowPort : Thing
    {
        protected SpriteMap _sprite;
        public bool getOpen = false;
        public YDoor _ydoor;
        public EditorProperty<bool> flip;
        public StateBinding _ydorBinding = new StateBinding("_ydoor", -1, false, false);
        public StateBinding _openBinding = new StateBinding("getOpen", -1, false, false);
        public YellowPort(float xpos, float ypos, YDoor ydor) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Things/KCDoors/CardPortYellow.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            base.depth = -0.5f;
            _canFlip = false;
            _ydoor = ydor;
            _sprite.frame = 0;
            flip = new EditorProperty<bool>(flipHorizontal);
        }
        public override void Update()
        {
            base.Update();
            YellowKC ykc = Level.CheckCircle<YellowKC>(base.x, base.y, 5f);
            if (ykc != null)
            {
                getOpen = true;
                _sprite.frame = 1;
            }
            else
            {
                getOpen = false;
                _sprite.frame = 0;
            }
            if (base.isServerForObject)
            {
                if (!(Level.current is Editor) && base.isServerForObject && flip == true)
                {
                    if (_ydoor == null)
                    {
                        _ydoor = new YDoor(base.x + 16f, base.y + 16f);
                        Level.Add(_ydoor);
                    }
                }
                else if (!(Level.current is Editor) && base.isServerForObject)
                {
                    if (_ydoor == null)
                    {
                        _ydoor = new YDoor(base.x - 16f, base.y + 16f);
                        Level.Add(_ydoor);
                    }
                }
            }
            if (_ydoor != null)
            {
                if (getOpen == true)
                {
                    _ydoor.getOpen = true;
                }
                if (getOpen == false)
                {
                    _ydoor.getOpen = false;
                }
            }
        }
    }
}
