using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class Grenade : MonoBehaviour
{
    [SerializeField] float explodeTime = 2f;
    [SerializeField] float radius = 2f;
    private void Awake() {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener((args) => GetComponent<Rigidbody>().useGravity = true);
        interactable.selectExited.AddListener((args) => Destroy(this.gameObject, explodeTime));  
    }

    private void OnDestroy() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders) {
            Target target = collider.GetComponent<Target>();
            if (target != null)
                target.HitTarget();
        }
    }
}
