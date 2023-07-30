using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Cameras
{
    [AddComponentMenu("Game Creator/Cameras/Main Camera")]
    [Icon(RuntimePaths.GIZMOS + "GizmoMainCamera.png")]
    public class MainCamera : TCamera
    {
        protected override void Awake()
        {
            base.Awake();
            ShortcutMainCamera.Change(this);
        }
        public void OpenAvoidClip()
        {
            this.AvoidClip.Enabled = true;
        }
        public void CloseAvoidClip()
        {
            this.AvoidClip.Enabled = false;
        }
    }
}
