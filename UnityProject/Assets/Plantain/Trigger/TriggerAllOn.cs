using Plantain;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks to see if all the Performers on a list of <see cref="TriggerAllOn.targets"/> are currently on.
/// </summary>
[AddComponentMenu("Plantain/Trigger/All On")]
public class TriggerAllOn : Trigger {
    /// <summary>
    /// List of things that need to all be ON to work.
    /// </summary>
    public List<Performer> requiredPerformers;

    /// <summary>
    /// Trigger only once per set of changes.
    /// </summary>
    private bool procced = false;

    void Awake() {
        if(requiredPerformers == null) {
            requiredPerformers = new List<Performer>();
        }
        requiredPerformers.RemoveAll(p => p == null);
    }
    
    override public void OnPerformerActivate(Performer p) { }
    override public void OnPerformerDeactivate(Performer p) { }

    void FixedUpdate() {
        if(isActive) {
            requiredPerformers.RemoveAll(p => p == null);
            if(requiredPerformers.Count > 0) {
                bool allOn = true;
                foreach(Performer p in requiredPerformers) {
                    if(!(allOn & p.state)) {
                        allOn = false;
                        break;
                    }
                }

                if(allOn && !procced) {
                    trigger();
                    procced = true;
                } else if (!allOn) {
                    procced = false;
                }
            }
        }
    }

    void OnDrawGizmos() {
        TriggerGizmos("AllOn");
        if(requiredPerformers != null) {
            foreach(Performer p in requiredPerformers) {
                if(p != null) {
                    Gizmos.color = (p.state) ? Color.green : Color.red;
                    Gizmos.DrawLine(_gizmoPosition, p.gizmoPosition);
                }
            }
        }
    }
}
