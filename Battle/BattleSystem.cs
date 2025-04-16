using System;
using System.Collections;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem: MonoBehaviour 
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    public event Action<bool> OnBattleOver;

    public GameOverScreen GameOverScreen;
    public void GameOver()
    {
        GameOverScreen.Setup();
    }
    public GameOverScreen1 GameOverScreen1;
    public void GameOver1()
    {
        GameOverScreen1.Setup();
    }

    BattleState state;
    int currentAction;
    int currentMove;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }
    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.setData(playerUnit.Bojevalec);
        enemyHud.setData(enemyUnit.Bojevalec);
       

        yield return dialogBox.TypeDialog($"A Dragon appeared.");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }
    void PlayerAction()
    {
        state= BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);

    }
    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnit.Bojevalec.Moves[currentMove];
        yield return dialogBox.TypeDialog($"Player{playerUnit.Bojevalec.Base.Name} attacked {move.Base.Name}");
        
        yield return new WaitForSeconds(1f);

        bool isDead = enemyUnit.Bojevalec.TakeDamage(move, playerUnit.Bojevalec);
        enemyHud.UpdateHP();
        if (isDead)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Bojevalec.Base.Name}Dragon is dead.");
            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
            GameOver();
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }
    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Bojevalec.GetRandomMove();
        yield return dialogBox.TypeDialog($"Dragon{enemyUnit.Bojevalec.Base.Name} attacked {move.Base.Name}");

        yield return new WaitForSeconds(1f);

        float originalModifiers = UnityEngine.Random.Range(0.85f, 1f);
        float a = (2 + 10) / 250f;
        float d = a * move.Base.Power * ((float)enemyUnit.Bojevalec.Attack / playerUnit.Bojevalec.Defense) + 2;
        float randomMultiplier = UnityEngine.Random.Range(0.5f, 1.5f);
        d *= randomMultiplier;
        int damage = Mathf.FloorToInt(d * originalModifiers);

        bool isDead = playerUnit.Bojevalec.TakeDamage(damage);
        playerHud.UpdateHP();
        if (isDead)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Bojevalec.Base.Name}Player is dead.");
            yield return new WaitForSeconds(2f);
            OnBattleOver(false);
            GameOver1();
        }
        else
        {
            PlayerAction();
        }
    }

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }

    }
    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(currentAction<1)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
                --currentAction;
        }
        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(currentAction == 0)
            {
                
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                
            }

        }
        
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.Bojevalec.Moves.Count-1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                --currentMove;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Bojevalec.Moves.Count - 2)
                currentMove+=2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
                currentMove -=2;
        }
        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Bojevalec.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
