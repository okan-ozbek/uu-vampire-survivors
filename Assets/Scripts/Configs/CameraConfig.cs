using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "camera_config", menuName = "Configs/Camera Config")]
    public class CameraConfig : ScriptableObject
    {
        public static Action<float, float> OnZoomOut;
        public static Action<float, float> OnZoomIn;
        public static Action<float> OnZoomReset;
    }
}