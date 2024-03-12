using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Background")]
    public class BackgroundBar : BackgroundTile
    {
        public BackgroundBar(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Tilesets/Club/barzone.png"), 16, 16, true);
            _opacityFromGraphic = true;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            _editorName = "Club";
        }
    }
}
