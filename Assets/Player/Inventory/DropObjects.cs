using UnityEngine;

public class DropObjects : MonoBehaviour
{
    [HideInInspector] 
    public GameObject DropObject;
    [HideInInspector] 
    public bool Drop;

    [SerializeField] 
    Transform[] DropInventory;
    [SerializeField]
    bool[] inventory;

    void Update()
    {
        if(Drop)
        {
            Drop = false;
            DoDrop();
        }
    }
    private void DoDrop()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (!inventory[i])
            {
                inventory[i] = true;
                DropObject.GetComponent<ObjectLerp>().LerpTo = DropInventory[i];
                DropObject.GetComponent<ObjectLerp>().Drop = true;
                DropObject = null;
                break;
            }
        }
    }
}
