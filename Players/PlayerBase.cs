using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Players", menuName = "Players/Create new players")]
public class PlayerBase : ScriptableObject
{
    [SerializeField] string name;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;

    [SerializeField] List<LearnableMove> learnableMoves;

   
    public string Name
    {
        get { return name; }
    }
    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }
    public Sprite BackSprite
    {
        get { return backSprite; }
    }
    public int MaxHp
    {
        get { return maxHp; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public int Defense
    {
        get { return defense; }
    }   
    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }

}
[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase move;     //tukaj sem popravljal (move)
    public MoveBase Base
    {
        get { return move; }
    }
}
