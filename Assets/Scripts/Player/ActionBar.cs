using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class ActionBar : MonoBehaviour {
    private List<ActionCommand> actions;
    private Transform slots;

    void Awake() {
        actions = new List<ActionCommand>();
        slots = transform.GetChild(2).transform;
        for (int i = 0; i < 4; i++) {
            actions.Add(new NullActionCommand());
        }
    }
    
    void UpdateImages() {
        for (int i = 0; i < 4; i++) {
            Debug.Log(i);
            slots.GetChild(i).GetComponent<Image>().sprite = actions[i].GetSprite();
        }
    }
    public void SetActions(List<ActionCommand> actions) {
        this.actions = actions;
    }

    public List<ActionCommand> GetActions() {
        return actions;
    }

    public void Use(int index) {
        actions[index].Execute();
    }

    public ActionCommand Equip(ActionCommand command, int index) {
        ActionCommand temp = actions[index];
        actions[index] = command;
        UpdateImages();
        return temp;
    }
    
}

