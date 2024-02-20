using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAbility : IAbility
{
    private Player _player;

    public void Dispose()
    {
        _player.OnStateChanged -= StateChanged;
    }

    public void InitializeAbility(Player player)
    {
        _player = player;
        _player.OnStateChanged += StateChanged;
    }

    private void StateChanged(StateContainer state)
    {
        
        if(state.Speed > 2f)
        {
            _player.SetColor(Color.green);
        }
            
    }
}
