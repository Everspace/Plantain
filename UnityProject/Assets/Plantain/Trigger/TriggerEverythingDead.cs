using Plantain;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers when all the game objects in it's list are gone.
/// </summary>
[AddComponentMenu("Plantain/Trigger/Death Checker")]
public class TriggerEverythingDead : Trigger {

    /// <summary>
    /// All the things that must be gone for this to trigger.
    /// </summary>
    public List<GameObject> thingsToDie;

    void Awake() {
        if(thingsToDie == null) {
            thingsToDie = new List<GameObject>();
        }
    }

    void FixedUpdate() {
        if(isActive) {
            thingsToDie.RemoveAll(i => i == null);

            if(thingsToDie.Count <= 0) {
                Fire();
                isActive = false;
            }
        }
    }

    void OnDrawGizmos() {
        TriggerGizmos("EverythingDead");
        GizmoTargetedObjects(thingsToDie);
    }

}
