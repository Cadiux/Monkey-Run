using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject go;
    public LayerMask lm;
    bool IsAlive = true;
    private Rigidbody2D Rigidbody2D;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundlayer;





    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float XVelocity = Rigidbody2D.velocity.x;
        //Debug.Log($"{IsGrounded()}");
  
        
        if (IsAlive)
        {
            PlayerMovement();
            FlipPlayer(XVelocity);
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                Jump();
            }
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            IsAlive = false;
        }
    }
    void PlayerMovement()
    {
        float xinput = Input.GetAxis("Horizontal");
        Rigidbody2D.velocity = new Vector2(xinput * runSpeed, Rigidbody2D.velocity.y);
    }
    private bool IsGrounded()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 2f);
        return groundCheck.collider.IsTouchingLayers(groundlayer);
    }
    private void Jump() => Rigidbody2D.velocity = new Vector2(0, jumpPower);

    private void FlipPlayer(float Direction)
    {
        if (Direction < -0.1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Direction > 0.1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

}