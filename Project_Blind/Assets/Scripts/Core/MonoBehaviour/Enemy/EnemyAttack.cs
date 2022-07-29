using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool attackable;

    public void setRange(Vector2 range)
    {
        gameObject.GetComponent<BoxCollider2D>().size = range;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
    }

    public bool Attackable()
    {
        return attackable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            attackable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            attackable = false;
    }
}