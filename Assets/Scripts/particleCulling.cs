using UnityEngine;

public class particleCulling : MonoBehaviour
{
    public ParticleSystem ps;
    private void Aweke(){
        ps.Stop();
    }

    private void OnBecameVisible(){
        ps.Play();
    }
    private void OnBecameInvisible(){
        ps.Stop();
    }
}
