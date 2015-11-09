using UnityEngine;
using System.Collections;

public class NumberText : MonoBehaviour, IEventListener
{
    public int CurNumber { get { return curNumber; } }

    private UILabel label;
    private int curNumber;
    //private Heap heap;

    void Awake()
    {
        EventBus.GetInstance().Subscribe(AppEventType.SEND_NUMBER, Execute);
    }

	// Use this for initialization
	void Start () 
    {
        label = GetComponent<UILabel>();
        curNumber = 0;

        //heap = GetComponent<Heap>();
	}

    public void Execute(IEvent e)
    {
        if (e.GetType() != typeof(SendNumberEvent))
        {
            Debug.LogError("It's not SendNumberEvent");
            return;
        }

        int number = ((SendNumberEvent)e).number;

        if (curNumber < 10)
            curNumber = curNumber * 10 + number;
        else
            curNumber = number;

        label.text = curNumber.ToString();
    }

    public void SendNumber()
    {
        //heap.Push(curNumber);
    }
}
