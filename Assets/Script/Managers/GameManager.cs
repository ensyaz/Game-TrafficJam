using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    public delegate void OnGameOver();
    public event OnGameOver OnGameOverEvent;

    public delegate void OnSetSpeedJumpTiming();
    public event OnSetSpeedJumpTiming OnSetSpeedJumpTimingEvent;

    [SerializeField]
    private Transform playerTransform;

    private bool _gameOver;
    private bool _grounded;
    private int _goldCount;
    private float _speedIncreaseRate = 150f;
    private float _initialPosition = 0;
    private float _finalScore;

    public bool IsGameOver { get => _gameOver; set => _gameOver = value; }
    public bool IsGrounded { get => _grounded; set => _grounded = value; }
    public int GoldCount { get => _goldCount; set => _goldCount = value; }
    public float finalScore { get => _finalScore; set => _finalScore = value; }

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Update()
    {
        if (IsGameOver)
            OnGameOverEvent?.Invoke();

        SetSpeedJumpTiming();
    }

    private void SetSpeedJumpTiming()
    {
        if (playerTransform.position.z - _initialPosition >= _speedIncreaseRate)
        {
            OnSetSpeedJumpTimingEvent?.Invoke();
            _initialPosition = playerTransform.position.z;   
        }
    }

 








}
