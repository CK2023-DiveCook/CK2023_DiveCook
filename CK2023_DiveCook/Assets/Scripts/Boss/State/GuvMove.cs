using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[CreateAssetMenu(fileName = "GuvMove", menuName = "FSM/GuvMove", order = 2)]
public class GuvMove : StateBase<Guv>
{
    [SerializeField] StateBase<Guv> IdleState;
    public override void Enter(Guv guv)
    {
        Debug.Log("Move State");
    }
    public override void Execute(Guv guv)
    {
        
    }
    public override void Exit(Guv guv)
    {

    }
}
