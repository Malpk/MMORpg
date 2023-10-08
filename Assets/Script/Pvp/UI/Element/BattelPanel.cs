using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattelPanel : MonoBehaviour
{
    [SerializeField] private string _locationName;
    [SerializeField] private BattelData _battel;
    [SerializeField] private EntityPanel _panelPrefab;
    [Header("Reference")]
    [SerializeField] private Transform _panelHolder;
    [SerializeField] private TextMeshProUGUI _lable;

    private List<EntityPanel> _entites = new List<EntityPanel>();
    private List<EntityPanel> _pool = new List<EntityPanel>();

    private void Reset()
    {
        _locationName = "�������� �������";
    }

    private void OnValidate()
    {
        name = "BattelPanel";
        if (_battel)
        {
            name += $"[{_battel.name}]";
            _locationName = _battel.Location;
        }
        _lable?.SetText(_locationName);
    }

    private void Awake()
    {
        if (_battel)
            SetBattel(_battel);
    }

    public void SetBattel(BattelData data)
    {
        _lable.SetText(_battel.Location);
        _pool.AddRange(_entites);
        _entites.Clear();
        foreach (var entity in data.Entities)
        {
            var panel = GetPanel();
            panel.SetEntity(entity);
            _entites.Add(panel);
        }
    }

    private EntityPanel GetPanel()
    {
        if(_pool.Count > 0)
        {
            var panel = _pool[0];
            _pool.Remove(panel);
            return panel;
        }
        return Instantiate(_panelPrefab.gameObject, _panelHolder).
            GetComponent<EntityPanel>();
    }
}