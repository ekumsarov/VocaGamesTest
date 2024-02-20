using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState 
{
    private List<StateContainer> _states;
    private int _currentState;

    public MoveState()
    {
        _states = new List<StateContainer>();
        _currentState = 0;
    }

    public MoveState(List<StateContainer> states)
    {
        if(states == null || states.Count == 0)
            _states = new List<StateContainer>();
        else
        {
            _states = states;
        }
            

        _currentState = -1;
    }

    public StateContainer NextState()
    {
        if(_states.Count == 0)
        {
            return new StateContainer(new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)), Random.Range(1f, 5f));
        }
        else
        {
            _currentState += 1;
            if(_currentState >= _states.Count)
            {
                return null;
            }
            else
                return _states[_currentState];
        }
    }
}
