using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PlayerState {
    public List<ActionCommand> actions;

    public int currentHealth;

    public PlayerState(List<ActionCommand> actions, int currentHealth) {
        this.actions = actions;
        this.currentHealth = currentHealth;
    }

}

