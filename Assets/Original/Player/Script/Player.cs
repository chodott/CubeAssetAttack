using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    //BuildSystem
    private SphereCollider _sphereCollider;
    [SerializeField]
    private Button _buildButton;
    private Camera _aimCamera;

    //WeaponSystem
    [SerializeField]
    private Weapon _equippedWeapon;
    [SerializeField]
    private Transform _weaponTransform;

    private Vector2 _moveInput;
    private Vector2 _rotateInput;
    private float _rotateSensitivity = 10.0f;

    private Vector3 _moveDirection;
    private Vector3 _curVelocity;
    private float _maxSpeed = 1.0f;
    private float _acceleration = 2.0f;
    private float _deceleration = 1.0f;


    protected void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _sphereCollider = GetComponent<SphereCollider>();
        _aimCamera = transform.Find("Camera").GetComponent<Camera>();
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

        //BuildSystem
        RaycastHit hit;
        if (Physics.Raycast(_aimCamera.transform.position, _aimCamera.transform.forward, out hit, 20.0f))
        {
            if (hit.collider.CompareTag("BuildPlatform"))
            {
                _buildButton.interactable = true;
                BuildManager.Instance.buildTransform = hit.collider.transform;
            }
            else _buildButton.interactable = false;

            if(hit.collider.CompareTag("Enemy"))
            {
                _equippedWeapon.Launch(hit.collider.transform);
            }
        
        }
        else _buildButton.interactable = false;


        float touchX = _rotateInput.x * _rotateSensitivity * Time.deltaTime;
        float touchY = _rotateInput.y * _rotateSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up, touchX);
        transform.Rotate(transform.right, touchY);
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
        _equippedWeapon.Launch(transform);
    }

    public void OnRotate(InputValue input)
    {
        _rotateInput = input.Get<Vector2>();

    }

}
