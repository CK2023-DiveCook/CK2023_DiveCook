using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public sealed class StateMachine<T> where T : MonoBehaviour
    {
        private T Boss;
        private StateBase<T> currState = null;
        private StateBase<T> prevState = null;

        private StateMachine() { }
        public StateMachine(T boss)
        {
            Boss = boss;
        }
        public StateBase<T> CurrentState { get { return currState; } }
        public StateBase<T> PreviousState { get { return prevState; } }
        public void SetCurrentState(StateBase<T> state) { if (null != state) currState = state; }
        public void SetPreviousState(StateBase<T> state) { if (null != state) prevState = state; }
        public void Update() { if (null != currState && Boss) currState.Execute(Boss); }
        public void ChangeState(StateBase<T> newState)
        {
            if (null == newState) return;

            prevState = currState;
            currState.Exit(Boss);
            currState = newState;
            currState.Enter(Boss);
        }
    }
}