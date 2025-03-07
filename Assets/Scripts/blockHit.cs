using System.Collections;
using UnityEngine;

public class blockHit : MonoBehaviour
{
    public Animator animator;
    public int numberHits = 1;
    private ContactPoint2D[] listContacts = new ContactPoint2D[1];
    public GameObject itemPrefab;
    public SpriteRenderer sr;
    public PlatformEffector2D platformEffector2D;
    public bool isHidden = false;

    private void Awake(){
        sr.enabled = !isHidden;
        platformEffector2D.enabled = isHidden;
        animator.ResetTrigger("isHit");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision){
        collision.GetContacts(listContacts);
        if (
            listContacts[0].normal.y > 0.5f &&
            collision.gameObject.CompareTag("Player") &&
            numberHits > 0
        ){
            StartCoroutine(Hit());
        }
    }

    private IEnumerator Hit(){
        yield return null;
        sr.enabled = true;
        platformEffector2D.enabled= false;
        animator.SetTrigger("isHit");
        numberHits--;
        if (itemPrefab != null){
            GameObject item = Instantiate(
                itemPrefab,
                transform.position,
                Quaternion.identity
            );
            item.GetComponent<Collectible>().canBeDestroyedOnContact  = false;
            Vector3 endPosition = (item.transform.localPosition + Vector3.up*1.5f);
            yield return item.transform.MoveBackAndForth(endPosition);
            item.GetComponent<Collectible>().Picked();
        }
    }
}
