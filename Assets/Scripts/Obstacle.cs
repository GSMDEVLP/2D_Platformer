using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>(); // проверка на соответствие соответствующего компонента
        
        if(unit && unit is Player)
        {
            unit.ReceiveDamage();
        }
    }
}
