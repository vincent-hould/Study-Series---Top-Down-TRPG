using UnityEngine;
using Cinemachine;

namespace TopDownTRPG
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCameraBase Cam;

        private void Awake()
        {
            SelectionEventChannelSO.OnCursorEnabled += SetFollowTarget;
            BattleEventChannelSO.OnUnitMoveStarted += SetFollowTarget;
        }

        private void OnDestroy()
        {
            SelectionEventChannelSO.OnCursorEnabled -= SetFollowTarget;
            BattleEventChannelSO.OnUnitMoveStarted -= SetFollowTarget;
        }

        private void SetFollowTarget(Transform target)
        {
            Cam.Follow = target;
        }
    }
}
