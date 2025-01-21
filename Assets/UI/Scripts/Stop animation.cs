using UnityEngine;

public class Stopanimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void DisableAnimation()
    {
        anim.enabled = false;
    }
}
