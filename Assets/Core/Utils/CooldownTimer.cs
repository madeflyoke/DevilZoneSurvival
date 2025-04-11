public class CooldownTimer
{
    private float _cooldownDuration;
    private float _remainingTime;
    private bool _isRunning;
    private bool _isFinished;

    public float CooldownDuration => _cooldownDuration;
    public float RemainingTime => _remainingTime;
    public bool IsRunning => _isRunning;
    public bool IsFinished => _isFinished;

    public CooldownTimer(float duration)
    {
        _cooldownDuration = duration;
        _remainingTime = 0f;
        _isRunning = false;
    }

    public void Start()
    {
        _remainingTime = _cooldownDuration;
        _isRunning = true;
    }

    public void Stop()
    {
        _isRunning = false;
    }

    public void Restart()
    {
        _isFinished = false;
        
        Start();
    }

    public void Tick(float deltaTime)
    {
        if (_isFinished || !_isRunning || !(_remainingTime > 0f))
        {
            return;
        }

        _remainingTime -= deltaTime;

        if (!(_remainingTime <= 0f))
        {
            return;
        }

        _remainingTime = 0f;
        _isRunning = false;
        _isFinished = true;
    }
}