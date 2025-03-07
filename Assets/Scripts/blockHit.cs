using UnityEngine;

public class blockHit : MonoBehaviour
{
    public Animator animator;
    public int numberHits = 1;
    private ContactPoint2D[] listContacts = new ContactPoint2D[1];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision){
        collision.GetContacts(listContacts);
        if (collision.gameObject.CompareTag("Player") &&
        numberHits > 0
        ){
            Hit();
        }
    }

    private void Hit(){
        animator.SetTrigger("isHit");
        numberHits--;
    }
}
