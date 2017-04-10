using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class ActionBar : MonoBehaviour {
    private List<ActionCommand> actions;
    private Transform slots;

    void Awake() {
        actions = new List<ActionCommand>();
        slots = transform.GetChild(1).transform;
        for (int i = 0; i < 4; i++) {
            actions.Add(new NullActionCommand());
        }
    }
    
    void UpdateImages() {
        for (int i = 0; i < 4; i++) {
            slots.GetChild(i).GetComponent<Image>().sprite = actions[i].GetSprite();
        }
    }
    public void SetActions(List<ActionCommand> actions) {
		Debug.Log ("SETTING ACTIONS to ");

        this.actions = actions;
		for (int i = 0; i < 4; i++) {
			Debug.Log ("\t" + actions [i].GetName ());
		}
        UpdateImages();
    }

    public List<ActionCommand> GetActions() {
        return actions;
    }

    public void Use(int index) {
		Debug.Log ("Index: " + index);
		for (int i = 0; i < 4; i++) {
			Debug.Log ("\t" + actions [i].GetName ());
		}
        actions[index].Execute();
    }

    public ActionCommand Equip(ActionCommand command, int index) {
        ActionCommand temp = actions[index];
        actions[index] = command;
        UpdateImages();
        return temp;
    }
    
}

