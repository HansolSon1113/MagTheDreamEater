using System.Collections;
using InGame.Notes;
using Interfaces;
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
        [SerializeField] private Transform noteSpawnPoint, noteDestination, noteEndPoint;
        private ScoreManager scoreManager;
        private float ratio;
        [SerializeField] private AudioManager audioManager;

        public AudioClip music
        {
            set
            {
                audioManager.musicAudioSource.clip = value;
                audioManager.musicAudioSource.Play();
            }
        }
        
        private void Start()
        {
            scoreManager = ScoreManager.Instance;
            
            ratio = 1 + (noteDestination.position.x - noteEndPoint.position.x) /
                (noteSpawnPoint.position.x - noteDestination.position.x);
            
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (notes.entries.Count > 0)
            {
                var note = notes.entries[0];
                yield return new WaitForSeconds(note.startTime - scoreManager.stageTime);
                
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
                        Debug.LogError("Wrong type detected! Must be 0 or 1.");
                        yield break;
                }

                var noteMovement = noteObject.GetComponent<NoteMovement>();
                var duration = (note.endTime - scoreManager.stageTime) * ratio;
                noteMovement.duration = duration;
                noteMovement.endPoint = noteEndPoint;
                if (notes.entries.Count <= 1)
                {
                    noteMovement.lastNote = true;
                }
                
                notes.entries.RemoveAt(0);
            }
        }
    }
}