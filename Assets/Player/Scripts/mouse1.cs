using UnityEngine;

public class mouse1 : MonoBehaviour
{
    [SerializeField] float mouseSensetivity, VertivalLimit;

    Transform playerBody;
    TouchField1 _field;
    float xRotation = 0f;


    void Start()
    {
        _field = GameObject.FindGameObjectWithTag("TouchField").GetComponent<TouchField1>();
        playerBody = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = _field.TouchDist.x * mouseSensetivity * Time.deltaTime;
        float mouseY = _field.TouchDist.y * mouseSensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -VertivalLimit, VertivalLimit);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
