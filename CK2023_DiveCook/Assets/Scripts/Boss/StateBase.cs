using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(fileName = "BaseState", menuName = "FSM/State", order = 0)]
    public abstract class StateBase<T> : ScriptableObject where T : class
    {
        public abstract void Enter(T boss);
        public abstract void Execute(T boss);
        public abstract void Exit(T boss);
    }
}