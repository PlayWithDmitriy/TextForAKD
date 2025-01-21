using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    private bool Open = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Open)
        {
            anim.enabled = true;
            Open = true;
        }
    }
}
