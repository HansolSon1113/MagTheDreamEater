using System.Collections;
using InGame.Notes;
using Interfaces;
using Stages;
using Stages.Map;
using UnityEngine;

namespace InGame.Managers
{
    public class DreamSpawnManager : MonoBehaviour, IStageDataContainer
    {
        public StageDatabaseSO notes { get; set; }

        public StageDatabaseSO data
        {
            set => notes = value;
        }

        [SerializeField] private GameObject dream, nightmare;
        [SerializeField] private Transform noteDestination;
        private ScoreManager scoreManager;

        private void Start()
        {
            scoreManager = ScoreManager.Instance;
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (notes.entries.Count > 0)
            {
                var note = notes.entries[0];
                var type = Mathf.RoundToInt(note.type);

                GameObject noteObject;
                switch (type)
                {
                    case 0:
                        noteObject = Instantiate(dream, transform.position, Quaternion.identity);
                        break;
                    case 1:
                        noteObject = Instantiate(nightmare, transform.position, Quaternion.identity);
                        break;
                    default:
                        Debug.Log("Wrong type detected!");
                        yield break;
                }

                var noteMovement = noteObject.GetComponent<NoteMovement>();
                noteMovement.destination = noteDestination;
                noteMovement.duration = note.duration;
                if (notes.entries.Count <= 1)
                {
                    noteMovement.lastNote = true;
                }

                yield return new WaitForSeconds(note.time - scoreManager.stageTime);
                notes.entries.RemoveAt(0);
            }
        }
    }
}