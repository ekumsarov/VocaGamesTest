using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] 
    private Player _prefab;

    private List<StateContainer> _states;
    private Player _mainPlayer;

    private void Start()
    {
        _states = new List<StateContainer>();
        _mainPlayer = GameObject.Instantiate<Player>(_prefab);
        _mainPlayer.Initialize(new MoveState());
        _mainPlayer.StateChanged += StateChanged;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            _mainPlayer.StateChanged -= StateChanged;
            Destroy(_mainPlayer.gameObject);

            GameObject.Instantiate<Player>(_prefab).Initialize(new MoveState(_states));
            _states = new List<StateContainer>();

            _mainPlayer = GameObject.Instantiate<Player>(_prefab);
            _mainPlayer.Initialize(new MoveState());
            _mainPlayer.StateChanged += StateChanged;
            
        }
    }

    private void StateChanged(StateContainer state)
    {
        _states.Add(state);
    }
}
