using UnityEngine;
using UnityEngine.UI;
public class BattleHud : MonoBehaviour 
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar HPBar;

    Bojevalec _bojevalec;
    public void setData(Bojevalec bojevalec)
    {
        _bojevalec = bojevalec;
        nameText.text = bojevalec.Base.Name;
        HPBar.SetHP((float)bojevalec.HP / bojevalec.MaxHp);
    }

    public void UpdateHP()
    {
        HPBar.SetHP((float)_bojevalec.HP / _bojevalec.MaxHp);
    }
}
