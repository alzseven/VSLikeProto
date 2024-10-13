using UnityEngine;

namespace Content
{
    public class LateSlerpFollower : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] Vector3 _offSet;
        
        //TODO:
        private void LateUpdate()
        {
            if (_target) transform.position = Vector3.Slerp(transform.position, _target.position + _offSet, 0.1f);
        }
    }
}