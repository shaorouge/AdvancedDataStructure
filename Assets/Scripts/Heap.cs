using UnityEngine;
using System.Collections.Generic;
using System;

public class Heap : MonoBehaviour
{
    //valuables that are used to monipulate cubes
    public float manualWaitingDuration;
    public float manualSpeed;
    public float autoWaitingDuration;
    public float autoSpeed;
    private ParamComponent commonComponent;

    public float bottomLine;    //Blocks start floating here
    public GameObject prefab;
    public int total;

    private List<Controller> ctrlList;
    private int exchangeIdx;

    void Start()
    {
        ctrlList = new List<Controller>();
        ctrlList.Add(null);
        exchangeIdx = 0;

        commonComponent = new ParamComponent(manualWaitingDuration, manualSpeed);
    }

    public bool IsFull()
    {
        return ctrlList.Count >= total;
    }

    public Controller Pop()
    {
        if (ctrlList.Count == 1 || !CanModify())
            return null;

        exchangeIdx = 1;

        Controller poppedCtrl = ctrlList[exchangeIdx];
        //ctrlList[ctrlList.Count - 1].SetDestination(ctrlList[exchangeIdx].transform.position, StateEnum.WAITING);
        ctrlList[exchangeIdx] = ctrlList[ctrlList.Count - 1];
        
        if (ctrlList[exchangeIdx].IsInteractable)
            ctrlList[exchangeIdx].SetDestination(poppedCtrl.transform.position, StateEnum.STEP_MOVING);
        else
            ctrlList[exchangeIdx].SetDestination(poppedCtrl.transform.position, StateEnum.WAITING);

        ctrlList.RemoveAt(ctrlList.Count - 1);
        MoveDown();

        return poppedCtrl;
    }

    public void Push(Controller ctrl)
    {
        if (ctrlList.Count >= total || !CanModify())
            return;
        
        exchangeIdx = ctrlList.Count;
        ctrl.transform.position = new Vector3(calcuStartPosX(exchangeIdx), bottomLine, 0.0f);
        ctrl.component = commonComponent;

        if(ctrl.IsInteractable)
            ctrl.SetDestination(new Vector3(ctrl.transform.position.x, -calcuStartPosY(exchangeIdx), 0.0f), StateEnum.STEP_MOVING);
        else
            ctrl.SetDestination(new Vector3(ctrl.transform.position.x, -calcuStartPosY(exchangeIdx), 0.0f), StateEnum.WAITING);
        
        ctrlList.Add(ctrl);

        if (IsFirstNumber(ctrlList.Count - 1))
            MoveParent(ctrlList.Count - 1, 1, 4);

        MoveUp();
    }

    public bool CanModify()
    {
        for (int i = 1; i < ctrlList.Count; i++)
        {
            if (!ctrlList[i].IsPlaced())
                return false;
        }

        return true;
    }

    public void Clear()
    {
        ctrlList.Clear();
        ctrlList.Add(null);
    }

    public Controller[] ToArray()
    {
        return ctrlList.ToArray();
    }

    private void MoveDown()
    {
        int index = exchangeIdx * 2;
        
        //Index is larger than the length of the List
        if (ctrlList.Count == 1)
            return;

        while(index < ctrlList.Count)
        {
            if (index + 1 < ctrlList.Count && ctrlList[index].value > ctrlList[index + 1].value)
                index++;

            if (ctrlList[exchangeIdx].value > ctrlList[index].value)
            {
                ctrlList[exchangeIdx].AddDestination(ctrlList[index]);

                Exchange(exchangeIdx, index);
                exchangeIdx = index;
                index *= 2;
            }
            else
                break;
        }
    }

    private void MoveUp()
    {
        int index = exchangeIdx / 2;

        while(exchangeIdx > 1 && ctrlList[exchangeIdx].value < ctrlList[index].value)
        {
            ctrlList[exchangeIdx].AddDestination(ctrlList[index]);

            Exchange(exchangeIdx, index);
            exchangeIdx = index;
            index /= 2;
        }
    }

    private void Exchange(int index1, int index2)
    {
        Controller tempCtrl = ctrlList[index1];
        ctrlList[index1] = ctrlList[index2];
        ctrlList[index2] = tempCtrl;
    }

    private void MoveParent(int index, float gap, float distance)
    {
        int parentIndex = index / 2;

        if (parentIndex < 2)
            return;

        float posX = ctrlList[index].destination.x + gap;

        for (int i = parentIndex; i < index; i++, posX += distance)
        {
            ctrlList[i].SetDestination(new Vector3(posX, ctrlList[parentIndex].transform.position.y, 0.0f), StateEnum.WAITING);
        }

        MoveParent(parentIndex, gap * 2, distance * 2);
    }

    private bool IsFirstNumber(int index)
    {
        float judge = Mathf.Log(index, 2);

        return judge == Mathf.Floor(judge);
    }

    private float calcuStartPosX(int index)
    {
        float begin = Mathf.Pow(2, calcuStartPosY(index));
        return 1 + 2 * index - 3 * begin;
    }

    private float calcuStartPosY(int index)
    {
        return Mathf.Floor(Mathf.Log(index, 2));
    }
}