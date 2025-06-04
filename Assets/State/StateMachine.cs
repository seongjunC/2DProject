using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState curState;
    public IState CurState => curState;

    public void ChangeState(IState newState)
    {

        if (curState != null) curState?.Exit();
        curState = newState;
        curState?.Enter();
    }

    public void Update()
    {
        curState?.Update();
    }

}
