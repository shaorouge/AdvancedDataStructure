using UnityEngine;
using System.Collections;

public class ButtonEvent : MonoBehaviour
{
    public NumberText numberText;
    public int number;

    public void SentNumber()
    {
        EventBus.GetInstance().Trigger(new SendNumberEvent(number));
    }
}
