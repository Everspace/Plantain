using Plantain;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Plantain/Trigger/Propagator")]
public class TriggerPropagator : Trigger {
    /// <summary>
    /// A list of GameObjects that will have the current GameObject's triggers passed on to.
    /// </summary>
    public List<GameObject> targets;

    /// <summary>
    /// Used to prevent infinite propagation by allowing only 1 pass on.
    /// </summary>
    protected bool procced = false;
    
    override public void OnPerformerActivate(Performer p) { }
    override public void OnPerformerDeactivate(Performer p) { }

    public void PerformTrigger(TriggerOption tOption) {
        if(!procced && isActive) {
            procced = true;
            foreach(GameObject gameObj in targets) {
                gameObj.SendMessage("PerformTrigger", tOption, SendMessageOptions.DontRequireReceiver);
            }
            procced = false;
        }
    }

    void OnDrawGizmos() {
        TriggerGizmos("Propagator");

        Gizmos.color = Color.cyan;
        GizmoTargetedObjects(targets);
    }
}
