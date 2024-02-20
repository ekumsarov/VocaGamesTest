using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAbility : IAbility
{
    private Vector3 _position;
    private float distance;

    private Player _player;

    public void Dispose()
    {
        _player.OnPositionChanged -= PositionChanged;
        _player = null;
    }

    public void InitializeAbility(Player player)
    {
        player.OnPositionChanged += PositionChanged;
        _player = player;
        _position = Vector3.zero;
        distance = 0f;
    }

    private void PositionChanged(Vector3 position)
    {
        distance += Vector3.Distance(_position, position);
        _position = position;
        if(distance > 3f)
        {
            distance = 0f;
            _player.SetColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        }
    }
}
