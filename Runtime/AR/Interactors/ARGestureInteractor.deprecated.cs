//-----------------------------------------------------------------------
// <copyright file="ARGestureInteractor.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

// Modifications copyright © 2020 Unity Technologies ApS

using System;

#if !AR_FOUNDATION_PRESENT && !PACKAGE_DOCS_GENERATION

// Stub class definition used to fool version defines that this MonoScript exists (fixed in 19.3)
namespace UnityEngine.XR.Interaction.Toolkit.AR
{
    /// <summary>
    /// The <see cref="ARGestureInteractor"/> allows the user to manipulate virtual objects (select, translate,
    /// rotate, scale, and elevate) through gestures (tap, drag, twist, and pinch).
    /// </summary>
    /// <remarks>
    /// To make use of this, add an <see cref="ARGestureInteractor"/> to your scene
    /// and an <see cref="ARBaseGestureInteractable"/> to any of your virtual objects.
    /// </remarks>
    [Obsolete("ARGestureInteractor has been replaced by the XRScreenspaceController and XRRayInteractor.")]
    public class ARGestureInteractor { }
}

#else

using System.Collections.Generic;
using Unity.XR.CoreUtils;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.EnhancedTouch;
#endif
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

namespace UnityEngine.XR.Interaction.Toolkit.AR
{
    /// <summary>
    /// The <see cref="ARGestureInteractor"/> allows the user to manipulate virtual objects (select, translate,
    /// rotate, scale, and elevate) through gestures (tap, drag, twist, and pinch).
    /// </summary>
    /// <remarks>
    /// To make use of this, add an <see cref="ARGestureInteractor"/> to your scene
    /// and an <see cref="ARBaseGestureInteractable"/> to any of your virtual objects.
    /// </remarks>
    [AddComponentMenu("XR/AR Gesture Interactor", 22)]
    [HelpURL(XRHelpURLConstants.k_ARGestureInteractor)]
    [Obsolete("ARGestureInteractor has been replaced by the XRScreenspaceController and XRRayInteractor.")]
    public class ARGestureInteractor : XRBaseInteractor
    {
#pragma warning disable CS0618 // ARSessionOrigin is deprecated in 5.0, but kept to support older AR Foundation versions
        [SerializeField]
        ARSessionOrigin m_ARSessionOrigin;
#pragma warning restore CS0618

        /// <summary>
        /// The <a href="https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.1/api/UnityEngine.XR.ARFoundation.ARSessionOrigin.html">ARSessionOrigin</a>
        /// that this Interactor will use (such as to get the <see cref="Camera"/>
        /// or to transform from Session space). Will find one if <see langword="null"/>.
        /// </summary>
        [Obsolete("arSessionOrigin is marked for deprecation and will be removed in a future version. Please use xrOrigin instead.")]
        public ARSessionOrigin arSessionOrigin
        {
            get => m_ARSessionOrigin;
            set
            {
                m_ARSessionOrigin = value;
                if (Application.isPlaying)
                    PushARSessionOrigin();
            }
        }

#pragma warning disable IDE1006 // Naming Styles
        static ARGestureInteractor s_Instance;
        /// <summary>
        /// (Read Only) The <see cref="ARGestureInteractor"/> instance.
        /// </summary>
        /// <remarks>
        /// <c>instance</c> has been deprecated. Use <see cref="ARBaseGestureInteractable.gestureInteractor"/> instead of singleton.
        /// </remarks>
        [Obsolete("instance has been deprecated. Use ARBaseGestureInteractable.gestureInteractor instead of singleton.")]
        public static ARGestureInteractor instance
        {
            get
            {
                if (s_Instance == null)
                {
#if UNITY_2023_1_OR_NEWER
                    s_Instance = FindAnyObjectByType<ARGestureInteractor>();
#else
                    s_Instance = FindObjectOfType<ARGestureInteractor>();
#endif
                    if (s_Instance == null)
                    {
                        Debug.LogError("No instance of ARGestureInteractor exists in the scene.");
                    }
                }

                return s_Instance;
            }
        }

        /// <inheritdoc cref="instance"/>
        /// <remarks>
        /// <c>Instance</c> has been deprecated. Use <see cref="instance"/> instead.
        /// </remarks>
        [Obsolete("Instance has been deprecated. Use instance instead. (UnityUpgradable) -> instance")]
        public static ARGestureInteractor Instance => instance;

        /// <inheritdoc cref="dragGestureRecognizer"/>
        /// <remarks><c>DragGestureRecognizer</c> has been deprecated. Use <see cref="dragGestureRecognizer"/> instead.</remarks>
        [Obsolete("DragGestureRecognizer has been deprecated. Use dragGestureRecognizer instead. (UnityUpgradable) -> dragGestureRecognizer")]
        public DragGestureRecognizer DragGestureRecognizer => dragGestureRecognizer;

        /// <inheritdoc cref="pinchGestureRecognizer"/>
        /// <remarks><c>PinchGestureRecognizer</c> has been deprecated. Use <see cref="pinchGestureRecognizer"/> instead.</remarks>
        [Obsolete("PinchGestureRecognizer has been deprecated. Use pinchGestureRecognizer instead. (UnityUpgradable) -> pinchGestureRecognizer")]
        public PinchGestureRecognizer PinchGestureRecognizer => pinchGestureRecognizer;

        /// <inheritdoc cref="twoFingerDragGestureRecognizer"/>
        /// <remarks><c>TwoFingerDragGestureRecognizer</c> has been deprecated. Use <see cref="twoFingerDragGestureRecognizer"/> instead.</remarks>
        [Obsolete("TwoFingerDragGestureRecognizer has been deprecated. Use twoFingerDragGestureRecognizer instead. (UnityUpgradable) -> twoFingerDragGestureRecognizer")]
        public TwoFingerDragGestureRecognizer TwoFingerDragGestureRecognizer => twoFingerDragGestureRecognizer;

        /// <inheritdoc cref="tapGestureRecognizer"/>
        /// <remarks><c>TapGestureRecognizer</c> has been deprecated. Use <see cref="tapGestureRecognizer"/> instead.</remarks>
        [Obsolete("TapGestureRecognizer has been deprecated. Use tapGestureRecognizer instead. (UnityUpgradable) -> tapGestureRecognizer")]
        public TapGestureRecognizer TapGestureRecognizer => tapGestureRecognizer;

        /// <inheritdoc cref="twistGestureRecognizer"/>
        /// <remarks><c>TwistGestureRecognizer</c> has been deprecated. Use <see cref="twistGestureRecognizer"/> instead.</remarks>
        [Obsolete("TwistGestureRecognizer has been deprecated. Use twistGestureRecognizer instead. (UnityUpgradable) -> twistGestureRecognizer")]
        public TwistGestureRecognizer TwistGestureRecognizer => twistGestureRecognizer;
#pragma warning restore IDE1006

        [SerializeField]
        XROrigin m_XROrigin;

        /// <summary>
        /// The <see cref="XROrigin"/> that this Interactor will use
        /// (such as to get the <see cref="Camera"/> or to transform from Session space).
        /// Will find one if <see langword="null"/>.
        /// </summary>
        public XROrigin xrOrigin
        {
            get => m_XROrigin;
            set
            {
                m_XROrigin = value;
                if (Application.isPlaying)
                    PushXROrigin();
            }
        }

        [SerializeField]
        LayerMask m_RaycastMask = -1;
        /// <summary>
        /// Gets or sets layer mask used for limiting ray cast targets.
        /// </summary>
        public LayerMask raycastMask
        {
            get => m_RaycastMask;
            set
            {
                m_RaycastMask = value;
                if (Application.isPlaying)
                    PushRaycastLayerMask();
            }
        }

        [SerializeField]
        QueryTriggerInteraction m_RaycastTriggerInteraction = QueryTriggerInteraction.Ignore;
        /// <summary>
        /// Gets or sets type of interaction with trigger colliders via ray cast.
        /// </summary>
        /// <remarks>
        /// When set to <see cref="QueryTriggerInteraction.UseGlobal"/>, the value of Queries Hit Triggers (<see cref="Physics.queriesHitTriggers"/>)
        /// in Edit &gt; Project Settings &gt; Physics will be used.
        /// </remarks>
        public QueryTriggerInteraction raycastTriggerInteraction
        {
            get => m_RaycastTriggerInteraction;
            set
            {
                m_RaycastTriggerInteraction = value;
                if (Application.isPlaying)
                    PushRaycastTriggerInteraction();
            }
        }

        /// <summary>
        /// (Read Only) The Drag gesture recognizer.
        /// </summary>
        public DragGestureRecognizer dragGestureRecognizer { get; private set; }

        /// <summary>
        /// (Read Only) The Pinch gesture recognizer.
        /// </summary>
        public PinchGestureRecognizer pinchGestureRecognizer { get; private set; }

        /// <summary>
        /// (Read Only) The two-finger Drag gesture recognizer.
        /// </summary>
        public TwoFingerDragGestureRecognizer twoFingerDragGestureRecognizer { get; private set; }

        /// <summary>
        /// (Read Only) The Tap gesture recognizer.
        /// </summary>
        public TapGestureRecognizer tapGestureRecognizer { get; private set; }

        /// <summary>
        /// (Read Only) The Twist gesture recognizer.
        /// </summary>
        public TwistGestureRecognizer twistGestureRecognizer { get; private set; }

        readonly List<IXRInteractable> m_ValidTargets = new List<IXRInteractable>();

        /// <summary>
        /// Temporary, reusable list of registered Interactables.
        /// </summary>
        static readonly List<IXRInteractable> s_Interactables = new List<IXRInteractable>();

        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();

            dragGestureRecognizer = new DragGestureRecognizer();
            pinchGestureRecognizer = new PinchGestureRecognizer();
            twoFingerDragGestureRecognizer = new TwoFingerDragGestureRecognizer();
            tapGestureRecognizer = new TapGestureRecognizer();
            twistGestureRecognizer = new TwistGestureRecognizer();

            FindXROrigin();
            PushXROrigin();
            PushRaycastLayerMask();
            PushRaycastTriggerInteraction();

#pragma warning disable 618 // Calling deprecated method to help with backwards compatibility.
            FindARSessionOrigin();
            PushARSessionOrigin();
#pragma warning restore 618

            if (m_XROrigin == null && m_ARSessionOrigin == null)
                Debug.LogWarning($"{nameof(ARGestureInteractor)} on {name} requires that a {nameof(XROrigin)} exists in the Scene, but none was found.", this);
        }

        /// <inheritdoc />
        protected override void OnEnable()
        {
            base.OnEnable();

#if ENABLE_INPUT_SYSTEM
            EnhancedTouchSupport.Enable();
#endif
            FindXROrigin();
            PushXROrigin();
            PushRaycastLayerMask();
            PushRaycastTriggerInteraction();

#pragma warning disable 618 // Calling deprecated method to help with backwards compatibility.
            FindARSessionOrigin();
            PushARSessionOrigin();
#pragma warning restore 618
        }

        /// <inheritdoc />
        protected override void OnDisable()
        {
            base.OnDisable();

#if AR_FOUNDATION_PRESENT && ENABLE_INPUT_SYSTEM
            EnhancedTouchSupport.Disable();
#endif
        }

        /// <inheritdoc />
        public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractor(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
                UpdateGestureRecognizers();
        }

        void FindXROrigin()
        {
            if (m_XROrigin == null)
                ComponentLocatorUtility<XROrigin>.TryFindComponent(out m_XROrigin);
        }

        void FindARSessionOrigin()
        {
#pragma warning disable CS0618 // ARSessionOrigin is deprecated in 5.0, but kept to support older AR Foundation versions
            if (m_ARSessionOrigin == null)
                ComponentLocatorUtility<ARSessionOrigin>.TryFindComponent(out m_ARSessionOrigin);
#pragma warning restore CS0618
        }

        /// <inheritdoc />
        public override void GetValidTargets(List<IXRInteractable> validTargets)
        {
            validTargets.Clear();
            validTargets.AddRange(m_ValidTargets);
        }

        /// <summary>
        /// Update all Gesture Recognizers.
        /// </summary>
        /// <seealso cref="GestureRecognizer{T}.Update"/>
        protected virtual void UpdateGestureRecognizers()
        {
            dragGestureRecognizer.Update();
            pinchGestureRecognizer.Update();
            twoFingerDragGestureRecognizer.Update();
            tapGestureRecognizer.Update();
            twistGestureRecognizer.Update();
        }

        /// <summary>
        /// Passes the <see cref="arSessionOrigin"/> to the Gesture Recognizers.
        /// </summary>
        /// <seealso cref="GestureRecognizer{T}.arSessionOrigin"/>
        [Obsolete("PushARSessionOrigin has been deprecated. Use PushXROrigin instead for similar functionality.")]
        protected virtual void PushARSessionOrigin()
        {
            dragGestureRecognizer.arSessionOrigin = m_ARSessionOrigin;
            pinchGestureRecognizer.arSessionOrigin = m_ARSessionOrigin;
            twoFingerDragGestureRecognizer.arSessionOrigin = m_ARSessionOrigin;
            tapGestureRecognizer.arSessionOrigin = m_ARSessionOrigin;
            twistGestureRecognizer.arSessionOrigin = m_ARSessionOrigin;
        }

        /// <summary>
        /// Passes the <see cref="xrOrigin"/> to the Gesture Recognizers.
        /// </summary>
        /// <seealso cref="GestureRecognizer{T}.xrOrigin"/>
        protected virtual void PushXROrigin()
        {
            dragGestureRecognizer.xrOrigin = m_XROrigin;
            pinchGestureRecognizer.xrOrigin = m_XROrigin;
            twoFingerDragGestureRecognizer.xrOrigin = m_XROrigin;
            tapGestureRecognizer.xrOrigin = m_XROrigin;
            twistGestureRecognizer.xrOrigin = m_XROrigin;
        }

        /// <summary>
        /// Passes raycast layer mask properties to the Gesture Recognizers.
        /// </summary>
        /// <seealso cref="GestureRecognizer{T}.raycastMask"/>
        protected virtual void PushRaycastLayerMask()
        {
            dragGestureRecognizer.raycastMask = m_RaycastMask;
            pinchGestureRecognizer.raycastMask = m_RaycastMask;
            twoFingerDragGestureRecognizer.raycastMask = m_RaycastMask;
            tapGestureRecognizer.raycastMask = m_RaycastMask;
            twistGestureRecognizer.raycastMask = m_RaycastMask;
        }

         /// <summary>
        /// Passes raycast trigger interaction properties to the Gesture Recognizers.
        /// </summary>
        /// <seealso cref="GestureRecognizer{T}.raycastTriggerInteraction"/>
        protected virtual void PushRaycastTriggerInteraction()
        {
            dragGestureRecognizer.raycastTriggerInteraction = m_RaycastTriggerInteraction;
            pinchGestureRecognizer.raycastTriggerInteraction = m_RaycastTriggerInteraction;
            twoFingerDragGestureRecognizer.raycastTriggerInteraction = m_RaycastTriggerInteraction;
            tapGestureRecognizer.raycastTriggerInteraction = m_RaycastTriggerInteraction;
            twistGestureRecognizer.raycastTriggerInteraction = m_RaycastTriggerInteraction;
        }

        /// <inheritdoc />
        protected override void OnRegistered(InteractorRegisteredEventArgs args)
        {
            base.OnRegistered(args);
            args.manager.interactableRegistered += OnInteractableRegistered;
            args.manager.interactableUnregistered += OnInteractableUnregistered;

            // Get all of the registered gesture interactables to use as the valid targets
            m_ValidTargets.Clear();
            interactionManager.GetRegisteredInteractables(s_Interactables);
            foreach (var interactable in s_Interactables)
            {
                if (interactable is ARBaseGestureInteractable)
                    m_ValidTargets.Add(interactable);
            }

            s_Interactables.Clear();
        }

        /// <inheritdoc />
        protected override void OnUnregistered(InteractorUnregisteredEventArgs args)
        {
            base.OnUnregistered(args);
            args.manager.interactableRegistered -= OnInteractableRegistered;
            args.manager.interactableUnregistered -= OnInteractableUnregistered;
        }

        void OnInteractableRegistered(InteractableRegisteredEventArgs args)
        {
            if (args.interactableObject is ARBaseGestureInteractable)
                m_ValidTargets.Add(args.interactableObject);
        }

        void OnInteractableUnregistered(InteractableUnregisteredEventArgs args)
        {
            if (args.interactableObject is ARBaseGestureInteractable)
                m_ValidTargets.Remove(args.interactableObject);
        }
    }
}
#endif
