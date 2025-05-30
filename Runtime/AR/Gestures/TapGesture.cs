//-----------------------------------------------------------------------
// <copyright file="TapGesture.cs" company="Google">
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

#if AR_FOUNDATION_PRESENT || PACKAGE_DOCS_GENERATION

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace UnityEngine.XR.Interaction.Toolkit.AR
{
    /// <summary>
    /// Gesture for when the user performs a tap on the touch screen.
    /// </summary>
    public class TapGesture : Gesture<TapGesture>
    {
        /// <summary>
        /// Initializes and returns an instance of <see cref="TapGesture"/>.
        /// </summary>
        /// <param name="recognizer">The gesture recognizer.</param>
        /// <param name="touch">The touch that started this gesture.</param>
        public TapGesture(TapGestureRecognizer recognizer, Touch touch)
            : this(recognizer, new CommonTouch(touch))
        {
        }

        /// <summary>
        /// Initializes and returns an instance of <see cref="TapGesture"/>.
        /// </summary>
        /// <param name="recognizer">The gesture recognizer.</param>
        /// <param name="touch">The touch that started this gesture.</param>
        public TapGesture(TapGestureRecognizer recognizer, InputSystem.EnhancedTouch.Touch touch)
            : this(recognizer, new CommonTouch(touch))
        {
        }

        TapGesture(TapGestureRecognizer recognizer, CommonTouch touch) : base(recognizer)
        {
            Reinitialize(touch);
        }

        internal void Reinitialize(Touch touch) => Reinitialize(new CommonTouch(touch));
        internal void Reinitialize(InputSystem.EnhancedTouch.Touch touch) => Reinitialize(new CommonTouch(touch));

        void Reinitialize(CommonTouch touch)
        {
            Reinitialize();
            fingerId = touch.fingerId;
            startPosition = touch.position;
            m_ElapsedTime = 0f;
        }

        /// <summary>
        /// (Read Only) The id of the finger used in this gesture.
        /// </summary>
        public int fingerId { get; private set; }

        /// <summary>
        /// (Read Only) The screen position where the gesture started.
        /// </summary>
        public Vector2 startPosition { get; private set; }

        /// <summary>
        /// (Read Only) The gesture recognizer.
        /// </summary>
        protected TapGestureRecognizer tapRecognizer => (TapGestureRecognizer)recognizer;

        float m_ElapsedTime;

        /// <inheritdoc />
        protected internal override bool CanStart()
        {
            if (GestureTouchesUtility.IsFingerIdRetained(fingerId))
            {
                Cancel();
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        protected internal override void OnStart()
        {
#pragma warning disable 618 // Using deprecated property to help with backwards compatibility.
            if (GestureTouchesUtility.RaycastFromCamera(startPosition, recognizer.xrOrigin, recognizer.arSessionOrigin, out var hit, recognizer.raycastMask, recognizer.raycastTriggerInteraction))
#pragma warning restore 618
            {
                var gameObject = hit.transform.gameObject;
                if (gameObject != null)
                {
                    var grabInteractable = gameObject.GetComponentInParent<XRGrabInteractable>();
                    if (grabInteractable != null)
                    {
                        var interactableObject = grabInteractable.gameObject;
                        if (interactableObject != null)
                            targetObject = interactableObject;
                    }
                    else
                    {
#pragma warning disable 618
                        var baseGestureInteractable = gameObject.GetComponentInParent<ARBaseGestureInteractable>();
                        if (baseGestureInteractable != null)
                        {
                            var interactableObject = baseGestureInteractable.gameObject;
                            if (interactableObject != null)
                                targetObject = interactableObject;
                        }
#pragma warning restore 618
                    }
                }
            }
        }

        /// <inheritdoc />
        protected internal override bool UpdateGesture()
        {
            if (GestureTouchesUtility.TryFindTouch(fingerId, out var touch))
            {
                m_ElapsedTime += touch.deltaTime;
                if (m_ElapsedTime > tapRecognizer.durationSeconds)
                {
                    Cancel();
                }
                else if (touch.isPhaseMoved)
                {
                    var diff = (touch.position - startPosition).magnitude;
                    var diffInches = GestureTouchesUtility.PixelsToInches(diff);
                    if (diffInches > tapRecognizer.slopInches)
                    {
                        Cancel();
                    }
                }
                else if (touch.isPhaseEnded)
                {
                    Complete();
                    return true;
                }
            }
            else
            {
                Cancel();
            }

            return false;
        }

        /// <inheritdoc />
        protected internal override void OnCancel()
        {
        }

        /// <inheritdoc />
        protected internal override void OnFinish()
        {
        }
    }
}

#endif
