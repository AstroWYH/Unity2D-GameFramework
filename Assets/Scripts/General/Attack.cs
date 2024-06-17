using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    public float attackRate;

    private void Start()
    {
        Debug.Log("Start");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Trigger Stay detected with: " + other.name);
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}