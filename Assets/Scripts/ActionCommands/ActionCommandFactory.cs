using UnityEngine;
class ActionCommandFactory {

    //Singleton bs
    private static ActionCommandFactory instance = null;
    public static ActionCommandFactory GetInstance() {
        if (instance == null) {
            instance = new ActionCommandFactory();
        }
        return instance;
    }
    private ActionCommandFactory() {}
    //End signleton bs

    public MeleeAttackActionCommand CreateMeleeAttack() {
        return new MeleeAttackActionCommand(2, 
            GameObject.Find("TargetingUI").GetComponent<TargetingController>(),
            GameObject.Find("Player").GetComponent<PlayerController>(),
            GameObject.Find("Player").GetComponent<Player>());
    }

	public KeyActionCommand CreateKeyAction(Color color) {
		return new KeyActionCommand (color,
			GameObject.Find ("TargetingUI").GetComponent<TargetingController> (),
			GameObject.Find ("Player").GetComponent<PlayerController> (),
            GameObject.Find("Player").GetComponent<Player>());
	}

	public NullActionCommand CreateNullAction() {
		return new NullActionCommand ();
	}
}

