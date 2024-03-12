using System;

namespace DuckGame.DuckUnbreakable
{
    public abstract class ForegroundTile : Thing, IStaticRender, IDontUpdate
    {
        private bool cheap;
        public bool isFlipped;
        public bool oppositeSymmetry;

        public virtual void DoPositioning()
        {
            cheap = true;
            graphic.position = position;
            graphic.scale = base.scale;
            graphic.center = center;
            graphic.depth = base.depth;
            graphic.alpha = base.alpha;
            graphic.angle = angle;
            (graphic as SpriteMap).UpdateFrame(false);
        }

        public ForegroundTile(float xpos, float ypos) : base(xpos, ypos, null)
        {
            base.layer = Layer.Foreground;
            _canBeGrouped = true;
            _isStatic = true;
            _opaque = true;
            if (Level.flipH)
            {
                flipHorizontal = true;
            }
        }
        public override BinaryClassChunk Serialize()
        {
            BinaryClassChunk element = base.Serialize();
            element.AddProperty("frame", (graphic as SpriteMap).frame);
            return element;
        }

        public override bool Deserialize(BinaryClassChunk node)
        {
            base.Deserialize(node);
            (graphic as SpriteMap).frame = node.GetProperty<int>("frame");
            return true;
        }


        public override void Initialize()
        {
            if (Level.current is Editor)
            {
                cheap = false;
                return;
            }
            DoPositioning();
        }

        public override ContextMenu GetContextMenu()
        {
            return null;
        }
        
        public override void Draw()
        {
            graphic.flipH = flipHorizontal;
            if (cheap)
            {
                graphic.UltraCheapStaticDraw(flipHorizontal);
                return;
            }
            base.Draw();
        }
    }
}
