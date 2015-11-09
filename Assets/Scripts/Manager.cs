using UnityEngine;
using System.Collections.Generic;

public class Manager : StateMachine 
{
    public NumberText numberText;
    public Heap heap;
    public SortedObjectsContainer sortedObjectsContainer;

    [SerializeField]
    private GameObject[] objectPool;
    [SerializeField]
    private GameObject prefab;

    /// <summary>
    /// Mark an object that will be moved when its parent of child is clicked
    /// </summary>
    public Controller movableCtrl = null;

    void Awake()
    {
        EventBus.GetInstance().Subscribe(AppEventType.IS_MOVABLE, CheckObjectIsMovable);

        stateMap = new Dictionary<StateEnum, IState>();
        stateMap.Add(StateEnum.MANUAL, new ManualState(this));
        stateMap.Add(StateEnum.AUTO_PUSHING, new AutoPushingState(this));

        currentState = stateMap[StateEnum.MANUAL];
    }

    void Start()
    {
        for(int i = 0; i < objectPool.Length; i++)
        {
            objectPool[i] = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            objectPool[i].SetActive(false);
        }
    }

    void Update()
    {
        Execute();
    }

    /// <summary>
    /// When an object is clicked, manager need to check if this object can be moved
    /// </summary>
    /// <param name="e"></param>
    public void CheckObjectIsMovable(IEvent e)
    {
        Controller ctrl = ((GameObjectEvent)e).gameObject.GetComponent<Controller>();

        if(movableCtrl == null)
        {
            movableCtrl = ctrl;
            movableCtrl.SetColor(Color.red);
            return;
        }
        
        if(ctrl.PeekDestinationQueue() != null && ctrl.PeekDestinationQueue() == movableCtrl)
            ExchangeCtrls(ctrl, movableCtrl);

        if (movableCtrl.PeekDestinationQueue() != null && ctrl == movableCtrl.PeekDestinationQueue())
            ExchangeCtrls(movableCtrl, ctrl);

        Resume();
    }

    private void ExchangeCtrls(Controller main, Controller side)
    {
        side.AddDestination(main);
        side.SetDestination(side.PopDestinationQueue().transform.position, StateEnum.STEP_MOVING);
        main.SetDestination(main.PopDestinationQueue().transform.position, StateEnum.STEP_MOVING);
    }

    private void Resume()
    {
        if (movableCtrl != null)
        {
            movableCtrl.SetColor(Color.white);
            movableCtrl = null;
        }
    }

    public void Push()
    {
        Resume();
        PushNumber(numberText.CurNumber);
    }

    public void PushNumber(int number)
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.GetComponent<Controller>().SetNumber(number);
                heap.Push(obj.GetComponent<Controller>());
                EventBus.GetInstance().Trigger(new ModifyButtonEvent(AppEventType.DISABLE_BUTTON));
                return;
            }
        }
    }

    public void Pop()
    {
        Resume();
        sortedObjectsContainer.Add(heap.Pop());
        EventBus.GetInstance().Trigger(new ModifyButtonEvent(AppEventType.DISABLE_BUTTON));
    }

    public void Clear()
    {
        heap.Clear();
        sortedObjectsContainer.Clear();

        foreach (GameObject obj in objectPool)
        {
            obj.GetComponent<Controller>().IsInteractable = false;
            obj.SetActive(false);
        }
    }

    public void SwitchToAutoPush()
    {
        EventBus.GetInstance().Trigger(new ModifyButtonEvent(AppEventType.DISABLE_BUTTON));
        SwitchState(StateEnum.AUTO_PUSHING);

        foreach (GameObject obj in objectPool)
        {
            obj.GetComponent<Controller>().IsInteractable = false;
        }
    }
    
    /// <summary>
    /// Make objects interactable
    /// </summary>
    public void StepMoveMode()
    {
        foreach(GameObject obj in objectPool)
        {
            obj.GetComponent<Controller>().IsInteractable = !obj.GetComponent<Controller>().IsInteractable;
        }

        Controller[] ctrlArray = sortedObjectsContainer.ToArray();

        foreach(Controller ctrl in ctrlArray)
        {
            ctrl.IsInteractable = false;
        }
    }
}
