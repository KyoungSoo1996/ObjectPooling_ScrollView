using System;
public class ContentData
{
    public int number { get; set; }
    public UnityEngine.Color color { get; set; }
    public Action buttonEvent { get; set; }

    public ContentData(int _number, UnityEngine.Color _color, Action _buttonEvent)
    {
        this.number = _number;
        this.color = _color;
        this.buttonEvent = _buttonEvent;
    }
}
