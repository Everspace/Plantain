using Plantain;
using UnityEngine;

/// <summary>
/// Triggers when it collides with an accepted tag.
/// </summary>
[AddComponentMenu("Plantain/Trigger/On Click")]
[RequireComponent(typeof(Collider))]
public class TriggerOnClick : Trigger {
    /// <summary>
    /// Will this only trigger once?
    /// </summary>
    public bool isOneShot = false;

    void OnMouseDown() {
        if(isActive) {
            Fire();
            if(isOneShot) {
                isActive = false;
            }
        }
    }

    void OnDrawGizmos() {
        TriggerGizmos("OnClick");
    }
}
