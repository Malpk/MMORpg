using System.Collections.Generic;
using UnityEngine;

public class GameStateLog : MonoBehaviour
{
    [SerializeField] private TextUI _steap;
    [SerializeField] private Field _field;
    [SerializeField] private List<GameStateLog> _childs;

    private void OnValidate()
    {
        _childs.Remove(this);
    }

    public void SetSteap(int steap)
    {
        _steap.SetText(steap.ToString());
        foreach (var child in _childs)
        {
            child.SetSteap(steap);
        }
    }

    public void SetProgress(float progress)
    {
        _field.SetValue(progress);
        foreach (var child in _childs)
        {
            child.SetProgress(progress);
        }
    }
}
