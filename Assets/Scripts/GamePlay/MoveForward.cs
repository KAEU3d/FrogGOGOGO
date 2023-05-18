using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Vector2 startPos;

    public int direction;

    private void Start()
    {
        startPos = transform.position;
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - startPos.x) > 25)
        {
            Destroy(this.gameObject);
        }
        move();
    }

    private void move()
    {
        transform.position += transform.right * speed * direction * Time.deltaTime;
    }

}
