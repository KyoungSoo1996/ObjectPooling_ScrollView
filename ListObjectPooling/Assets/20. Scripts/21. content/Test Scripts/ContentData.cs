using System;
public class ColorButtonContent
{
    public int number { get; set; }
    public UnityEngine.Color color { get; set; }
    public Action buttonEvent { get; set; }

    public ColorButtonContent(int _number, UnityEngine.Color _color, Action _buttonEvent)
    {
        this.number = _number;
        this.color = _color;
        this.buttonEvent = _buttonEvent;
    }
}
