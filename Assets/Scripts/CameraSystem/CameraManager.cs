using UnityEngine;

namespace TopDownTRPG
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float PanSpeed = 3f;
        [SerializeField] private float HorizontalPanningBuffer = 5f;
        [SerializeField] private float VerticalPanningBuffer = 2f;

        private Transform _target;

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

        private void Update()
        {
            if (_target != null)
            {
                var cam = Helpers.Camera;
                if (
                    Mathf.Abs(cam.transform.position.x - _target.position.x) > HorizontalPanningBuffer ||
                    Mathf.Abs(cam.transform.position.y - _target.position.y) > VerticalPanningBuffer
                ) {
                    cam.transform.position = Vector3.MoveTowards(
                        cam.transform.position,
                        _target.position,
                        Time.deltaTime * PanSpeed
                    );
                }
            }
        }

        private void SetFollowTarget(Transform target)
        {
            Helpers.Camera.transform.position = target.position;
            _target = target;
        }

        private void StopFollowingUnit(Unit unit) => _target = null;

        private void StopFollowing() => _target = null;
    }
}
