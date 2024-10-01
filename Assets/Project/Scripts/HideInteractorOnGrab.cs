using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HideInteractorOnGrab : MonoBehaviour
{
    private XRBaseInteractor interactor;

    private void OnEnable() {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable() {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args) {
        var interactor = args.interactorObject as XRBaseInputInteractor;
        foreach (Renderer renderer in interactor.transform.parent.GetComponentsInChildren<Renderer>()) 
            renderer.enabled = false;
    }

    private void OnRelease(SelectExitEventArgs args) {        
        var interactor = args.interactorObject as XRBaseInputInteractor;
        foreach (Renderer renderer in interactor.transform.parent.GetComponentsInChildren<Renderer>()) 
            renderer.enabled = true;
    }
}
