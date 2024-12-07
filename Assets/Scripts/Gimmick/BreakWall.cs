using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    [SerializeField]Collider2D col;
    SpriteRenderer spriteRenderer;


    void Start()
    {
        col = this.gameObject.GetComponent<Collider2D>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            col.enabled = false;
            spriteRenderer.color = new Color(0, 0, 0, 0);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            col.enabled = false;
            spriteRenderer.color = new Color(0, 0, 0, 0);
        }
    }
}
