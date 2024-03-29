using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager sharedInstance;

    public bool IsGameOver { get => _gameOver; set => _gameOver = value; }
    public bool IsGrounded { get => _grounded; set => _grounded = value; }
    public int GoldCount { get => _goldCount; set => _goldCount = value; }
    public float finalScore { get => _finalScore; set => _finalScore = value; }

    #region Events
    public delegate void OnGameOver();
    public event OnGameOver OnGameOverEvent;
    public delegate void OnSetSpeedJumpTiming();
    public event OnSetSpeedJumpTiming OnSetSpeedJumpTimingEvent;
    #endregion

    [SerializeField]
    private Transform playerTransform;

    private bool _gameOver;
    private bool _grounded;
    private int _goldCount;
    private float _paramChangeRange = 150f;
    private float _initialPosition = 0;
    private float _finalScore;

    

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

    // Increase and decrease the speed of player and jumping duration according to distance
    private void SetSpeedJumpTiming()
    {
        if (playerTransform.position.z - _initialPosition >= _paramChangeRange)
        {
            OnSetSpeedJumpTimingEvent?.Invoke();
            _initialPosition = playerTransform.position.z;   
        }
    }
}
