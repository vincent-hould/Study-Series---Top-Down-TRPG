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
            SelectionEventChannelSO.OnCursorDisabled += StopFollowing;
            BattleEventChannelSO.OnUnitMoveStarted += SetFollowTarget;
            BattleEventChannelSO.OnUnitMoveEnded += StopFollowingUnit;
        }

        private void OnDestroy()
        {
            SelectionEventChannelSO.OnCursorEnabled -= SetFollowTarget;
            SelectionEventChannelSO.OnCursorDisabled -= StopFollowing;
            BattleEventChannelSO.OnUnitMoveStarted -= SetFollowTarget;
            BattleEventChannelSO.OnUnitMoveEnded -= StopFollowingUnit;
        }

        private void SetFollowTarget(Transform target)
        {
            Cam.Follow = target;
        }

        private void StopFollowingUnit(Unit unit) => Cam.Follow = null;

        private void StopFollowing() => Cam.Follow = null;
    }
}
