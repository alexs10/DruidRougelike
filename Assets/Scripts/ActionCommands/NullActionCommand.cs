using UnityEngine;

public class NullActionCommand: ActionCommand {
	public void Execute() {
		//Do nothing because this is a null action command
	}

    public string GetName() {
        return "";
    }

	public string GetDescription() {
		return "";
	}

	public Sprite GetSprite(){
		//return Resources.Load<Sprite> ("KeySprite");
		return null;
	}
}


