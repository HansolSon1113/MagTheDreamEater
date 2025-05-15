using Stages.Map;
using UnityEngine;

namespace Stages.Detail
{
    public class StagesMagAnimationManager: MonoBehaviour
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
            if(animator.GetBool("Move") != stagesManager.moving)
                animator.SetBool("Move", stagesManager.moving);
        }
    }
}