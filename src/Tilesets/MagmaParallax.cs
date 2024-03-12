using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Tiles|Parallax")]
    public class MagmaBackground : BackgroundUpdater
    {
        public MagmaBackground(float xpos, float ypos) : base(xpos, ypos)
        {
            graphic = new SpriteMap(GetPath("Sprites/Tilesets/Magma/MagmaIcon.png"), 16, 16, false)
            {
                frame = 0
            };
            center = new Vec2(8f, 8f);
            _collisionSize = new Vec2(16f, 16f);
            _collisionOffset = new Vec2(-8f, -8f);
            base.depth = 0.9f;
            base.layer = Layer.Foreground;
            _visibleInGame = false;
            _editorName = "Magma PL";
        }
        
        public override void Initialize()
        {
            if (Level.current is Editor)
            {
                return;
            }
            backgroundColor = new Color(241, 100, 31);
            Level.current.backgroundColor = backgroundColor;
            _parallax = new ParallaxBackground(GetPath("Sprites/Tilesets/Magma/lavaSky.png"), 0f, 0f, 3);
            float speed = 0.2f;
            _parallax.AddZone(0, 0.9f, speed, false, true);
            _parallax.AddZone(1, 0.9f, speed, false, true);
            _parallax.AddZone(2, 0.9f, speed, false, true);
            _parallax.AddZone(3, 0.9f, speed, false, true);
            _parallax.AddZone(4, 0.9f, speed, false, true);
            _parallax.AddZone(5, 0.9f, speed, false, true);
            _parallax.AddZone(6, 0.9f, speed, false, true);
            _parallax.AddZone(7, 0.9f, speed, false, true);
            _parallax.AddZone(8, 0.9f, speed, false, true);
            _parallax.AddZone(9, 0.9f, speed, false, true);
            _parallax.AddZone(10, 0.8f, speed, false, true);
            _parallax.AddZone(11, 0.7f, speed, false, true);
            _parallax.AddZone(12, 0.6f, speed, false, true);
            _parallax.AddZone(13, 0.5f, speed, false, true);
            _parallax.AddZone(14, 0.4f, speed, false, true);
            _parallax.AddZone(15, 0.3f, speed, false, true);
            Vec2 value = new Vec2(0f, -12f);
            Sprite sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/bigRock1_reflection.png"), 0f, 0f);
            sprite.depth = -0.9f;
            sprite.position = new Vec2(-30f, 113f) + value;
            _parallax.AddZoneSprite(sprite, 12, 0f, 0f, true);
            sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/bigRock1.png"), 0f, 0f);
            sprite.depth = -0.8f;
            sprite.position = new Vec2(-31f, 50f) + value;
            _parallax.AddZoneSprite(sprite, 12, 0f, 0f, true);
            sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/bigRock2_reflection.png"), 0f, 0f);
            sprite.depth = -0.9f;
            sprite.position = new Vec2(210f, 108f) + value;
            _parallax.AddZoneSprite(sprite, 12, 0f, 0f, true);
            sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/bigRock2.png"), 0f, 0f);
            sprite.depth = -0.8f;
            sprite.position = new Vec2(211f, 52f) + value;
            _parallax.AddZoneSprite(sprite, 12, 0f, 0f, true);
            sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/rock1_reflection.png"), 0f, 0f);
            sprite.depth = -0.9f;
            sprite.position = new Vec2(119f, 131f) + value;
            _parallax.AddZoneSprite(sprite, 13, 0f, 0f, true);
            sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/rock1.png"), 0f, 0f);
            sprite.depth = -0.8f;
            sprite.position = new Vec2(121f, 114f) + value;
            _parallax.AddZoneSprite(sprite, 13, 0f, 0f, true);
            value = new Vec2(-30f, -20f);
            sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/rock2_reflection.png"), 0f, 0f);
            sprite.depth = -0.9f;
            sprite.position = new Vec2(69f, 153f) + value;
            _parallax.AddZoneSprite(sprite, 14, 0f, 0f, true);
            sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/rock2.png"), 0f, 0f);
            sprite.depth = -0.8f;
            sprite.position = new Vec2(71f, 154f) + value;
            _parallax.AddZoneSprite(sprite, 14, 0f, 0f, true);
            value = new Vec2(200f, 2f);
            sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/rock3_reflection.png"), 0f, 0f);
            sprite.depth = -0.9f;
            sprite.position = new Vec2(70f, 153f) + value;
            _parallax.AddZoneSprite(sprite, 15, 0f, 0f, true);
            sprite = new Sprite(GetPath("Sprites/Tilesets/Magma/rock3.png"), 0f, 0f);
            sprite.depth = -0.8f;
            sprite.position = new Vec2(71f, 154f) + value;
            _parallax.AddZoneSprite(sprite, 15, 0f, 0f, true);
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