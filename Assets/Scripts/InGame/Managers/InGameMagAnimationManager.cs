using Setting;
using UnityEngine;

namespace InGame.Managers
{
    public class InGameMagAnimationManager : MonoBehaviour
    {
        private Animator animator;
        public static InGameMagAnimationManager Instance;
        private bool isAnimationPlaying = false;
        private string queuedAnimation = null, animWalk = "InGameMagWalk", animEat = "InGameMagEat", animThrow = "InGameMagThrow";

        public bool eat
        {
            set
            {
                var clip = value ? animEat : animWalk;
                PlayAnimation(clip);
            }
        }

        public bool thr
        {
            set
            {
                var clip = value ? animThrow : animWalk;
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

            var ee = EasterEggManager.Instance.easterEggOn;
            if (ee)
            {
                animWalk = "InGameEEWalk";
                animEat = "InGameEEEat";
                animThrow = "InGameEEThrow";
                
                animator.Play(animWalk);
            }
        }

        private void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animWalk) ||
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
                animator.Play(animWalk);
            }
        }
        
        private void PlayAnimation(string clipName)
        {
            if (isAnimationPlaying && !clipName.Equals(animWalk))
            {
                queuedAnimation = clipName;
                return;
            }
            
            animator.Play(clipName, 0, 0f);
            isAnimationPlaying = !clipName.Equals(animWalk);
        }
    }
}