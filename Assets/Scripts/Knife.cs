using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    #region Dependencies
    [SerializeField] private Vector2 throwForce;
    private bool isActive = true;
    private Rigidbody2D rb;
    private BoxCollider2D knifeCollider;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knifeCollider = rb.GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (GetClikcedObject()=="Knife" && isActive)
        {
            SoundManager.Instance.PlaySFX("KnifeClicked");
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            GameController.Instance.uiManager.DecreamentDisplayKnifeCount();
        }
        GetClikcedObject();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
            return;
        isActive = false;
        if (collision.collider.CompareTag("Wheel"))
        {
            GetComponent<ParticleSystem>().Play();
            SoundManager.Instance.PlaySFX("KnifeHit");
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform);
            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.3f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1f);

            GameController.Instance.OnSuccessfulKnifeHit();
        }
        else if (collision.collider.CompareTag("Knife"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -2);
            GameController.Instance.StartGameOverSequence(false);
        }
    }
    #endregion

    #region Functionality Mehtods
    private string GetClikcedObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if(hit.collider != null)
            {
                return hit.collider.gameObject.tag;
            }
        }
        return null;
    }
    #endregion
}
