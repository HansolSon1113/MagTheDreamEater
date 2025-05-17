using DG.Tweening;
using Setting;
using Stages.Map;
using UnityEngine;

namespace Stages.Detail
{
    public class StagesMagAnimationManager : MonoBehaviour
    {
        private StagesManager stagesManager;
        private Animator animator;

        private void Start()
        {
            stagesManager = StagesManager.Instance;

            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (animator.GetBool("Move") != stagesManager.moving)
            {
                animator.SetBool("Move", stagesManager.moving);
            }

            var ee = animator.GetBool("EasterEgg");
            if (ee != EasterEggManager.Instance.easterEggOn)
            {
                animator.SetBool("EasterEgg", EasterEggManager.Instance.easterEggOn);
                var scale = ee ? new Vector3(0.15f, 0.15f, 1f) : new Vector3(0.25f, 0.25f, 1f);
                transform.DOScale(scale, 0.2f);
            }
        }
    }
}