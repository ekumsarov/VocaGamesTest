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
    private List<IAbility> _abilities;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    public Action<StateContainer> OnStateChanged;
    public Action<Vector3> OnPositionChanged;
    
    public void Initialize(MoveState state)
    {
        if(_meshRenderer == null)
        {
            gameObject.TryGetComponent<MeshRenderer>(out _meshRenderer);
            if(_meshRenderer == null)
                throw new ArgumentNullException();
        }

        _abilities = new List<IAbility>();
        transform.position = new Vector3(0f, 0f, 0f);
        _timeStamp = 0f;
        _moveState = state;
        _currentState = new StateContainer(Vector3.zero, 0f);

        _abilities.Add(new DistanceAbility());
        _abilities.Add(new SpeedAbility());

        for(int i = 0; i < _abilities.Count; i++)
        {
            _abilities[i].InitializeAbility(this);
        }

        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = _currentState.Direction * _currentState.Speed * Time.deltaTime;
        transform.position += acceleration;
        OnPositionChanged?.Invoke(transform.position);

        _timeStamp += Time.deltaTime;
        if(_timeStamp >= 1f)
        {
            _currentState = _moveState.NextState();
            if(_currentState == null)
            {
                DestroyPlayer();
            }
                

            _timeStamp = 0f;
            OnStateChanged?.Invoke(_currentState);
        }
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }

    public void DestroyPlayer()
    {
        for(int i = 0; i < _abilities.Count; i++)
        {
            _abilities[i].Dispose();
        }
        Destroy(gameObject);
    }
}
