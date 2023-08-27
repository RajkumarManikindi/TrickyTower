using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public class ZoomInAIDisplay : MonoBehaviour
    {

        private Animator _animator;
        private static readonly int OnClick = Animator.StringToHash("OnClick");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            GetComponent<Button>().onClick.AddListener(OnImageClick);
        }

        private void OnImageClick()
        {
            if (_animator != null)
            {
                _animator.SetTrigger(OnClick);
            }
        }
    }
}
