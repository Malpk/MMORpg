using UnityEngine;
using System.Collections.Generic;

public class PvpControlelr : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _steapTime;
    [Header("Reference")]
    [SerializeField] private List<Entity> _partsPvp;
    [SerializeField] private Player _player;
    [SerializeField] private GameStateLog _game;
    [SerializeField] private PvpMenuHolder _inteface;
    [SerializeField] private AttackMenu _attack;

    private int _steapCount;
    private float _progress = 0f;

    private Entity _activeEntity;

    private List<Entity> _query = new List<Entity>();

    private void Reset()
    {
        _steapTime = 1f;
    }

    private void OnEnable()
    {
        foreach (var part in _partsPvp)
        {
            part.Body.OnDead += () => DeadPart(part);
        }
    }

    private void OnDisable()
    {
        foreach (var part in _partsPvp)
        {
            part.Body.OnDead -= () => DeadPart(part);
        }
    }

    private void Start()
    {
        Next();
    }

    private void Update()
    {
        _progress = Mathf.Clamp01(_progress + Time.deltaTime / _steapTime);
        _game.SetProgress(1 -_progress);
        if (_progress == 1)
        {
            _progress = 0;
            _activeEntity.Stop();
            _attack.Skip();
            _inteface.ShowHud();
        }
    }


    public void SetParts(List<Entity> parts)
    {
        enabled = false;
        _partsPvp = parts;
        _query.Clear();
        enabled = true;
        Next();
    }

    private void Next()
    {
        _progress = 0;
        if (_query.Count == 0)
        {
            _steapCount++;
            _game.SetSteap(_steapCount);
            _query.AddRange(_partsPvp);
        }
        SetActive(_query[0]);
        _query.Remove(_query[0]);
    }


    private void DeadPart(Entity entity)
    {
        entity.Stop();
        _query.Remove(entity);
        _partsPvp.Remove(entity);
    }

    private void SetActive(Entity next)
    {
        if (_activeEntity)
        {
            _activeEntity.OnComplite -= Next;
        }
        _activeEntity = next;
        _activeEntity.Body.ReloadPart();
        _activeEntity.OnComplite += Next;
        _activeEntity.Play();
    }
}
