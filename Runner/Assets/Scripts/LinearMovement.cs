using System;
using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    [SerializeField, Range(1,20)]
    private float speed = 6f;

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        this.transform.position += new Vector3(0, 0, -speed * Time.deltaTime); 
    }
}
