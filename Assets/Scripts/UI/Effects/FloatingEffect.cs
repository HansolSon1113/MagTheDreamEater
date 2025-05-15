using UnityEngine;

namespace UI.Effects
{
    public class FloatingEffect: MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private void Update()
        {
            var scale = 1f + Mathf.Sin(Time.time * 2f) * speed;
            transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}