using Plantain;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers when it collides with an accepted tag.
/// </summary>
[AddComponentMenu("Plantain/Trigger/On Hit")]
[RequireComponent(typeof(Collider))]
public class TriggerOnHit : Trigger {
    /// <summary>
    /// Will this only trigger once?
    /// </summary>
    public bool isOneShot = false;

    /// <summary>
    /// A list of GameObject tags that will be allowed to trigger.
    /// </summary>
    public List<string> acceptedTags;

    void OnCollisionEnter(Collision other) {
        if(isActive) {
            if(acceptedTags.IndexOf(other.collider.tag) >= 0) {
                trigger();
                if(isOneShot) {
                    isActive = false;
                }
            }
        }
    }

    override public void OnPerformerActivate(Performer p) { isActive = true; }
    override public void OnPerformerDeactivate(Performer p) { isActive = false; }

    void OnDrawGizmos() {
        TriggerGizmos("OnHit");
    }
}
