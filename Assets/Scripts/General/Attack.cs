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

    // 只要player和boar身上有一个collider2d是istrigger就行，这个OnTriggerStay2D不管在player，还是在boar身上，都会触发。
    // 但切记，不能2个collider2d都排除对方。
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Trigger Stay detected with: " + other.name);
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
