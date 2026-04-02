using UnityEngine;

namespace Templates
{
    public class GizmoPoint : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.green;
        [SerializeField] private float _size = 0.5f;

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _size);
        }
    }
}