using UnityEngine;

namespace Setting
{
    public class EasterEggManager : MonoBehaviour
    {
        public static EasterEggManager Instance;
        [HideInInspector] public bool easterEggOn = false;
        private bool processing = false;
        private float time = 0f;
        private int keyIndex = 0;
        private KeyCode[] currentSequence;
        private static readonly KeyCode[] easterEgg = { KeyCode.N, KeyCode.S, KeyCode.S, KeyCode.K }, basic = { KeyCode.O, KeyCode.R };

        private void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (processing)
            {
                time += Time.deltaTime;
                
                if (time > 0.5f)
                {
                    Debug.Log("Timeout!");
                    ResetSequence();
                    return;
                }

                if (!Input.anyKeyDown) return;
                if (Input.GetKeyDown(currentSequence[keyIndex]))
                {
                    Debug.Log(currentSequence[keyIndex]);
                    keyIndex++;
                    time = 0f;

                    if (keyIndex < currentSequence.Length) return;
                    Debug.Log("Success!");
                    if (currentSequence == easterEgg)
                        easterEggOn = true;
                    else if (currentSequence == basic)
                        easterEggOn = false;
                    ResetSequence();
                }
                else
                {
                    Debug.Log("Fail!");
                    ResetSequence();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("Starting easterEgg sequence");
                    StartSequence(easterEgg);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    Debug.Log("Starting basic sequence");
                    StartSequence(basic);
                }
            }
        }

        private void StartSequence(KeyCode[] sequence)
        {
            currentSequence = sequence;
            processing = true;
            keyIndex = 0;
            time = 0f;
        }

        private void ResetSequence()
        {
            processing = false;
            keyIndex = 0;
            time = 0f;
        }
    }
}