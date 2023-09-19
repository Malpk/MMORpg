using UnityEngine;
using TMPro;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _playerRang;

    public void LoadPlayer(EntityData data)
    {
        _playerName.SetText(data.Name);
        _playerRang.SetText(data.Rang.ToString());
    }
}
