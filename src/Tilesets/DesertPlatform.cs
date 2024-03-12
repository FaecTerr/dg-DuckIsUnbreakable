using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Platform")]
    public class DesertPlatform : AutoPlatform
    {
        public DesertPlatform(float x, float y) : base(x, y, Mod.GetPath<DuckUnbreakable>("Sprites/Tilesets/Desert/Scaffolding.png"))
        {
            _editorName = "Desert Scaffold";
            physicsMaterial = PhysicsMaterial.Default;
            verticalWidth = 14f;
            verticalWidthThick = 15f;
            horizontalHeight = 8f;
            _hasNubs = true;
            _collideBottom = true;
        }
    }
}
