using Plantain;
using UnityEngine;

/// <summary>
/// Changes the renderers material based on the performer's state.
/// </summary>
[AddComponentMenu("Plantain/Performer/Material Switcher")]
[RequireComponent(typeof(Renderer))]
public class PerformerMaterialSwitcher : Performer {
    /// <summary>
    /// Material when the performer's <code>state = true</code> (on)
    /// </summary>
    public Material onMaterial;
    /// <summary>
    /// Material when the performer's <code>state = false</code> (off)
    /// </summary>
    public Material offMaterial;
    /// <summary>
    /// Material when the performer is <code>!isActive</code>
    /// </summary>
    public Material inactiveMaterial;
    /// <summary>
    /// The renderer that this performer is changing. Will default to the current object's renderer.
    /// </summary>
    public Renderer render;

    void Awake() {
        if(render == null) {
            render = gameObject.GetComponent<Renderer>();
            if(render == null) {
                Debug.LogError("A renderer is missing from this game object", gameObject);
            }
        }
        ToggleMaterial();
    }

    override public void PerformTrigger(TriggerOption tOption) {
        base.PerformTrigger(tOption);
        ToggleMaterial();
    }

    void ToggleMaterial() {
        render.material = (isActive) ? (state) ? onMaterial : offMaterial : inactiveMaterial;
    }

    void OnDrawGizmos() {
        PerformerGizmos("MaterialSwitcher");
        Gizmos.color = (isActive) ? (state) ? Color.green : Color.red : Color.gray;
        if(render != null && render.gameObject != null) {
            Gizmos.DrawLine(_gizmoPosition, render.gameObject.transform.position);
        }
    }
}
