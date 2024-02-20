using System.Collections.Generic;
using UnityEngine;

public class StateContainer 
{
    private Vector3 _direction;
    public Vector3 Direction => _direction;
    private float _speed;
    public float Speed => _speed;

    public StateContainer(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }
}