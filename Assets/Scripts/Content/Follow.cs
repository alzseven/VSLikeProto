using System;
using UnityEngine;

namespace Content
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        
        private void Update()
        {
            transform.position += Vector3.Normalize((Vector2)(target.position - transform.position)) * (speed * Time.deltaTime);
        }
    }
}