using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public static bool GoRay;
    [HideInInspector]
    public Vector3 touchPosition;

    [SerializeField]
    float TakeLenth;

    [SerializeField]
    Transform[] inventoryTrackers;

    [SerializeField]
    bool[] inventory;

    [SerializeField]
    GameObject[] inventoryObjects;

    [SerializeField]
    private TextMeshProUGUI tex;
    private int inv = 0;

    private void Start()
    {
        tex.text = inv.ToString() + "/" + inventory.Length.ToString();
    }
    void Update()
    {
        if(GoRay)
        {
            RayCheck();
        }
    }

    GameObject Obj;
    void RayCheck()
    {
        Debug.Log("GoRay");
        touchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, TakeLenth))
        {
            if(hit.collider.CompareTag("TakeObject"))
            {
                Obj = hit.collider.gameObject;
                
                Debug.Log("takeObject");
                TakeObject();
            }

            if (hit.collider.CompareTag("DropObject"))
            {
                Obj = hit.collider.gameObject;
                
                Debug.Log("dropObject");
                DropObject();
            }
        }
        GoRay = false;
    }
    private void TakeObject()
    {
        for(int i = 0; i < inventoryTrackers.Length; i++)
        {
            if (!inventory[i])
            {
                Debug.Log("inInventory");
                inventory[i] = true;
                inventoryObjects[i] = Obj.gameObject;
                Obj.GetComponent<ObjectLerp>().LerpTo = inventoryTrackers[i];
                Obj.GetComponent<ObjectLerp>().Take = true;
                Obj = null;

                inv += 1;
                tex.text = inv.ToString() + "/" + inventory.Length.ToString();

                break;
            }
        }
    }
    private void DropObject()
    {
        for (int i = 0; i < inventoryTrackers.Length; i++)
        {
            Debug.Log(inventory[i]);
            if (inventory[i])
            {
                Debug.Log("outInventory");
                inventory[i] = false;
                Obj.GetComponent<DropObjects>().DropObject = inventoryObjects[i];
                Obj.GetComponent<DropObjects>().Drop = true;
                Obj = null;

                inv -= 1;
                tex.text = inv.ToString() + "/" + inventory.Length.ToString();

                break;
            }
        }
    }
}
