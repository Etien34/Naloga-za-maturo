using UnityEngine;
[CreateAssetMenu(fileName ="Move",menuName ="Players/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string name;

    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int timesPerformed;

    public string Name
    {
        get { return name; }
    }
    public int Power
    {
        get { return power; }
    }
    public int Accuracy
    {
        get { return accuracy; }
    }
    public int TimesPerformed
    {
        get { return timesPerformed; }
    }

}
