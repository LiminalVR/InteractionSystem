﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Liminal.SDK.InteractableSystem
{
    public class Grabber : MonoBehaviour
    {
        public GameObject[] Models;

        public List<GrabbableBase> Interactables;
        public List<GrabbableBase> GrabbedInteractables;
        public List<GrabPointProvider> GrabPointProviders;

        public Trigger GrabTrigger;
        public VelocityEstimator VelocityEstimator;

        public LayerMask Ignore;

        public Dictionary<GrabPointMarker, GrabPointProvider> GrabPointProvidersTable { get; private set; } = new Dictionary<GrabPointMarker, GrabPointProvider>();

        public HashSet<GrabbableBase> GrabbedInteractablesTable = new HashSet<GrabbableBase>();

        public bool Grabbing => GrabbedInteractables.Count != 0;

        private void Awake()
        {
            GrabTrigger.OnEnter.AddListener(OnGrabEnter);
            GrabTrigger.OnExit.AddListener(OnGrabExit);

            foreach (var grabPointProvider in GrabPointProviders)
                GrabPointProvidersTable.Add(grabPointProvider.Marker, grabPointProvider);
        }

        private void OnDestroy()
        {
            GrabTrigger.OnEnter.RemoveListener(OnGrabEnter);
            GrabTrigger.OnExit.RemoveListener(OnGrabExit);
        }

        private void Update()
        {
            foreach (var grabbedInteractable in GrabbedInteractables)
            {
                if(grabbedInteractable.UpdateMode == EUpdateMode.Update)
                    grabbedInteractable.Grabbing(this);
            }
        }

        private void FixedUpdate()
        {
            foreach (var grabbedInteractable in GrabbedInteractables)
            {
                if (grabbedInteractable.UpdateMode == EUpdateMode.FixedUpdate)
                    grabbedInteractable.Grabbing(this);
            }
        }

        /// <summary>
        /// Attempt to grab any grabbables within GrabTrigger.
        /// </summary>
        public void Grab()
        {
            foreach (var interactable in Interactables)
                Grabbed(interactable);
        }

        /// <summary>
        /// Attempt to ungrab all holding grabbables.
        /// </summary>
        public void UnGrab(bool ignorePolicy = false)
        {
            for (var i = GrabbedInteractables.Count - 1; i >= 0; i--)
            {
                var interactable = GrabbedInteractables[i];

                if(!interactable.GrabFlags.HasFlag(EGrabFlags.StayAttachedToControllerWhenUnGrabbed) || ignorePolicy)
                    UnGrabbed(interactable);
            }
        }

        public void Grabbed(GrabbableBase interactable)
        {
            if (GrabbedInteractablesTable.Contains(interactable))
                return;

            if (interactable.GrabFlags.HasFlag(EGrabFlags.UnGrabOthers))
            {
                for (var i = GrabbedInteractables.Count - 1; i >= 0; i--)
                {
                    var grabbed = GrabbedInteractables[i];
                    if (ReferenceEquals(grabbed, interactable))
                        continue;

                    UnGrabbed(grabbed);
                }
            }

            interactable.Grabber = this;
            interactable.Grabbed(this);

            GrabbedInteractables.Add(interactable);
            GrabbedInteractablesTable.Add(interactable);

            if (interactable.GrabFlags.HasFlag(EGrabFlags.HideControllers))
            {
                foreach (var model in Models)
                    model.gameObject.SetActive(false);
            }
        }

        public void UnGrabbed(GrabbableBase interactable)
        {
            interactable.UnGrabbed(this);
            interactable.Grabber = null;

            GrabbedInteractables.Remove(interactable);
            GrabbedInteractablesTable.Remove(interactable);

            if (interactable.GrabFlags.HasFlag(EGrabFlags.HideControllers))
            {
                foreach (var model in Models)
                    model.gameObject.SetActive(true);
            }
        }

        public void OnGrabEnter(Collider col)
        {
            if (ShouldIgnore(col))
                return;

            var interactable = col.GetComponentInParent<GrabbableBase>();
            if (Interactables.Contains(interactable))
                return;

            if(interactable != null)
                Interactables.Add(interactable);
        }

        public void OnGrabExit(Collider col)
        {
            if (ShouldIgnore(col))
                return;

            var interactable = col.GetComponentInParent<GrabbableBase>();
            if (interactable != null)
                Interactables.Remove(interactable);
        }

        public bool ShouldIgnore(Collider col)
        {
            return LayerUtils.LayerExists(col.gameObject.layer, Ignore);
        }
    }

    [Serializable]
    public class GrabPointProvider
    {
        public GrabPointMarker Marker;
        public Transform Point;
    }
}