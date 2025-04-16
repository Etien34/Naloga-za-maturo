using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour 
{
    [SerializeField] PlayerBase _base;
    [SerializeField] bool isPlayerUnit;

    public Bojevalec Bojevalec{get; set;}

    public void Setup()
    {
        Bojevalec =new Bojevalec(_base);
        if (isPlayerUnit)
            GetComponent<Image>().sprite = Bojevalec.Base.BackSprite;
        else
            GetComponent<Image>().sprite = Bojevalec.Base.FrontSprite;
    }
}
