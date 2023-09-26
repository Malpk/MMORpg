using UnityEngine;

public class PvpControlelr : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _steapTime;
    [Header("Reference")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Player _player;
    [SerializeField] private Field _steapProgressField;
    [SerializeField] private TextUI _steapText;

    private int _steapCount;
    private float _progress = 0f;

    private IPvp _pvp;

    private void Reset()
    {
        _steapTime = 1f;
    }


    private void OnEnable()
    {
        _enemy.OnComplite += SetPlayer;
        _player.OnComplite += SetEnemy;
    }

    private void OnDisable()
    {
        _enemy.OnComplite -= SetPlayer;
        _player.OnComplite -= SetEnemy;
    }

    private void Start()
    {
        SetPlayer();
    }

    private void Update()
    {
        _progress = Mathf.Clamp01(_progress + Time.deltaTime / _steapTime);
        _steapProgressField.SetValue(1 -_progress);
        if (_progress == 1)
        {
            _progress = 0;
            _pvp.Skip();
        }
    }

    private void SetEnemy()
    {
        _pvp = _enemy;
        _pvp.Play();
        _progress = 0;
    }

    private void SetPlayer()
    {
        _pvp = _player;
        _steapCount++;
        _steapText.SetText(_steapCount.ToString());
        _pvp.Play();
        _progress = 0;
    }
}
