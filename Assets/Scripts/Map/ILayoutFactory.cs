using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Map {
    interface ILayoutFactory {
        RoomLayout createLayout();
    }
}
