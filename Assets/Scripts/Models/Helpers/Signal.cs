using System;

public class SignalOnPlayerStarted : Signal
{
    private static SignalOnPlayerStarted _instance;

    public static SignalOnPlayerStarted Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SignalOnPlayerStarted();

            return _instance;
        }
    }
}

public delegate void Listener();

public abstract class Signal
{
    public event Action Listener = delegate { };

    public void AddListener(Action callback)
    {
        Listener += callback;
    }

    public void RemoveListener(Action callback)
    {
        Listener -= callback;
    }

    public void Dispatch()
    {
        Listener();
    }
}