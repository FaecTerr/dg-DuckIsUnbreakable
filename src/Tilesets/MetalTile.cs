using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Blocks")]
    public class MetalTileset : AutoBlock
    {
        public MetalTileset(float xval, float yval) : base(xval, yval, GetPath<DuckUnbreakable>("Sprites/Tilesets/Metal/MetalTileset.png"))
        {
            _editorName = "Metal";
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