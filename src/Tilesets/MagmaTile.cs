using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Blocks")]
    public class MagmaTileset : AutoBlock
    {
        public MagmaTileset(float xval, float yval) : base(xval, yval, GetPath<DuckUnbreakable>("Sprites/Tilesets/Magma/MagmaTileset.png"))
        {
            _editorName = "Magma";
            verticalWidth = 14f;
            verticalWidthThick = 16f;
            horizontalHeight = 16f;
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