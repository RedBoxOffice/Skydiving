using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speedWalking;
    [SerializeField] private float _speedRunning;
    [SerializeField] private float _turnSmoothTime;
    [SerializeField] private Transform _camera;
    
    private float _turnSmoothVelocity;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();        
    }

    void Update()
    {
        MoveHero();
    }

    private void MoveHero()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            if (Input.GetKey(KeyCode.LeftShift))
            {                
                transform.position += moveDirection * _speedRunning * Time.deltaTime;
            }
            else
            {                
                transform.position += moveDirection * _speedWalking * Time.deltaTime;
            }
        }       
    }
}
