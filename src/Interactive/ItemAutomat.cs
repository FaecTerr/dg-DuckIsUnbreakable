using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.DuckUnbreakable
{
    [EditorGroup("Faecterr's|Stuff")]
    public class ItemAutomat : Thing, IContainAThing
    {
        public PhysicsObject _containedObject1;
        public PhysicsObject _containedObject2;
        public PhysicsObject _containedObject3;
        private Sprite _none;
        private Sprite _projection1;
        private Sprite _projection2;
        private Sprite _projection3;
        public SpriteMap _sprite;

        public ItemAutomat(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<DuckUnbreakable>("Sprites/Tilesets/ItemAutomat.png"), 34, 50, true);
            _sprite.frame = 0;
            graphic = _sprite;
            center = new Vec2(17f, 25f);
            collisionSize = new Vec2(32f, 48f);
            collisionOffset = new Vec2(-16f, -24f);
            base.depth = -0.75f;
            _canFlip = true;
            _none = new Sprite("none", 0f, 0f);
            _none.center = new Vec2(8f, 8f);
            _none.alpha = 0.7f;
            _projection1 = _none;
            _projection2 = _none;
            _projection3 = _none;
        }
        public Type contains { get; set; }
        public Type contains2 { get; set; }
        public Type contains3 { get; set; }
        public override void Update()
        {
            base.Update();
            if (containedObject1 == null)
            {
                List<Type> things = ItemBox.GetPhysicsObjects(Editor.Placeables);
                things.RemoveAll((Type t) => t == typeof(LavaBarrel) || t == typeof(Grapple));
                contains = things[NetRand.Int(things.Count - 1)];
                PhysicsObject newThing = Editor.CreateThing(contains) as PhysicsObject;
                if (Rando.Int(1000) == 1 && newThing is Gun && (newThing as Gun).CanSpawnInfinite())
                {
                    (newThing as Gun).infiniteAmmoVal = true;
                    (newThing as Gun).infinite.value = true;
                }
                newThing.position = position;
                containedObject1 = newThing;
            }
            if (containedObject2 == null)
            {
                List<Type> things = ItemBox.GetPhysicsObjects(Editor.Placeables);
                things.RemoveAll((Type t) => t == typeof(LavaBarrel) || t == typeof(Grapple));
                contains2 = things[NetRand.Int(things.Count - 1)];
                PhysicsObject newThing = Editor.CreateThing(contains2) as PhysicsObject;
                if (Rando.Int(1000) == 1 && newThing is Gun && (newThing as Gun).CanSpawnInfinite())
                {
                    (newThing as Gun).infiniteAmmoVal = true;
                    (newThing as Gun).infinite.value = true;
                }
                newThing.position = position;
                containedObject2 = newThing;
            }
            if (containedObject3 == null)
            {
                List<Type> things = ItemBox.GetPhysicsObjects(Editor.Placeables);
                things.RemoveAll((Type t) => t == typeof(LavaBarrel) || t == typeof(Grapple));
                contains3 = things[NetRand.Int(things.Count - 1)];
                PhysicsObject newThing = Editor.CreateThing(contains3) as PhysicsObject;
                if (Rando.Int(1000) == 1 && newThing is Gun && (newThing as Gun).CanSpawnInfinite())
                {
                    (newThing as Gun).infiniteAmmoVal = true;
                    (newThing as Gun).infinite.value = true;
                }
                newThing.position = position;
                containedObject3 = newThing;
            }
            if (containedObject1.graphic != null)
                _projection1 = containedObject1.graphic;
            if (containedObject2.graphic != null)
                _projection2 = containedObject2.graphic;
            if (containedObject3.graphic != null)
                _projection3 = containedObject3.graphic;
            foreach (SmallCoin coin in Level.CheckRectAll<SmallCoin>(topLeft, bottomRight))
            {
                if (coin != null)
                {
                    Level.Remove(coin);
                    int i = Rando.Int(1, 3);
                    if (i == 1 && containedObject1 != null)
                    {
                        SpawnItem(containedObject1);
                        Level.Add(containedObject1);
                        containedObject1 = null;
                    }
                    if (i == 2 && containedObject2 != null)
                    {
                        SpawnItem(containedObject2);
                        Level.Add(containedObject2);
                        containedObject2 = null;
                    }
                    if (i == 3 && containedObject3 != null)
                    {
                        SpawnItem(containedObject3);
                        Level.Add(containedObject3);
                        containedObject3 = null;
                    }
                }
            }

        }
        public void SpawnItem(PhysicsObject obj)
        {
            if (obj != null)
            {
                Level.Add(obj);
            }
        }
        public PhysicsObject containedObject1
        {
            get
            {
                return _containedObject1;
            }
            set
            {
                _containedObject1 = value;
            }
        }
        public PhysicsObject containedObject2
        {
            get
            {
                return _containedObject2;
            }
            set
            {
                _containedObject2 = value;
            }
        }
        public PhysicsObject containedObject3
        {
            get
            {
                return _containedObject3;
            }
            set
            {
                _containedObject3 = value;
            }
        }
        /*public virtual PhysicsObject GetSpawnItem()
        {

            return ;
        }*/

        public override void Draw()
        {
            base.Draw();
            if (_projection1 != null)
            {
                float divideW = 15f / _projection1.width;
                float divideH = 8f / _projection1.height;
                _projection1.alpha = 0.6f;
                //_projection1.xscale = divideW;
                //_projection1.yscale = divideH;
                Graphics.Draw(_projection1, position.x+5f, position.y - 12f);
            }
            else
            {
                _projection1 = _none;
                _projection1.alpha = 0.6f;
                Graphics.Draw(_projection1, position.x + 5f, position.y - 12f);
            }
            if (_projection2 != null)
            {
                float divideW = 15f / _projection2.width;
                float divideH = 8f / _projection2.height;
                _projection2.alpha = 0.6f;
                //_projection2.xscale = divideW;
                //_projection2.yscale = divideH;
                Graphics.Draw(_projection2, position.x+5f, position.y);
            }
            else
            {
                _projection2 = _none;
                _projection2.alpha = 0.6f;
                Graphics.Draw(_projection2, position.x + 5f, position.y - 12f);
            }
            if (_projection3 != null)
            {
                float divideW = 15f / _projection3.width;
                float divideH = 8f / _projection3.height;
                _projection3.alpha = 0.6f;
                //_projection3.xscale = divideW;
                //_projection3.yscale = divideH;
                Graphics.Draw(_projection3, position.x + 5f, position.y + 12f);
            }
            else
            {
                _projection3 = _none;
                _projection3.alpha = 0.6f;
                Graphics.Draw(_projection3, position.x + 5f, position.y - 12f);
            }
        }
    }
}
