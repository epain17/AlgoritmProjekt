using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProjekt.Utility.AI
{
    class StateMachine <T>
    {
        private T myOwner;
        State<T> currentState;
        State<T> previousState;
        State<T> globalState;

        public StateMachine(T owner)
        {
            this.myOwner = owner;
            currentState = null;
            previousState = null;
            globalState = null;
        }

        public void SetCurrentState(State<T> s)
        {
            currentState = s;
        }

        public void SetGlobalState(State<T> s)
        {
            globalState = s;
        }

        public void SetPreviousState(State<T> s)
        {
            previousState = s;
        }

        public void Update()
        {
            if(globalState != null)
            {
                globalState.Execute(myOwner);
            }
            if(currentState != null)
            {
                currentState.Execute(myOwner);
            }
        }

        public void ChangeState(State<T> newState)
        {
            previousState = currentState;
            currentState.Exit(myOwner);
            currentState = newState;
            currentState.Enter(myOwner);
        }

        public void RevertToPreviousState()
        {
            ChangeState(previousState);
        }

        public State<T> CurrentState()
        {
            return currentState;
        }

        public State<T> PreviousState()
        {
            return previousState;
        }

        public State<T> GlobalState()
        {
            return globalState;
        }
    }
}
