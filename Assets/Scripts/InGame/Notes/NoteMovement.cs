using DG.Tweening;
using InGame.Managers;
using UnityEngine;

namespace InGame.Notes
{
    public class NoteMovement : MonoBehaviour
    {
        [HideInInspector] public float endTime;
        [HideInInspector] public Transform spawnPoint, destination, endPoint;
        public bool lastNote;
        private ScoreManager scoreManager = ScoreManager.Instance;

        private void Start()
        {
            var duration = (endTime - scoreManager.stageTime) * (1 + (destination.position.x - endPoint.position.x) / (spawnPoint.position.x - destination.position.x));

            transform.DOMove(endPoint.position, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
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