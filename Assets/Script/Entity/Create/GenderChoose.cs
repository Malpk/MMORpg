using UnityEngine;

public class GenderChoose : MonoBehaviour
{

    public event System.Action<EntityGender> OnChooseGender;

    #region Set
    public void SetMale()
    {
        SetGender(EntityGender.Male);
    }

    public void SetGirl()
    {
        SetGender(EntityGender.Girl);
    }
    #endregion

    private void SetGender(EntityGender input)
    {
        OnChooseGender?.Invoke(input);
    }    
}
