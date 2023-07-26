using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject parent;
    public GameObject Parent { set { parent = value; } get { return parent; } }

    private Vector3 direction; // направление пули
    private float speed= 10;
    public Vector3 Direction { set { direction = value; } } // устанавливаем напрвление пули через Player 

    public Color Color
    {
        set { sprite.color = value; }
    }


    private SpriteRenderer sprite;


    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit.gameObject != parent)
        {
            Destroy(gameObject);
        }
    }
}
