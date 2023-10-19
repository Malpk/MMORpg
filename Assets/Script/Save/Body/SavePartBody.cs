[System.Serializable]
public class SavePartBody 
{
    public int FullHealth = -1;
    public int Health = -1;
    public int Damage = 0;
    public int ArmorId = -1;
    public float HealProgress = 0f;
    public PartType Part;
    public SavePartState State;
}
