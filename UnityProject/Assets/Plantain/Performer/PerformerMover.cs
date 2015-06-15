using Plantain;
using UnityEngine;

[AddComponentMenu("Plantain/Performer/Mover")]
public class PerformerMover : Performer {
    public GameObject startPosition;
    public GameObject targetPosition;
    public float speed = 0.5f;

    private Transform currentDestination;

    void Awake() {
        if(startPosition == null) {
            startPosition = new GameObject();
            Transform t = startPosition.GetComponent<Transform>();
            t.position = gameObject.transform.position;
            t.rotation = gameObject.transform.rotation;
            startPosition.gameObject.name = "Start (" + name + ")";
        }

        if(targetPosition == null) {
            Debug.LogError("This mover has no target destination!", this);
        }

        SetDestination();
    }
    
    void Update() {
        if(isActive) {
            SetDestination();
            transform.position = Vector3.Lerp(transform.position, currentDestination.position, speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, currentDestination.rotation, speed);
        }
    }

    void SetDestination() {
        currentDestination = (state) ? targetPosition.transform : startPosition.transform;
    }

    void OnDrawGizmos() {
        PerformerGizmos("Mover");

        Gizmos.color = (state) ? Color.green : Color.white;

        if(targetPosition != null) {
            Vector3 pos = (startPosition == null) ? transform.position : startPosition.transform.position;
            Gizmos.DrawLine(pos, targetPosition.transform.position);
        }
    }
}
