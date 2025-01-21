using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;

    [SerializeField] GameObject Target;

    private float dist, startTimer, endTimer;
    private bool Timer, CanCheckClick = true;

    void Start()
    {
        endTimer = 40;
        Inventory.GoRay = false;
    }
    void Update()
    {
        CheckTimer();

        if (Pressed)
        {
            if(CanCheckClick)
            {
                Check();
            }
            TakeTouch();
        }
        else
        {
            Clicking();
        }
    }
    public void FixedUpdate()
    {
        if (Timer)
        {
            startTimer += 1/40;
        }
        else startTimer = 0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

    void TakeTouch()
    {
        Vector2 NowPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        dist = Vector2.Distance(Target.transform.position, NowPos);

        if (PointerId >= 0 && PointerId < Input.touches.Length)
        {
            TouchDist = Input.touches[PointerId].position - PointerOld;
            PointerOld = Input.touches[PointerId].position;
        }
        else
        {
            TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
            PointerOld = Input.mousePosition;
        }
    }
    void Check()
    {
        Target.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        CanCheckClick = false;
        Timer = true;
    }
    void CheckTimer()
    {
        if (startTimer > endTimer && Timer == true)
        {
            startTimer = 0f;
            Timer = false;
            Target.transform.position = new Vector2(2000, 2000);
        }
    }
    void Clicking()
    {
        if (!CanCheckClick && Timer && dist < 10f)
        {
            Apply();
            StopCheck();
        }
        else if (!CanCheckClick)
        {
            StopCheck();
        }
    }
    void StopCheck()
    {
        TouchDist = new Vector2();
        CanCheckClick = true;
        Timer = false;
        startTimer = 0f;
        dist = 0f;
        Target.transform.position = new Vector2(2000, 2000);
    }
    void Apply()
    {
        Debug.Log("SayToDoRay");
        Inventory.GoRay = true;
    }
}