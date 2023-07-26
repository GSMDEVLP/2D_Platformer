using System.Linq;
using UnityEngine;

public class MoveableMonster : Monsters
{
    [SerializeField] private float speed = 2f;

    private SpriteRenderer sprite;
    private Vector3 direction;

    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    protected override void Start()
    {
        direction = transform.right;
    }
    protected override void Update()
    {
        Move();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if(unit && unit is Player)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.7f) ReceiveDamage();
            else unit.ReceiveDamage();
        }
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5f + transform.right * direction.x * 0.5f, 0.1f); // задаем точку в параметре(текущая позиця находится в ногах врага + середина врага + правая ее часть)

        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Player>())) direction *= -1f; // colliders.Length = 0 если нет препятствий чтобы упал на другой блок 

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
