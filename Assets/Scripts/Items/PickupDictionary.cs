using System.Collections.Generic;
using UnityEngine;

public class PickupDictionary {
	private Dictionary<string, ActionCommand> dict;
	private ActionCommandFactory acf;
	//Singleton junk
	private static PickupDictionary instance = null;
	public static PickupDictionary GetInstance() {
		if (instance == null) {
			instance = new PickupDictionary ();
		}
		return instance;
	}
	//EndSingleton junk

	private PickupDictionary ()	{
		acf = ActionCommandFactory.GetInstance ();
		dict = new Dictionary<string, ActionCommand> ();
		dict.Add("MagentaKey", acf.CreateKeyAction(Color.magenta));
		dict.Add ("RedKey", acf.CreateKeyAction (Color.red));
			
	}
	public ActionCommand Get(string actionName) {
		return dict [actionName];
	}

}


