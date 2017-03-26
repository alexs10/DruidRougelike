using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PlayerState {
    public Dictionary<string, ActionCommand> actions;

    public int currentHealth;

    public PlayerState(Dictionary<string, ActionCommand> actions, int currentHealth) {
        this.actions = actions;
        this.currentHealth = currentHealth;
    }

}

