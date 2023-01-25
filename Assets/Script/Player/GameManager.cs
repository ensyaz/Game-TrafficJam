using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnGameOver();
    public event OnGameOver OnGameOverEvent;

    public static GameManager sharedInstance;
    private bool gameOver;
    private bool grounded;

    public bool IsGameOver { get => gameOver; set => gameOver = value; }
    public bool IsGrounded { get => grounded; set => grounded = value; }

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
