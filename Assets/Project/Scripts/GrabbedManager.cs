using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace AssemblySystem
{
    /// <summary>
    /// Manages the tracking of grabbed game objects by the left and right hands/controllers.
    /// Provides functionality to track which objects are currently held and handle events for grabbing and releasing objects.
    /// </summary>
    public class GrabbedManager : MonoBehaviour
    {
        private (GameObject left, GameObject right) grabbedGameObjects;
        private GameObject lastReleasedGameObject;

        /// <summary>
        /// Event triggered when a game object is grabbed.
        /// </summary>
        public event Action<(GameObject gameObject, bool left)> OnGrab;

        /// <summary>
        /// Event triggered when a game object is released.
        /// </summary>
        public event Action<(GameObject gameObject, bool left)> OnRelease;

        /// <summary>
        /// Gets the currently grabbed game objects for both left and right hands/controllers.
        /// </summary>
        public (GameObject left, GameObject right) GrabbedGameObjects => grabbedGameObjects;

        /// <summary>
        /// Unity's Start method. Registers listeners for grab and release events on all XR interactors.
        /// </summary>
        private void Start() {
            XRBaseInputInteractor[] interactors = FindObjectsOfType<XRBaseInputInteractor>(true);
            foreach (XRBaseInputInteractor interactor in interactors) {
                interactor.selectEntered.AddListener((args) => {
                    AddGrabbedGameObject(args.interactableObject.transform.gameObject, args.interactorObject.handedness == InteractorHandedness.Left);
                });

                interactor.selectExited.AddListener((args) => {
                    RemoveGrabbedGameObject(args.interactableObject.transform.gameObject, args.interactorObject.handedness == InteractorHandedness.Left);
                });
            }
        }

        /// <summary>
        /// Adds a game object to the list of grabbed objects, updating the appropriate slot.
        /// </summary>
        /// <param name="gameObject">The game object to add.</param>
        /// <param name="left">True if the game object is grabbed with the left hand/controller; false if with the right hand/controller.</param>
        public void AddGrabbedGameObject(GameObject gameObject, bool left) {
            if (left) {
                grabbedGameObjects.left = gameObject;
                if (grabbedGameObjects.right != gameObject)
                    OnGrab?.Invoke((gameObject, true));
            }
            else {
                grabbedGameObjects.right = gameObject;
                if (grabbedGameObjects.left != gameObject)
                    OnGrab?.Invoke((gameObject, false));
            }
        }

        /// <summary>
        /// Removes a game object from the list of grabbed objects, updating the appropriate slot.
        /// </summary>
        /// <param name="gameObject">The game object to remove.</param>
        /// <param name="left">True if the game object is removed from the left hand/controller; false if from the right hand/controller.</param>
        public void RemoveGrabbedGameObject(GameObject gameObject, bool left) {
            if (left && grabbedGameObjects.left == gameObject) {
                grabbedGameObjects.left = null;
                if (grabbedGameObjects.right == gameObject)
                    return;
                
                lastReleasedGameObject = gameObject;
                OnRelease?.Invoke((gameObject, false));
            }
            else if (grabbedGameObjects.right == gameObject) {
                grabbedGameObjects.right = null;
                if (grabbedGameObjects.left == gameObject)
                    return;
                    
                lastReleasedGameObject = gameObject;
                OnRelease?.Invoke((gameObject, true));
            }
        }
    }
}