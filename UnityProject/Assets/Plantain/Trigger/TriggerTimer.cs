using Plantain;
using UnityEngine;

/// <summary>
/// A trigger that ticks down, and then triggers when finished.
/// </summary>
[AddComponentMenu("Plantain/Trigger/Timer")]
public class TriggerTimer : Trigger {

    /// <summary>
    /// Will this trigger start again onced finished?
    /// </summary>
    public bool autoReset = false;

    /// <summary>
    /// The timer's time in seconds
    /// </summary>
    public float duration = 10;

    /// <summary>
    /// Prevents not intiallyActive triggers from starting the timer immediately.
    /// </summary>
    private bool ignoringInitalActivate = true;

    /// <summary>
    /// Internal time tracker
    /// </summary>
    private float _time = 0;

    void Start() {
        if(initiallyActive) {
            ignoringInitalActivate = false;
        }
    }

    void Update() {
        if(isActive) {
            if(_time > 0) {
                _time -= Time.deltaTime;
            }

            if(_time <= 0) {
                Fire();

                if(autoReset) {
                    onPerformerActivate();
                } else {
                    isActive = false;
                }
            }
        }
    }

    override protected void Fire() {
        base.Fire();
        onPerformerActivate();
    }

    override public void OnPerformerActivate(Performer p) { onPerformerActivate(); }
    public void onPerformerActivate() {
        if(ignoringInitalActivate) {
            ignoringInitalActivate = false;
        } else {
            _time = duration;
            isActive = true;
        }
    }

    override public void OnPerformerDeactivate(Performer p) { onPerformerDeactivate(); }
    public void onPerformerDeactivate() {
        _time = duration;
        isActive = false;
    }

    void OnDrawGizmos() {
        TriggerGizmos("Timer");
        //Draw a little clock line.
        var progress = Mathf.Deg2Rad * _time / duration * 360;
        Gizmos.color = Color.white;
        Gizmos.DrawRay(_gizmoPosition, Camera.current.transform.TransformDirection(Mathf.Sin(progress), Mathf.Cos(progress), 0) * 1);
    }
}
