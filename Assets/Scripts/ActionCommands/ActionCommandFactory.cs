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

	public ActionCommand CreateFromName(string name) {
		switch (name) {
		case "Smack":
			return CreateMeleeAttack ();
		case "Push":
			return CreatePushAttack ();
		case "Shoot":
			return CreateRangedAttack ();
		default:
			return new NullActionCommand ();
		}
	}

    public MeleeAttackActionCommand CreateMeleeAttack() {
        return new MeleeAttackActionCommand(5, 
            GameObject.Find("TargetingUI").GetComponent<TargetingController>(),
            GameObject.Find("Player").GetComponent<PlayerController>(),
            GameObject.Find("Player").GetComponent<Player>());
    }


	public RangedAttackActionCommand CreateRangedAttack() {
		return new RangedAttackActionCommand(3,
			3,
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

	public PushAttackActionCommand CreatePushAttack() {
		return new PushAttackActionCommand( 1,
			2,
			GameObject.Find ("TargetingUI").GetComponent<TargetingController> (),
			GameObject.Find ("Player").GetComponent<PlayerController> (),
			GameObject.Find("Player").GetComponent<Player>());
	}

	public NullActionCommand CreateNullAction() {
		return new NullActionCommand ();
	}
}

