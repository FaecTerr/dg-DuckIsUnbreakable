using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Blocks")]
    public class GreceTileset : AutoBlock
    {
        public GreceTileset(float xval, float yval) : base (xval, yval, GetPath<DuckUnbreakable>("Sprites/Tilesets/Greek/greceTileset.png"))
        {
            _editorName = "Grece";
            physicsMaterial = PhysicsMaterial.Metal;
            verticalWidth = 10f;
            verticalWidthThick = 12f;
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
