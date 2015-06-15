using System.Collections.Generic;
using UnityEngine;

namespace Plantain {

    /// <summary>
    /// The base Trigger type. All Triggers should inherit from this class.
    /// </summary>
    [HideInInspector]
    public abstract class Trigger : MonoBehaviour {

        /// <summary>
        /// Does this start on?
        /// </summary>
        public bool initiallyActive = false;

        /// <summary>
        /// Can this produce triggers?
        /// </summary>
        public bool isActive = false;

        /// <summary>
        /// What will a performer do when this trigger triggers?
        /// </summary>
        public TriggerOption triggerOption = TriggerOption.toggle;

        /// <summary>
        /// The location of the triggers' gizmo.
        /// </summary>
        [HideInInspector]
        public Vector3 gizmoPosition {
            get { return _gizmoPosition; }
        }
        protected Vector3 _gizmoPosition;

        /// <summary>
        /// Director for Trigger Gizmos
        /// </summary>
        protected string gizmoPath = "Plantain\\Trigger\\";

        /*
        *	Plantain Functions
        */

        /// <summary>
        /// Alert performers of what state to change to based on the current <see cref="TriggerOption"/>.
        /// </summary>
        protected virtual void Fire() {
            SendMessage("PerformTrigger", triggerOption, SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// Called when a Performer or the game object it is on turns on.
        /// </summary>
        /// <param name="p"></param>
        public abstract void OnPerformerActivate(Performer p);

        /// <summary>
        /// Called when a Performer or the game object it is on turns off
        /// </summary>
        /// <param name="p"></param>
        public abstract void OnPerformerDeactivate(Performer p);

        /*
        *	Gizmo Drawing Functions
        */

        /// <summary>
        /// Draw ALL the icons
        /// </summary>
        /// <param name="gizmoName">The icon name</param>
        protected void TriggerGizmos(string gizmoName) {
            Trigger[] ms = GetComponents<Trigger>();
            var i = 0;
            foreach(Trigger m in ms) {
                if(m.Equals(this)) {
                    break;
                }
                if(m != null) {
                    i++;
                }
            }

            _gizmoPosition = transform.position + Vector3.down + Camera.current.transform.TransformDirection(Vector3.right) * i;

            string state = (isActive) ? "Trigger_On" : "Trigger_NotOn";
            Gizmos.DrawIcon(_gizmoPosition, gizmoPath + state);
            state = (isActive) ? "Trigger_Active" : "Trigger_NotActive";
            Gizmos.DrawIcon(_gizmoPosition, gizmoPath + state);

            if(gizmoName != null) {
                Gizmos.DrawIcon(_gizmoPosition, gizmoPath + gizmoName);
            }
        }

        /// <summary>
        /// Helper function to shoot a debug line at any targets of this list.
        /// </summary>
        /// <param name="gameObjList"></param>
        protected void GizmoTargetedObjects(List<GameObject> gameObjList) {
            if(gameObjList != null && gameObjList.Count > 0) {
                foreach(GameObject gameObj in gameObjList) {
                    if(!gameObject.Equals(gameObj) && gameObj != null) {
                        Gizmos.DrawLine(_gizmoPosition, gameObj.transform.position);
                    }
                }
            }
        }
    }

}
