using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    [SerializeField] private Transform target;

    private void Awake()
    {
        if (!target)
            target = FindObjectOfType<Player>().transform; // ищет активный объект текущего типа(который указан в <>)

    }

    private void Update()
    {
        Vector3 position = target.position;
        position.z = -10f;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
