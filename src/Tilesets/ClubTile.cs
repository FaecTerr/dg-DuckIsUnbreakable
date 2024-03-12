using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Blocks")]
    public class ClubTileset : AutoBlock
    {
        public ClubTileset(float xval, float yval) : base(xval, yval, GetPath<DuckUnbreakable>("Sprites/Tilesets/Club/ClubStone.png"))
        {
            _editorName = "Club";
            verticalWidth = 12f;
            verticalWidthThick = 14f;
            horizontalHeight = 14f;
        }
        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}