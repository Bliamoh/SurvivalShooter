using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6f;

    private Vector3 _movement;
    private Animator _anim;
    private Rigidbody _playerRigidBody;
    private int _floorMask;
    private float _camRayLength = 100f;

    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _anim = GetComponent<Animator>();
        _playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        _movement.Set(h, 0f, v);

        _movement = _movement.normalized * Speed * Time.deltaTime;

        _playerRigidBody.MovePosition(transform.position + _movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, _camRayLength))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0 || v != 0;
        _anim.SetBool("IsWalking", walking);
    }
}
