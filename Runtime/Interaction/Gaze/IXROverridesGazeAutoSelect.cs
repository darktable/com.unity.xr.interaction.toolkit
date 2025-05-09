using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace UnityEngine.XR.Interaction.Toolkit.Gaze
{
    /// <summary>
    /// An interface that represents an interactable that provides
    /// overrides of the default values for hover to select and auto deselect.
    /// </summary>
    /// <seealso cref="XRBaseInteractable"/>
    /// <seealso cref="XRGazeInteractor.GetHoverTimeToSelect"/>
    /// <seealso cref="XRGazeInteractor.GetTimeToAutoDeselect"/>
    [MovedFrom("UnityEngine.XR.Interaction.Toolkit")]
    public interface IXROverridesGazeAutoSelect
    {
        /// <summary>
        /// Enables this interactable to override the <see cref="XRRayInteractor.hoverTimeToSelect"/> on an <see cref="XRGazeInteractor"/>.
        /// </summary>
        /// <seealso cref="gazeTimeToSelect"/>
        /// <seealso cref="XRRayInteractor.hoverToSelect"/>
        bool overrideGazeTimeToSelect { get; }

        /// <summary>
        /// Number of seconds for which an <see cref="XRGazeInteractor"/> must hover over this interactable to select it if <see cref="XRRayInteractor.hoverToSelect"/> is enabled.
        /// </summary>
        /// <seealso cref="overrideGazeTimeToSelect"/>
        /// <seealso cref="XRRayInteractor.hoverTimeToSelect"/>
        float gazeTimeToSelect { get; }

        /// <summary>
        /// Enables this interactable to override the <see cref="XRRayInteractor.timeToAutoDeselect"/> on an <see cref="XRGazeInteractor"/>.
        /// </summary>
        /// <seealso cref="timeToAutoDeselectGaze"/>
        /// <seealso cref="XRRayInteractor.autoDeselect"/>
        bool overrideTimeToAutoDeselectGaze { get; }

        /// <summary>
        /// Number of seconds that the interactable will remain selected by an <see cref="XRGazeInteractor"/> before being
        /// automatically deselected if <see cref="overrideTimeToAutoDeselectGaze"/> is true.
        /// </summary>
        /// <seealso cref="overrideTimeToAutoDeselectGaze"/>
        float timeToAutoDeselectGaze { get; }
    }
}
