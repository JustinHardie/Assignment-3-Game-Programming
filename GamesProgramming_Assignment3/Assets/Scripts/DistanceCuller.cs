using UnityEngine;

[DisallowMultipleComponent]
public class DistanceCuller2D : MonoBehaviour
{
    [Tooltip("How far outside the screen we still keep this rendered (in viewport units). 0.1 = 10% beyond edges")]
    public float viewportMargin = 0.1f;

    [Tooltip("Also pause Animator when culled")]
    public bool pauseAnimator = true;

    Camera cam;
    Renderer[] renderers;
    Animator[] animators;
    bool currentlyVisible = true;

    void Awake()
    {
        cam = Camera.main;
        renderers = GetComponentsInChildren<Renderer>(true);
        animators = GetComponentsInChildren<Animator>(true);
    }

    void LateUpdate()
    {
        if (!cam) { cam = Camera.main; if (!cam) return; }

        // Use bounds center; for very large objects you could use Renderer.bounds checks
        Vector3 vp = cam.WorldToViewportPoint(transform.position);

        bool inView =
            vp.z > 0f &&                                // in front of camera
            vp.x > -viewportMargin && vp.x < 1f + viewportMargin &&
            vp.y > -viewportMargin && vp.y < 1f + viewportMargin;

        if (inView == currentlyVisible) return;
        currentlyVisible = inView;

        // Toggle renderers
        if (renderers != null)
            foreach (var r in renderers) if (r) r.enabled = inView;

        // Optional: pause/resume animators
        if (pauseAnimator && animators != null)
            foreach (var a in animators) if (a) a.enabled = inView;
    }
}
