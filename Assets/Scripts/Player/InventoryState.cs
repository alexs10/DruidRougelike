using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class InventoryState {
    public List<ActionCommand> items;

    public InventoryState(List<ActionCommand> items) {
        this.items = items;
    }

}

