using System;
using System.Collections.Generic;
using UnityEngine;

public class SortedObjectsContainer : MonoBehaviour
{
    public float horizontalGap;
    public float verticalGap;
    public Vector3 initPosition;
    public int numberPerRow;

    private List<Controller> list;

    void Awake()
    {
        list = new List<Controller>();
    }

    public void Add(Controller ctrl)
    {
        if(ctrl == null)
            return;

        ctrl.IsInteractable = false;
        list.Add(ctrl);
        Vector3 des = new Vector3(initPosition.x + ((list.Count - 1) % numberPerRow) * horizontalGap,
                                  initPosition.y - ((list.Count - 1) / numberPerRow) * verticalGap,
                                  initPosition.z);

        ctrl.SetDestination(des, StateEnum.WAITING);
    }

    public Controller[] ToArray()
    {
        return list.ToArray();
    }

    public void Clear()
    {
        list.Clear();
    }
}
