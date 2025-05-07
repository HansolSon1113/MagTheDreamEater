using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface ISubmittable
    {
        public InputAction submitAction { get; set; }
    
        void Awake();
    
        void OnDestroy();

        void OnSubmitPerformed(InputAction.CallbackContext ctx);

        void Submit();
    }
}