using UnityEngine;

public class MapPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _content;

    private bool _activate;
    private bool _block;

    public event System.Action<MapPoint> OnActive;

    public bool IsBlcok => _content || _block;

    public GameObject Content => _content;

    private void OnValidate()
    {
        name = "Point";
        if (_content)
            name += $" Contain[{_content.name}]";
    }

    private void OnMouseDown()
    {
        if (!IsBlcok)
        {
            _activate = true;
            OnActive?.Invoke(this);
            _animator.SetBool("active", _activate);
            _animator.SetBool("select", false);
        }
    }

    private void OnMouseEnter()
    {
        if (!IsBlcok)
        {
            if (!_activate)
            {
                _animator.SetBool("select", true);
            }
        }
        else
        {
            _animator.SetBool("none", true);
        }
    }

    private void OnMouseExit()
    {
        if (!_activate)
        {
            _animator.SetBool("select", false);
        }
        _animator.SetBool("none", false);
    }

    public void SetMode(bool mode)
    {
        _block = mode;
    }

    public void SetContent(GameObject content)
    {
        _content = content;
    }

    public void Deactivate()
    {
        _activate = false;
        _animator.SetBool("active", _activate);
    }
}
