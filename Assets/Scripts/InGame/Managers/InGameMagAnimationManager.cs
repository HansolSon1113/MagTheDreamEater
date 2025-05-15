using UnityEngine;

namespace InGame.Managers
{
    public class InGameMagAnimationManager : MonoBehaviour
    {
        private Animator animator;
        public static InGameMagAnimationManager Instance;
        private bool isAnimationPlaying = false;
        private string queuedAnimation = null;

        public bool eat
        {
            set
            {
                var clip = value ? "InGameMagEat" : "InGameMagWalk";
                PlayAnimation(clip);
            }
        }

        public bool thr
        {
            set
            {
                var clip = value ? "InGameMagThrow" : "InGameMagWalk";
                PlayAnimation(clip);
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("InGameMagWalk") ||
                !(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)) return;
            isAnimationPlaying = false;
            if (queuedAnimation != null)
            {
                animator.Play(queuedAnimation, 0, 0f);
                queuedAnimation = null;
                isAnimationPlaying = true;
            }
            else
            {
                animator.Play("InGameMagWalk");
            }
        }
        
        private void PlayAnimation(string clipName)
        {
            if (isAnimationPlaying && !clipName.Equals("InGameMagWalk"))
            {
                queuedAnimation = clipName;
                return;
            }
            
            animator.Play(clipName, 0, 0f);
            isAnimationPlaying = !clipName.Equals("InGameMagWalk");
        }
    }
}