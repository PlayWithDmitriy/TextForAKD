using UnityEngine;

public class ObjectLerp : MonoBehaviour
{
    [HideInInspector]
    public Transform LerpTo;

    [HideInInspector]
    public bool CanTake = true, Drop, Take;

    [SerializeField] float LerpSpeed;
    private Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(Take && CanTake)
        {
            TakeObject();
        }
        else if(Drop)
        {
            DropObject();
        }
    }

    private bool One;
    void TakeObject()
    {
        if(!One)
        {
            One = true;
            this.transform.parent = player;
            this.GetComponent<BoxCollider>().enabled = false;
        }
        float dist1 = Vector3.Distance(transform.position, LerpTo.position);
        this.transform.position = Vector3.Lerp(this.transform.position, LerpTo.transform.position, LerpSpeed * 40 * Time.deltaTime);
        if(dist1 < 0.05f)
        {
            Take = false;
            One = false;
            CanTake = false;
            this.transform.position = LerpTo.transform.position;
        }
    }
    void DropObject()
    {
        if (!One)
        {
            One = true;
            this.transform.parent = null;
            CanTake = false;
        }
        float dist1 = Vector3.Distance(transform.position, LerpTo.position);
        this.transform.position = Vector3.Lerp(this.transform.position, LerpTo.transform.position, LerpSpeed * 40 * Time.deltaTime);
        if (dist1 < 0.5f)
        {
            Drop = false;
            One = false;
            //this.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
