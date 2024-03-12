using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    //[EditorGroup("Faecterr's|Details")]
    public class CameraFall : PhysicsObject
    {
        public float togo = 90;
        public bool init;
        public float initialDelay = 5f;

        public CameraFall(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            thickness = 0;
            hugWalls = WallHug.Floor;
        }

        public override void Initialize()
        {
            if (!(Level.current is Editor))
            {
                //Level.current.camera.position = new Vec2(Level.current.camera.position.x, Level.current.camera.position.y - togo);
            }
            base.Initialize();
        }

        public override void Update()
        {
            if(!(Level.current is Editor))
            {
                if (!init)
                {
                    Level.current.camera.position = new Vec2(Level.current.camera.position.x, Level.current.camera.position.y - togo);
                    init = true;
                }
            }

            if (initialDelay <= 0)
            {
                if (togo > 0)
                {
                    float go = togo * 0.002f;
                    if (go < 0.2f)
                    {
                        go = 0.2f;
                    }
                    togo -= go;

                    Level.current.camera.position = new Vec2(Level.current.camera.position.x, Level.current.camera.position.y + go);
                }
            }
            else
            {
                initialDelay -= 0.01666666f;
            }
            base.Update();
        }
    }
}
