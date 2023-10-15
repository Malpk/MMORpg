using UnityEngine;

public class GlorySet : MonoBehaviour
{
    [SerializeField] private int _glory;
    [Header("Reference")]
    [SerializeField] private TextUI[] _text;

    public int Glory {
        get 
        {
            return _glory;
        }
        private set
        {
            _glory = value;
            SetText(_glory);
        }
    }

    private void Awake()
    {
        SetText(_glory);
    }

    public void SetGlroy(int glory)
    {
        Glory = glory;
    }

    public void AddGlory(int glory)
    {
        Glory += glory;
    }

    public void RemoveGlory(int glory)
    {
        Glory -= glory;
    }

    private void SetText(int glory)
    {
        foreach (var text in _text)
        {
            text.SetText(glory.ToString());
        }
    }

}
