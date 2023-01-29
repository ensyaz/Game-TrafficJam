using UnityEngine;

public class Sample : MonoBehaviour
{
    Animator animator;
    bool isWalking;
    bool isRunning;
    bool forwardPressed;
    bool runPressed;

    float velocity;
    public float accelaration;
    int velocityHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
       forwardPressed = Input.GetKey(KeyCode.W);
       runPressed = Input.GetKey(KeyCode.LeftShift);
        //ProcessMovement(); 
       Movement();
    }

    void Movement()
    {
        if(forwardPressed && velocity < 1.0f)
        {
            velocity += Time.deltaTime * accelaration;
        }

        if(!forwardPressed && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * accelaration;
        }

        animator.SetFloat(velocityHash, velocity);

    }
    void ProcessMovement()
    {
        Walk();
        Run();
    }

    void Walk()
    {
        isWalking = animator.GetBool("isWalking");

        if (!isWalking && forwardPressed)
        {
            animator.SetBool("isWalking", true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool("isWalking", false);
        }
    }

    void Run()
    {
        isRunning = animator.GetBool("isRunning");

        if (!isRunning && forwardPressed && runPressed)
        {
            animator.SetBool("isRunning", true);
        }

        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool("isRunning", false);
        }

    }

    

   

    
}
