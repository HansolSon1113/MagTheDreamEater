using UnityEngine;

namespace InGame.Managers
{
    public class InGameMagAnimationManager : MonoBehaviour
    {
        private Animator animator;
        public static InGameMagAnimationManager Instance;
        private bool _eat, _thr;

        public bool eat
        {
            set
            {
                _eat = value;
                animator.SetBool("Eat", value);
            }
        }

        public bool thr
        {
            set
            {
                _thr = value;
                animator.SetBool("Throw", value);
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
            if (_eat && !animator.GetCurrentAnimatorStateInfo(0).IsName("Eat"))
            {
                eat = false;
            }

            if (_thr && !animator.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
            {
                thr = false;
            }
        }
    }
}