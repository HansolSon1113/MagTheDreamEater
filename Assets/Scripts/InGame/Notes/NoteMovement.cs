using DG.Tweening;
using InGame.Managers;
using UnityEngine;

namespace InGame.Notes
{
    public class NoteMovement : MonoBehaviour
    {
        [HideInInspector] public float duration;
        [HideInInspector] public Transform endPoint;
        public bool lastNote;
        private ScoreManager scoreManager = ScoreManager.Instance;

        private void Start()
        {
            transform.DOMove(endPoint.position, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                scoreManager.score--;
                scoreManager.health -= 4;
                Destroy(gameObject);
            });
        }

        private void OnDestroy()
        {
            if (lastNote || scoreManager.health <= 0)
            {
                ScoreManager.Instance.Stop();
            }
        }
    }
}