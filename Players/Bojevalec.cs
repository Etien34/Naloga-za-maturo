using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class Bojevalec
{
    public PlayerBase Base { get; set; }

    
    public List<Move> Moves { get; set; }

    public int HP { get; set; }

    public Bojevalec(PlayerBase pbase)
    {
        Base = pbase;
        HP = MaxHp;
        Moves = new List<Move>();

        foreach (var move in Base.LearnableMoves)
        {
            Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
            {
                break;
            }
        }

    }
    public int Attack
    {
        get
        {
            return Mathf.FloorToInt(5 / 100f) + 5;
        }
    }
    public int MaxHp
    {
        get
        {
            return Mathf.FloorToInt(5 / 100f) + 5;
        }
    }
    public int Defense
    {
        get
        {
            return Mathf.FloorToInt(5/100f)+10;
        }
    }
    public bool TakeDamage(Move move, Bojevalec attacker)
    {
        float modifiers = UnityEngine.Random.Range(0.85f, 1f);
        float a = (2 + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        float randomMultiplier = UnityEngine.Random.Range(0.5f, 1.5f);
        d *= randomMultiplier;

        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }
        return false;
    }
    public Move GetRandomMove()
    {
        int r=Random.Range(0,Moves.Count);
        return Moves[r];
    }
    public bool TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }
        return false;
    }
}
