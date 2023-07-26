using UnityEngine;

public class Player : Unit
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;

    public int Lives
    {
        get { return lives; }
        set
        {
            if (value < 5) lives = value;
            livesBar.Refresh();
        }
    }
    private LivesBar livesBar;

    [SerializeField] private float jumpForce = 15f;   


    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isGrounded = false;

    private Bullet bullet;

    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet"); // подгружаем ссылку на Prefab - bullet
    }

    private void FixedUpdate()
    {
        if (!isGrounded) State = CharState.Jump;
    }

    private void Update()
    {
        if(isGrounded) State = CharState.Idle;

        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Fire1")) Shoot();
        if (isGrounded && Input.GetButtonDown("Jump"))
        {            
            Jump();
            isGrounded = false;            
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0;

        if (isGrounded) State = CharState.Run;

    }

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.8f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1f : 1f);


    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);        
    }

    public override void ReceiveDamage()
    {
        Lives--;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 8f, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") 
            isGrounded = true;

        Unit unit = collision.gameObject.GetComponent<Unit>();

        if (unit)
            ReceiveDamage();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (bullet && bullet.Parent != gameObject)
            ReceiveDamage();
    }
}

public enum CharState
{
    Idle,
    Run,
    Jump
}
