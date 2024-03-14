using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeForce : MonoBehaviour
{
   [SerializeField] private float strikeForce=20f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        ApplyForce();
    }
    private void ApplyForce()
    {
        Vector2 direcrtion = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddForce(direcrtion * strikeForce, ForceMode2D.Impulse);
    }
}
