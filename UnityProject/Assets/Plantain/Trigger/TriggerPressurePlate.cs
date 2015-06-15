using Plantain;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Trigger triggers when a correct tag enters or exits a collider.
/// </summary>
[AddComponentMenu("Plantain/Trigger/Pressure Plate")]
[RequireComponent(typeof(Collider))]
public class TriggerPressurePlate : Trigger {
    /// <summary>
    /// Will this only trigger once?
    /// </summary>
    public bool isOneShot = false;

    /// <summary>
    /// A list of GameObject tags that will be checked in the collider.
    /// </summary>
    public List<string> acceptedTags;

    void Start() {
        if(acceptedTags == null || acceptedTags.Count == 0) {
            acceptedTags = new List<string>();
        }
    }

    void OnTriggerEnter(Collider other) {
        if(isActive) {
            if(acceptedTags.IndexOf(other.tag) >= 0) {
                Fire();
                if(isOneShot) {
                    isActive = false;
                }
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(isActive) {
            if(acceptedTags.IndexOf(other.tag) >= 0) {
                Fire();
            }
        }
    }

    override public void OnPerformerActivate(Performer p) { isActive = true; }
    override public void OnPerformerDeactivate(Performer p) { isActive = false; }

    void OnDrawGizmos() {
        TriggerGizmos("PressurePlate");
    }
}
