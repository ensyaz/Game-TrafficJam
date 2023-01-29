using DG.Tweening;
using System;
using UnityEngine;

public class GoldMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveOffset = 0.5f;
    private Transform _transform;
    private GoldCollisionController _goldCollisionInstance;

    private Tweener _moveTween;
    private Tweener _rotateTween;

    private void Awake()
    {
        _transform = transform;
        _goldCollisionInstance = GetComponent<GoldCollisionController>();
    }

    private void OnEnable()
    {
        _goldCollisionInstance.OnCollectEvent += KillTweener;
    }

    private void OnDisable()
    {
        _goldCollisionInstance.OnCollectEvent -= KillTweener;
    }

    private void Start()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 offset = new Vector3(_transform.position.x, _transform.position.y + _moveOffset, _transform.position.z);
        _moveTween = _transform.DOMove(offset, 2f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void Rotate()
    {
        _rotateTween = _transform.DORotate( new Vector3(0, 5f, 0) , 0.1f).SetLoops(-1, LoopType.Incremental);
    }

    private void KillTweener()
    {
        _moveTween.Kill();
        _rotateTween.Kill();
    }

}
