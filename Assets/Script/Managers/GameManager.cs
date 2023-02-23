using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    public delegate void OnGameOver();
    public event OnGameOver OnGameOverEvent;

    private bool _gameOver;
    private bool _grounded;
    private int _goldCount;

    public bool IsGameOver { get => _gameOver; set => _gameOver = value; }
    public bool IsGrounded { get => _grounded; set => _grounded = value; }
    public int GoldCount { get => _goldCount; set => _goldCount = value; }

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Update()
    {
        if (IsGameOver)
            OnGameOverEvent?.Invoke();
    }






}
