using UnityEngine;

namespace UI.Setting
{
    public class ScreenSetter : MonoBehaviour
    {
        const int setWidth = 1920;
        const int setHeight = 1080;
        private int lastScreenWidth;
        private int lastScreenHeight;
        private float resolutionCheckInterval = 1f;
        private float timeSinceLastCheck;
        public static ScreenSetter Instance;

        private void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        private void Start()
        {
            if (Camera.main == null) return;

            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;

            UpdateResolution();
        }

        private void Update()
        {
            timeSinceLastCheck += Time.deltaTime;

            if (timeSinceLastCheck < resolutionCheckInterval) return;

            timeSinceLastCheck = 0f;

            if (Screen.width == lastScreenWidth && Screen.height == lastScreenHeight) return;

            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            UpdateResolution();
        }

        private static void UpdateResolution()
        {
            const float targetAspect = (float)setWidth / setHeight;
            var deviceAspect = (float)Screen.width / Screen.height;
            
            Screen.SetResolution(setWidth, setHeight, true);

            if (Mathf.Approximately(deviceAspect, targetAspect))
            {
                Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
            }
            else if (deviceAspect > targetAspect)
            {
                var newWidth = targetAspect / deviceAspect;
                Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
            }
            else
            {
                var newHeight = deviceAspect / targetAspect;
                Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
            }
        }
    }
}