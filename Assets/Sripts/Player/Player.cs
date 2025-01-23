using System;
using DG.Tweening;
using GameInput;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private float _gridSize;

    

    private void OnEnable()
    {
        _inputReader.Left += OnLeft;
        _inputReader.Right += OnRight;
    }

    private void OnRight()
    {
        transform.DOMove(transform.position + Vector3.right * _gridSize, 0.5f);
    }

    private void OnLeft()
    {
        transform.DOMove(transform.position + Vector3.left * _gridSize, 0.5f);
    }

    private void OnDisable()
    {
        _inputReader.Left -= OnLeft;
        _inputReader.Right -= OnRight;
    }
}