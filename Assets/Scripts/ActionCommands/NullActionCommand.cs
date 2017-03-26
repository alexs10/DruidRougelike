using System;

public class NullActionCommand: ActionCommand {
	public void Execute() {
		//Do nothing because this is a null action command
	}

    public string GetName() {
        return "Null";
    }
}


