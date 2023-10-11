using UnityEngine;

public class MapPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _content;

    private bool _activate;
    private bool _block;

    public event System.Action<MapPoint> OnActive;
    public event System.Action<MapPoint> OnExit;

    public GameObject Content => _content;

    private void OnValidate()
    {
        name = "Point";
        if (_content)
            name += $" Contain[{_content.name}]";
    }

    private void OnMouseDown()
    {
        if (!_block)
        {
            if (!_activate)
            {
                _activate = true;
                OnActive?.Invoke(this);
                _animator.SetBool("active", _activate);
                _animator.SetBool("select", false);
            }
            else
            {
                _activate = false;
                OnExit?.Invoke(this);
                _animator.SetBool("active", false);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (!_block)
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

    public bool SetBlock(bool mode)
    {
        if (!_content)
        {
            _block = mode;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetContent(GameObject content)
    {
        SetBlock(false);
        _content = content;
        if(_content)
            _content.transform.position = transform.position;
    }

    public void Deactivate()
    {
        _activate = false;
        _animator.SetBool("active", _activate);
    }
}
