using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    [SerializeField]
    private Weapon _equippedWeapon;
    [SerializeField]
    private Transform _weaponTransform;

    private Vector2 _moveInput;

    private Vector3 _moveDirection;
    private Vector3 _curVelocity;
    private float _maxSpeed = 1.0f;
    private float _acceleration = 2.0f;
    private float _deceleration = 1.0f;


    protected void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        EquipWeapon(_equippedWeapon);
    }

    protected void Update()
    {
        _moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;

        Vector3 targetVelocity = _moveDirection.normalized * _maxSpeed;

        _curVelocity = Vector3.MoveTowards(
        _curVelocity,
        targetVelocity,
        (targetVelocity.magnitude > _curVelocity.magnitude ? _acceleration : _deceleration) * Time.deltaTime
        );

        // 이동 처리
        Vector3 movement = _curVelocity * Time.deltaTime;
        _characterController.Move(movement);

        _animator.SetFloat("Speed", _curVelocity.magnitude);
    }

    private void EquipWeapon(Weapon weapon)
    {
        weapon.Equipped(_weaponTransform);
    }




    //InputAction
    public void OnMove(InputValue input)
    {
        _moveInput = input.Get<Vector2>();
    }
    public void OnAttack(InputValue input)
    {
        if (_equippedWeapon == null) return;
        _equippedWeapon.Launch();
    }

}
