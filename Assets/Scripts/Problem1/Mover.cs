namespace Gimica.ProblemOne
{
    using UnityEngine;

    // general notice, I rarely use comments in my code as I focus to write my code self explanatory,
    //you will find many comments in my code for the sake of this CaseStudy requirements.
    public class Mover : MonoBehaviour
    {
        // i usually add this kind of values to a scriptable object, with a range constraints like Range(0, 10)
        // so that the user can not assign unwanted values like less than zero.
        private const float MinMoveDistanceThreshold = 1;

        [SerializeField] private bool _lookAtTarget;
        [SerializeField, Range(0, 30)] private float _moveSpeed = 5f;
        [SerializeField] private Transform _target;

        private Vector3 _lastTransformPosition;
        private Vector3 _lastTargetPosition;
        
        private void Awake()
        {
            TryCheckForDeveloperErrors();
        }

        private void Update()
        {
            if (!CanMove())
            {
                ConstrainPositionToMinDistanceThreshold();
                return;
            }

            TryLookAtTarget();
            MoveToTarget();
        }
        
        private bool CanMove()
        {
            var transformPos = transform.position;
            var targetPos = _target.position;
            
            // avoiding distance check each frame
            var anyPositionsChanged = (transformPos != _lastTransformPosition) || (targetPos != _lastTargetPosition);
            if (!anyPositionsChanged)
            {
                return false;
            }
            
            _lastTransformPosition = transformPos;
            _lastTargetPosition = targetPos;
            
            // for a further optimization, .sqrMagnitude can be used but at this point I prefer readability
            return Vector3.Distance(transformPos, targetPos) > MinMoveDistanceThreshold;
        }

        // - normalizing the direction to keep the speed constant. If we only use the direction, the speed will change
        //depending on the distance.
        // - using Time.deltaTime to keep the movement framerate independent.
        // - if physics were required we could have also used rigidbody.MovePosition() with Time.fixedDeltaTime;
        private void MoveToTarget()
        {
            var normalizedDirectionToTarget = (_target.position - transform.position).normalized;
            transform.position += normalizedDirectionToTarget * (_moveSpeed * Time.deltaTime);
        }

        // - keeps the target away at the min distance threshold
        private void ConstrainPositionToMinDistanceThreshold()
        {
            var normalizedDirectionToTransform = (transform.position - _target.position).normalized;
            transform.position = _target.position + normalizedDirectionToTransform * MinMoveDistanceThreshold;
        }
        
        private void TryLookAtTarget()
        {
            if (!_lookAtTarget)
            {
                return;
            }
            
            transform.LookAt(_target);
        }
        
        private void TryCheckForDeveloperErrors()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            // i usually use editor scripts for such cases, to let the user know with the red exclamation mark and tooltips.
            if (_target == null)
            {
                Debug.LogError($"{gameObject.name} : Mover has no target.");
            }

            if (_moveSpeed < 0)
            {
                Debug.LogError($"{gameObject.name} : Mover _moveSpeed is a negative. Converting to positive. Please fix.");
                _moveSpeed = Mathf.Abs(_moveSpeed);
            }

            if (MinMoveDistanceThreshold <= 0)
            {
                Debug.LogError($"{gameObject.name} : Mover MinMoveDistanceThreshold is a negative value. Please fix.");
            }
#endif
        }
    }
}