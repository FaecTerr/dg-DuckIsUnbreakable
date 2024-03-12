using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Parallax")]
    public class ClubParallax : BackgroundUpdater
    {
        public ClubParallax(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Tilesets/Club/ClubIcon"), 16, 16, false);
            center = new Vec2(8f, 8f);
            _collisionSize = new Vec2(16f, 16f);
            _collisionOffset = new Vec2(-8f, -8f);
            base.depth = 0.9f;
            base.layer = Layer.Foreground;
            _visibleInGame = false;
            _editorName = "Club PL";
        }
        public override void Initialize()
        {
            if (Level.current is Editor)
            {
                return;
            }
            backgroundColor = new Color(186, 173, 216);
            Level.current.backgroundColor = backgroundColor;
            _parallax = new ParallaxBackground(Mod.GetPath<DuckUnbreakable>("Sprites/Tilesets/Club/Club.png"), 0f, 0f, 3);
            float speed = 0.4f;
            _parallax.AddZone(0, 0f, speed, false, true);
            _parallax.AddZone(1, 0f, speed, false, true);
            _parallax.AddZone(2, 0f, speed, false, true);
            _parallax.AddZone(3, 0f, speed, false, true);
            _parallax.AddZone(4, 0f, speed, false, true);
            _parallax.AddZone(5, 0f, speed, false, true);
            _parallax.AddZone(6, 0f, speed, false, true);
            _parallax.AddZone(7, 0f, speed, false, true);
            _parallax.AddZone(8, 0f, speed, false, true);
            _parallax.AddZone(9, 0f, speed, false, true);
            _parallax.AddZone(10, 0f, speed, false, true);
            _parallax.AddZone(11, 0f, speed, false, true);
            _parallax.AddZone(12, 0f, speed, false, true);
            _parallax.AddZone(13, 0f, speed, false, true);
            _parallax.AddZone(14, 0f, speed, false, true);
            _parallax.AddZone(15, 0f, speed, false, true);
            _parallax.AddZone(16, 0f, speed, false, true);
            _parallax.AddZone(17, 0f, speed, false, true);
            _parallax.AddZone(18, 0f, speed, false, true);
            _parallax.AddZone(19, 0f, speed, false, true);
            _parallax.AddZone(20, 0f, speed, false, true);
            _parallax.AddZone(21, 0f, speed, false, true);
            _parallax.AddZone(22, 0f, speed, false, true);
            _parallax.AddZone(23, 0f, speed, false, true);
            _parallax.AddZone(24, 0f, speed, false, true);
            _parallax.AddZone(25, 0f, speed, false, true);
            _parallax.AddZone(26, 0f, speed, false, true);
            _parallax.AddZone(27, 0f, speed, false, true);
            _parallax.AddZone(28, 0f, speed, false, true);
            _parallax.AddZone(29, 0f, speed, false, true);
            Level.Add(_parallax);
        }
        public override void Update()
        {
            Vec2 wallScissor = BackgroundUpdater.GetWallScissor();
            if (wallScissor != Vec2.Zero)
            {
                scissor = new Rectangle((float)((int)wallScissor.x), 0f, (float)((int)wallScissor.y), (float)Graphics.height);
            }
            base.Update();
        }
        public override void Terminate()
        {
            Level.Remove(_parallax);
        }
    }
}
