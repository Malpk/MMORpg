using UnityEngine.SceneManagement;
using UnityEngine;

public class EndMenu : UIMenu
{
    [SerializeField] private int _mainMenuId = 1;
    [Header("Reference")]
    [SerializeField] private TextUI _reward;
    [SerializeField] private DataSaver _saver;

    public void ShowMenu(int reward = 0)
    {
        _reward.SetText(reward.ToString());
        Show();
    }

    public void ReturnMainMenu()
    {
        _saver.Save();
        SceneManager.LoadScene(_mainMenuId);
    }
}
