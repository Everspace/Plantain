using Plantain;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Plantain/Trigger/Propagator")]
public class TriggerPropagator : Trigger {
    /// <summary>
    /// Prevent a trigger from hopping any further than the targeted objects.
    /// </summary>
    public bool preventPropigation = false;

    /// <summary>
    /// A list of GameObjects that will have the current GameObject's triggers passed on to.
    /// </summary>
    public List<GameObject> targets;

    /// <summary>
    /// Used to prevent propigation for infinite loops, among other senarios.
    /// </summary>
    protected bool procced = false;

    public void PerformTrigger(TriggerOption tOption) {
        if(!procced && isActive) {
            procced = true;
            foreach(GameObject gameObj in targets) {
                if(preventPropigation) {
                    TriggerPropagator[] propagators = gameObj.GetComponents<TriggerPropagator>();
                    foreach(TriggerPropagator propigator in propagators) {
                        propigator.procced = true;
                    }

                    gameObj.SendMessage("PerformTrigger", tOption, SendMessageOptions.DontRequireReceiver);

                    foreach(TriggerPropagator propigator in propagators) {
                        propigator.procced = false;
                    }

                } else {
                    gameObj.SendMessage("PerformTrigger", tOption, SendMessageOptions.DontRequireReceiver);
                }
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
