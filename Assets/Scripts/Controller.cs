using UnityEngine;
using System.Collections.Generic;
using System;

public class Controller : StateMachine
{
    public float defaultWaitingDuration;
    public Vector3 destination;
    public int value;
    public ParamComponent component;
    public Vector3 direction;
    public bool IsInteractable { get; set; }

    private Queue<Controller> destinationQueue;

	// Use this for initialization
	void Awake () 
    {
        stateMap = new Dictionary<StateEnum, IState>();

        stateMap.Add(StateEnum.IDLE, new IdleState());
        stateMap.Add(StateEnum.WAITING, new WaitingState(this));
        stateMap.Add(StateEnum.MOVING, new MovingState(this));
        stateMap.Add(StateEnum.STEP_MOVING, new StepMovingState(this));

        currentState = stateMap[StateEnum.IDLE];

        destinationQueue = new Queue<Controller>();
	}

	// Update is called once per frame
	void Update () 
    {
        Execute();
	}

    void OnMouseDown()
    {
        if (IsInteractable)
            EventBus.GetInstance().Trigger(new GameObjectEvent(AppEventType.IS_MOVABLE, gameObject));
    }

    public void Move()
    {
        if ((destination - transform.position).magnitude <= component.speed)
        {
            transform.position = destination;
            direction = Vector3.zero;

            if(destinationQueue.Count > 0)
            {
                Controller ctrl = destinationQueue.Dequeue();

                SetDestination(ctrl.transform.position, StateEnum.WAITING);
                ctrl.SetDestination(transform.position, StateEnum.WAITING);
            }
            else
                SwitchState(StateEnum.IDLE);
        }
        else
            transform.position += direction * component.speed;
    }

    public void StepMove()
    {
        if ((destination - transform.position).magnitude <= component.speed)
        {
            transform.position = destination;
            direction = Vector3.zero;

            SwitchState(StateEnum.IDLE);
        }
        else
            transform.position += direction * component.speed;
    }

    public void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    public Controller PeekDestinationQueue()
    {
        if(destinationQueue.Count > 0)
            return destinationQueue.Peek();

        return null;
    }

    public Controller PopDestinationQueue()
    {
        if(destinationQueue.Count > 0)
            return destinationQueue.Dequeue();

        return null;
    }

    public void AddDestination(Controller ctrl)
    {
        destinationQueue.Enqueue(ctrl);
    }

    public bool IsPlaced()
    {
        return transform.position == destination && 
               currentState.GetType() == typeof(IdleState) &&
               destinationQueue.Count == 0;
    }

    public void SetDestination(Vector3 des, StateEnum state)
    {
        destination = des;
        direction = (destination - transform.position).normalized;
        SwitchState(state);
    }

    public void SetNumber(int num)
    {
        value = num;
        transform.Find("Number").GetComponent<TextMesh>().text = num.ToString();
    }
}
