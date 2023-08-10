using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(fileName = "GuvIdle", menuName = "FSM/GuvIdle", order = 1)]
public class GuvIdle : StateBase<Guv>
{
    [SerializeField] StateBase<Guv> MoveState;
    [SerializeField] StateBase<Guv> DieState;
    [SerializeField] StateBase<Guv> RushState;
    [SerializeField] StateBase<Guv> FaintState; // 기절

    public override void Enter(Guv guv)
    {
        Debug.Log("Idle State");
    }

    public override void Execute(Guv guv)
    {
        if(guv.CurrentHp <= 0)
        {
            guv.ChangeState(DieState);
            return;
        }
        else if(guv)
        {

        }
    }

    public override void Exit(Guv guv)
    {
        
    }
}