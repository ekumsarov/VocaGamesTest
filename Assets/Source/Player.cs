using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private float _timeStamp;
    private MoveState _moveState;
    private StateContainer _currentState;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    public Action<StateContainer> StateChanged;
    public Action<Vector3> PositionChanged;
    
    public void Initialize(MoveState state)
    {
        if(_meshRenderer == null)
        {
            gameObject.TryGetComponent<MeshRenderer>(out _meshRenderer);
            if(_meshRenderer == null)
                throw new ArgumentNullException();
        }

        transform.position = new Vector3(0f, 0f, 0f);
        _timeStamp = 0f;
        _moveState = state;
        _currentState = _moveState.NextState();
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = _currentState.Direction * _currentState.Speed * Time.deltaTime;
        transform.position += acceleration;
        PositionChanged?.Invoke(transform.position);

        _timeStamp += Time.deltaTime;
        if(_timeStamp >= 1f)
        {
            _currentState = _moveState.NextState();
            if(_currentState == null)
                Destroy(gameObject);

            _timeStamp = 0f;
            StateChanged?.Invoke(_currentState);
        }
    }
}
