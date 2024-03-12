using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Blocks")]
    public class grecePlatform : AutoPlatform
    {
        public grecePlatform(float x, float y) : base(x, y, GetPath<DuckUnbreakable>("Sprites/Tilesets/Greek/grecePlatform.png"))
        {
            _editorName = "Grece Scaffold";
            physicsMaterial = PhysicsMaterial.Wood;
            verticalWidth = 14f;
            verticalWidthThick = 15f;
            horizontalHeight = 8f;
            _hasNubs = true;
            _collideBottom = true;
        }
    }
}
