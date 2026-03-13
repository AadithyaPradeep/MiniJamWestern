using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] private InputActionReference movement;
    public float speed = 1;
    private Vector2 movementVector;
    private bool movementKeyDown;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool rolling;
    public float dashAmount;
    private bool jumpKeyDown = false;
    private bool canroll = true;
    public float rollDelay;
    public float rollTime = 1f;
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && canroll)
        {
            Roll();
        }
        if (!rolling)
        {

            if (GetLiveDirection().x < 0)
            {
                spriteRenderer.flipX = true;

                animator.SetBool("Running", true);
            }
            if (GetLiveDirection().x > 0)
            {
                spriteRenderer.flipX = false;
                animator.SetBool("Running", true);

            }
            if (GetLiveDirection().x == 0)
            {

                animator.SetBool("Running", false);
            }
            if (GetLiveDirection().y != 0)
            {
                animator.SetBool("Running", true);
            }
        }
    }
    void OnEnable()
    {
        rolling = false;
        movement.action.Enable();
        movement.action.performed += OnMovementKeyDown;
        movement.action.canceled += OnMovementKeyUp;
    }



    void OnDisable()
    {
        movement.action.performed -= OnMovementKeyDown;
        movement.action.canceled -= OnMovementKeyUp;
        movement.action.Disable();
    }

    private void FixedUpdate()
    {
        if (movementKeyDown && !rolling)
        {
            transform.position += speed * Time.deltaTime * new Vector3(movementVector.x, movementVector.y, 0);

        }
    }



    private void OnMovementKeyDown(InputAction.CallbackContext context)
    {
        movementKeyDown = true;
        var result = context.ReadValue<Vector2>();
        movementVector = result;
    }
    private void OnMovementKeyUp(InputAction.CallbackContext context)
    {
        movementKeyDown = false;
    }

    public Vector2 GetLastDirection()
    {
        return movementVector;
    }
    public void Roll()
    {
        canroll = false;
        rolling = true;
        animator.SetTrigger("Roll");

        StartCoroutine("rollin");
       

    }
    public Vector2 GetLiveDirection()
    {
        if (movementKeyDown == false) return Vector2.zero;
        return movementVector;
    }
    private IEnumerator rollin()
    {
        
        float elapsed = 0f;

        Vector2 dir = GetLiveDirection();
        if (dir == Vector2.zero)
            dir = GetLastDirection();

        while (elapsed < rollTime)
        {
            transform.position += dashAmount * Time.deltaTime * new Vector3(dir.x, dir.y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rolling = false;

        StartCoroutine("timer");
    }
    private IEnumerator timer()
    {
        canroll = false;
        yield return new WaitForSeconds(rollDelay);
        canroll = true;

    }
}