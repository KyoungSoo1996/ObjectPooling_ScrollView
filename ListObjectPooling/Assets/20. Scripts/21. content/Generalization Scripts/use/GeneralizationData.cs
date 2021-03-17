using System;

public class GeneralizationData
{
    public int number { get; set; }
    public UnityEngine.Color color { get; set; }
    public Action buttonEvent { get; set; }

    public GeneralizationData(int _number, UnityEngine.Color _color, Action _action)
    {
        this.number = _number;
        this.color = _color;
        this.buttonEvent = _action;
    }
}
