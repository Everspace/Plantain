using System.Collections.Generic;
using UnityEngine;

namespace Plantain {

    /// <summary>
    /// The base Performer type. All Performers should inherit from this class.
    /// </summary>
    [HideInInspector]
    public class Performer : MonoBehaviour {

        /// <summary>
        /// The current state of the Performer, either true or false, off or on.
        /// </summary>
        public bool state = true;
        /// <summary>
        /// Will this performer recieve triggers?
        /// </summary>
        public bool isActive = true;

        /// <summary>
        /// The location of the performer's gizmo.
        /// </summary>
        [HideInInspector]
        public Vector3 gizmoPosition {
            get { return _gizmoPosition; }
        }
        protected Vector3 _gizmoPosition;

        /// <summary>
        /// The location of the gizmos
        /// </summary>
        protected string gizmoPath = "Plantain\\Performer\\";

        /*
        *	Unity Functions
        */
        
        protected virtual void Start() {
            if(isActive) {
                Activate();
            }
        }

        protected virtual void OnEnable() { Activate(); }
        protected virtual void OnDisable() { Deactivate(); }
        protected virtual void OnDestroy() { Deactivate(); }

        /*
        *	Plantain Functions
        */

        /// <summary>
        /// Lets triggers know that this Performer is now availible
        /// </summary>
        public virtual void Activate() {
            isActive = true;
            BroadcastMessage("OnPerformerActivate", this, SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// Lets triggers know that this Performer is <b><u>not</u></b> availible
        /// </summary>
        public virtual void Deactivate() {
            isActive = false;
            BroadcastMessage("OnPerformerDeactivate", this, SendMessageOptions.DontRequireReceiver);
        }
        
        /// <summary>
        /// This is called when a trigger meets the conditions to change state.
        /// </summary>
        /// <param name="tOption"></param>
        public virtual void PerformTrigger(TriggerOption tOption) {
            if(isActive) {
                ApplyTriggerOption(tOption);
            }
        }

        //Switches states
        /// <summary>
        /// Switches the states of the performer based on the <see cref="TriggerOption"/>.
        /// </summary>
        /// <param name="option"></param>
        public void ApplyTriggerOption(TriggerOption option) {
            switch(option) {
                case TriggerOption.turnOff:
                    state = false;
                    break;
                case TriggerOption.turnOn:
                    state = true;
                    break;
                case TriggerOption.toggle:
                    state = !state;
                    break;
                default:
                    Debug.LogError("Passed bad TriggerOption during PerformTrigger", gameObject);
                    break;
            }
        }

        /*
        *	Gizmo Drawing Functions
        */

        /// <summary>
        /// Draw ALL the icons
        /// </summary>
        /// <param name="gizmoName">The icon name</param>
        protected void PerformerGizmos(string gizmoName) {
            Performer[] performerArray = GetComponents<Performer>();
            var i = 0;
            foreach(Performer p in performerArray) {
                if(p.Equals(this)) {
                    break;
                }
                i++;
            }
            //Grow our line of Performer Gizmos above the object to the right.
            Vector3 cameraRight = Camera.current.transform.TransformDirection(Vector3.right);
            _gizmoPosition = gameObject.transform.position + Vector3.up + cameraRight * i;

            var gizmoState = (isActive) ? (state) ? "Performer_On" : "Performer_NotOn" : "Performer_NotActive";
            Gizmos.DrawIcon(_gizmoPosition, gizmoPath + gizmoState);
            Gizmos.DrawIcon(_gizmoPosition, gizmoPath + gizmoName);
        }

        /// <summary>
        /// Helper function that draws a line from the <see cref="Performer._gizmoPosition"/> to each of the GameObjects
        /// </summary>
        /// <param name="gameObjList"></param>
        protected void GizmoTargetedObjects(List<GameObject> gameObjList) {
            foreach(GameObject gameObj in gameObjList) {
                if(gameObj != null && !gameObj.Equals(this.gameObject)) {
                    Gizmos.DrawLine(_gizmoPosition, gameObj.transform.position);
                }
            }
        }
    }
}
