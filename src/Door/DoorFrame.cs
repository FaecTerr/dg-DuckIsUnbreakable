using System;

namespace DuckGame.DuckUnbreakable
{

    public class DUDoorFrame : Thing
    {

        public DUDoorFrame(float xpos, float ypos) : base(xpos, ypos, null)
        {
            graphic = new Sprite(GetPath("Sprites/Things/KCDoors/doorFrame.png"), 0f, 0f);
            center = new Vec2(5.5f, 26f);
            base.depth = -0.95f;
            _editorCanModify = false;
        }
    }
}
