using System;
using System.Collections.Generic;

public class StepMoveButton : BaseButtonAction
{
    public String stepMoveText;
    public String autoMoveText;

    private UILabel textLabel;

    void Awake()
    {
        Init();
    }

    /// <summary>
    /// Change text to auto move text
    /// </summary>
    public void OnAutoButtonClicked()
    {
        textLabel.text = autoMoveText;
    }

    public override void Init()
    {
        base.Init();

        textLabel = transform.Find("Label").GetComponent<UILabel>();
        textLabel.text = autoMoveText;
    }

    public override void OnButtonClicked()
    {
        if (textLabel.text == autoMoveText)
            textLabel.text = stepMoveText;
        else
            textLabel.text = autoMoveText;
    }
}
