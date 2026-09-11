using UnityEngine;

namespace _project.Scripts.ScriptableObjects
{
    public class SelectionArrowScript : MonoBehaviour
    {
        private static readonly int Selected = Animator.StringToHash("Selected");
        [SerializeField] private GameObject _arrow;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Select()
        {
            _arrow.SetActive(true);
            _animator.SetBool(Selected, true);
	    }

        public void Deselect()
        {
            _arrow.SetActive(false);
            _animator.SetBool(Selected, false);
        }

    }
}