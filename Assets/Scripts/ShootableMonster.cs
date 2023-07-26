using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : Monsters
{
    [SerializeField] private float rate = 2f;
    private Bullet bullet;
    [SerializeField] private Color bulletColor = Color.white;

    protected override void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
    }
    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    }
    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
        newBullet.Color = bulletColor;
    }

    protected override void OnTriggerEnter2D(Collider2D collider) 
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Player)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.3f) ReceiveDamage(); // коллизия по бокам
            else unit.ReceiveDamage();
        }
    }
}