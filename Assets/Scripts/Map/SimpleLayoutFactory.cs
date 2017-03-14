using UnityEngine;

namespace Assets.Scripts.Map {
    class SimpleLayoutFactory : ILayoutFactory{
        private AreaFactory areaFactory;
        private int width;
        private int height;

        public SimpleLayoutFactory(string areaString, int width, int height) {
            this.width = width;
            this.height = height;
            this.areaFactory = GameObject.Find(areaString).GetComponent<AreaFactory>();
        }
        public RoomLayout createLayout() {
            return new SimpleSquareRoomLayout(areaFactory, width, height);
        }
    }
}
