using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Special|Traps")]
    public class MovingSpikes : Thing, IWirePeripheral
    {
        public SpriteMap _sprite;
        public EditorProperty<int> Direction;
        public EditorProperty<bool> Auto;
        private float charge = 0f;
        private new bool active = false;

        public MovingSpikes(float xpos, float ypos) : base(xpos, ypos, null)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Tilesets/TriggerSpikes.png"), 16, 32, false);
            graphic = _sprite;
            center = new Vec2(8f, 16f);
            collisionOffset = new Vec2(-8f, -16f);
            collisionSize = new Vec2(16f, 32f);
            hugWalls = (WallHug.Floor | WallHug.Ceiling | WallHug.Left | WallHug.Right);
            _sprite.AddAnimation("idle", 0.1f, true, new int[] {
                0
            });
            _sprite.AddAnimation("move", 1f, false, new int[] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7
            });
            _sprite.AddAnimation("back", 1f, false, new int[] {
                7,
                6,
                5,
                4,
                3,
                2,
                1,
                0
            });
            base.depth = -0.5f;
            _sprite.SetAnimation("idle");
            _editorName = "Wire Spikes";
            base.layer = Layer.Foreground;
            Direction = new EditorProperty<int>(1, this, 1f, 4f, 1f, null);
            Auto = new EditorProperty<bool>(false);
        }
        public override void EditorPropertyChanged(object property)
        {
            base.EditorPropertyChanged(property);
            angleDegrees = 90f * Direction - 90f;
            if (Direction == 1 || Direction == 3)
            {
                collisionSize = new Vec2(16f, 32f);
                _collisionOffset = new Vec2(-8f, -16f);
            }
            if (Direction == 2 || Direction == 4)
            {
                collisionSize = new Vec2(32f, 16f);
                _collisionOffset = new Vec2(-16f, -8f);
            }
        }
        public override void Update()
        {
            base.Update();
            Vec2 dir = new Vec2(0f, 0f);
            if (Direction == 1)
            {
                dir = new Vec2(0f, -1f);
            }
            if (Direction == 2)
            {
                dir = new Vec2(1f, 0f);
            }
            if (Direction == 3)
            {
                dir = new Vec2(0f, 1f);
            }
            if (Direction == 4)
            {
                dir = new Vec2(-1f, 0f);
            }
            Duck duck = Level.CheckLine<Duck>(position - dir * 8f, position + dir * 16f);
            if (duck != null && Auto == true && active == false)
            {
                active = true;
                charge = 1f;
            }
            if (charge > 0f && active == true)
            {
                charge -= 0.05f;
                if (charge <= 0f)
                {
                    action = true;
                    active = false;
                }
            }
            else if (active == false && charge < 1f)
            {
                charge += 0.04f;
            }
            else if (active == false && charge >= 1f)
            {
                action = false;
            }
            angleDegrees = 90f * Direction - 90f;
            if (action == true)
            {
                if (_sprite.currentAnimation == "idle")
                    _sprite.SetAnimation("move");
                if (_sprite.currentAnimation == "back")
                    _sprite.SetAnimation("move");
                foreach (Duck d in Level.CheckLineAll<Duck>(position, position + dir*16f))
                {
                    if (d != null)
                    {
                        d.Kill(new DTFall());
                    }
                }
                foreach (Ragdoll d in Level.CheckLineAll<Ragdoll>(position, position + dir * 16f))
                {
                    if (d != null)
                    {
                        d.Killed(new DTFall());
                    }
                }
            }
            if (action == false)
            {
                if (_sprite.currentAnimation == "move")
                    _sprite.SetAnimation("back");
            }
        }
        public void Pulse(int type, WireTileset wire)
        {
            if (type == 0)
            {
                action = true;
                Vec2 dir = new Vec2(0f, 0f);
                if (Direction == 1)
                {
                    dir = new Vec2(0f, -1f);
                }
                if (Direction == 2)
                {
                    dir = new Vec2(1f, 0f);
                }
                if (Direction == 3)
                {
                    dir = new Vec2(0f, 1f);
                }
                if (Direction == 4)
                {
                    dir = new Vec2(-1f, 0f);
                }
                foreach (Duck d in Level.CheckLineAll<Duck>(position - dir * 8f, position + dir * 8f))
                {
                    if (d != null)
                    {
                        d.Kill(new DTFall());
                    }
                }
                action = false;
                return;
            }
            if (type == 1)
            {
                action = true;
                return;
            }
            if (type == 2)
            {
                action = false;
            }
        }
    }
}
