using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RadioPoint : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private string _decription;
    [SerializeField] private Animator _animator;
    [Header("Event")]
    [SerializeField] private UnityEvent _onClick;

    public event System.Action<RadioPoint> OnSelect;

    public string Description => _decription;

    public void Select()
    {
        _onClick.Invoke();
        OnSelect?.Invoke(this);
        _animator.SetBool("select", true);
    }

    public void Reload()
    {
        _animator.SetBool("select", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }


}
