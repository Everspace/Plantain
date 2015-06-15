using Plantain;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enables or Disables a GameObject based on this Performer's state.
/// </summary>
[AddComponentMenu("Plantain/Performer/Object Enabler")]
public class PerformerObjectEnabler : Performer {
    /// <summary>
    /// List of things that will be turned on or off.
    /// </summary>
    public List<GameObject> targets;

    void Awake() {
        if(targets == null) {
            targets = new List<GameObject>();
        }
    }

    override public void PerformTrigger(TriggerOption tOption) {
        base.PerformTrigger(tOption);
        if(isActive) {
            ToggleGameObjects();
        }
    }

    void ToggleGameObjects() {
        if(targets.Count > 0) {
            targets.RemoveAll(gameObj => gameObj == null);
            foreach(GameObject gameObj in targets) {
                gameObj.SetActive(state);
            }
        }
    }

    void OnDrawGizmos() {
        if(targets != null) {
            Gizmos.color = (isActive) ? (state) ? Color.green : Color.red : Color.gray;
            GizmoTargetedObjects(targets);
        }
        PerformerGizmos("Enabler");
    }
}
