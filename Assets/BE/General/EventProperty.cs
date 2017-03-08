public delegate void DelVoid();

public class EventProperty<T>
{
    public event DelVoid OnChange;

    public T val
    {
        get
        {
            return _val;
        }
        set
        {
            _val = value;
            if(OnChange != null)
                OnChange.Invoke();
        }
    }

    T _val;

    public EventProperty(T val)
    {
        _val = val;
    }
}