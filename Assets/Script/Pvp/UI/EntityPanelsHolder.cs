using UnityEngine;
using System.Collections.Generic;

public class EntityPanelsHolder : MonoBehaviour
{
    [SerializeField] private EntityDataPanel _prefab;
    [Header("Reference")]
    [SerializeField] private Transform _panelHolder;
    [SerializeField] private EntityDataPanel _mainPanel;

    public void AddEmtity(Entity entity)
    {

        if (_mainPanel.IsBusy)
        {
            var panel = Instantiate(_prefab.gameObject, _panelHolder).
                GetComponent<EntityDataPanel>();
            panel.BindPanel(entity);
        }
        else
        {
            _mainPanel.BindPanel(entity);
        }
    }
}
