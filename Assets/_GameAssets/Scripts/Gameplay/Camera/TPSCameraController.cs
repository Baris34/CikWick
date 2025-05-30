using System;
using UnityEngine;

public class TPSCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerVisualTransform;
    [SerializeField] private Transform _orientationTransform;
    [SerializeField] private Transform _playerTransform;

    [Header("Settings")]
    [SerializeField] private float _cameraSpeed = 5f;
    
    private Vector2 _playerInput;

    private void Update()
    {
        Vector3 viewDirection = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
        _orientationTransform.forward = viewDirection.normalized;
        
        _playerInput.x = Input.GetAxis("Horizontal");
        _playerInput.y = Input.GetAxis("Vertical");

        Vector3 inputDirection = _orientationTransform.forward * _playerInput.y + _playerInput.x * _orientationTransform.right;
        
        if(inputDirection != Vector3.zero)
            _playerVisualTransform.forward = Vector3.Slerp(_playerVisualTransform.forward, inputDirection.normalized,_cameraSpeed * Time.deltaTime);

    }
}
