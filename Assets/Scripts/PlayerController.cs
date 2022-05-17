using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private FloatingJoystick _joystick;

    private Rigidbody _rb;

    private Animator _animator;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _center;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = _player.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_rb.position.magnitude < 2 )
        {
            _animator.SetBool("IsNearEnemy", true);

            GameEvens.enemyDamage.Invoke();
        }
        else
        {
            _animator.SetBool("IsNearEnemy", false);
        }
    }
    private void FixedUpdate()
    {
        _center.transform.Rotate(Vector3.down, _joystick.Horizontal * 0.8f);

        if (_joystick.Vertical > 0)
        {
            _rb.velocity = (_rb.position * -(5  / _rb.position.magnitude));
        }
        else if (_joystick.Vertical < 0)
        {
            _rb.velocity = (_rb.position * (5 / _rb.position.magnitude));
        }
        else { _rb.velocity = Vector3.zero; }
 
        _animator.SetFloat("Horizontal", _joystick.Horizontal * 10);

        _animator.SetFloat("Vertical", _joystick.Vertical * 10);

        transform.LookAt(Vector3.zero);
    }


}
