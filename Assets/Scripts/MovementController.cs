using SimpleInputNamespace;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    /* WASD + mouse + on-screen joystick  */

    public Animator animator;

    [SerializeField] private GameObject _playerModel;
    [SerializeField] private CharacterController _characterController;

    public Joystick joystick;
    public float walkSpeed;
    private float walkAnimSmooth;
    private float magnitude;
    private Vector3 input;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
    }



    void Update()
    {

        Vector3 keyboardInput = GetKeyboardInput();
        Vector3 joystickInput = GetJoystickInput();
        magnitude = joystickInput.magnitude;

        
        // keyboardMagnitude = input.magnitude;
        if (joystickInput.magnitude > 0)
        {
            magnitude = joystickInput.magnitude;
            input = joystickInput;
        }
        else if (keyboardInput.magnitude > 0)
        {
            magnitude = keyboardInput.magnitude;
            input = keyboardInput;
        }

        if (magnitude > 0f)
        {
            _characterController.Move(input * Time.deltaTime * walkSpeed);
            _playerModel.transform.forward = input;
        }
        walkAnimSmooth = Mathf.Lerp(walkAnimSmooth, magnitude, 10 * Time.deltaTime);
        animator.SetFloat("Movement", walkAnimSmooth);
    }


    private Vector3 GetKeyboardInput()
    {
        Vector3 p_Velocity = new Vector3();
        if (SimpleInput.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (SimpleInput.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (SimpleInput.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (SimpleInput.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity.normalized;
    }

    private Vector3 GetJoystickInput()
    {
        return new Vector3(joystick.xAxis.value, 0, joystick.yAxis.value);
    }
}
