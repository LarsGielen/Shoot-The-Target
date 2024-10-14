using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class Grenade : MonoBehaviour
{
    [SerializeField] float explodeTime = 2f;
    [SerializeField] float explosionRadius = 2f;

    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] GameObject visual;

    private void Awake() {
        explosionVFX.gameObject.SetActive(false);
        
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener((args) => GetComponent<Rigidbody>().useGravity = true);
        interactable.selectExited.AddListener((args) => StartCoroutine(ExplosionDelay()));  
    }

    private IEnumerator ExplosionDelay() {
        yield return new WaitForSeconds(explodeTime);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders) {
            Target target = collider.GetComponent<Target>();
            if (target != null)
                target.HitTarget();
        }


        visual.SetActive(false);
        explosionVFX.gameObject.SetActive(true);

        Destroy(this.gameObject, explosionVFX.main.duration);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
