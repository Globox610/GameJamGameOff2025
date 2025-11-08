
using JetBrains.Annotations;

/// <summary>
/// CooldownTimer class tracks the time until it reaches 0. 
/// 
/// It does not automatically countdown the time by itself and needs to be updated through Update().
/// Once it has hit the countdown of 0 it emits an event which can be used with object.TimerCompleteEvent += method.
/// 
/// Basic usage: 
/// 
/// private CooldownTimer timer;
/// 
/// (initialize the cd)
/// timer = new CooldownTimer(duration_of_the_cd, true);
/// timer.Start();
/// timer.TimerCompleteEvent += method;
/// 
/// (in update)
/// timer.Update(Time.deltaTime);
/// </summary>
public class CooldownTimer
{
    public float TimeRemaining { get; private set; }
    public float TotalTime { get; private set; }
    public bool IsActive { get; private set; }
    public int TimesCounted { get; private set; }

    public float TimeElapsed => TotalTime - TimeRemaining;
    public float PercentElapsed => TimeElapsed / TotalTime;
    public float PercentRemaining => TimeRemaining / TotalTime;
    public bool IsCompleted => TimeRemaining <= 0;

    public delegate void TimerCompleteHandler();

    /// <summary>
    /// Emits event when timer is completed
    /// </summary>
    public event TimerCompleteHandler TimerCompleteEvent;

    /// <summary>
    /// Create a new CooldownTimer
    /// Must call Start() to begin timer
    /// </summary>
    /// <param name="time">Timer length (seconds)</param>
    /// <param name="recurring">Is this timer recurring</param>
    public CooldownTimer(float time)
    {
        TotalTime = time;
        TimeRemaining = TotalTime;
    }

    /// <summary>
    /// Start timer with existing time
    /// </summary>
    public void Start()
    {
        if (IsActive)
        {
            TimesCounted++;
        }

        TimeRemaining = TotalTime;
        IsActive = true;

        if (TimeRemaining <= 0)
        {
            TimerCompleteEvent?.Invoke();
        }
    }

    /// <summary>
    /// Start timer with new time
    /// </summary>
    public void Start(float time)
    {
        TotalTime = time;
        Start();
    }

    public virtual void Update(float timeDelta)
    {
        if (TimeRemaining > 0 && IsActive)
        {
            TimeRemaining -= timeDelta;
            if (TimeRemaining <= 0)
            {
                TimeRemaining = TotalTime;
                Pause();

                TimerCompleteEvent?.Invoke();
                TimesCounted++;
            }
        }
    }

    public void Invoke()
    {
        TimerCompleteEvent?.Invoke();
    }

    public void Pause()
    {
        IsActive = false;
    }

    /// <summary>
    /// Add additional time to timer
    /// </summary>
    public void AddTime(float time)
    {
        TimeRemaining += time;
        TotalTime += time;
    }

    /// <summary>
    /// Set Current remaining time with a set time
    /// </summary>
    public void SetRemainingTime(float time)
    {
        TimeRemaining = time;
    }

    /// <summary>
    /// Set TotalTime with with a set time 
    /// </summary>
    public void SetTotalTime(float time)
    {
        TotalTime = time;
    }

    public void SetTime(float totalTime, float remainingTime)
    {
        TotalTime = totalTime;
        TimeRemaining = remainingTime;
    }

    public void SetNewTimer(float time)
    {
        SetRemainingTime(time);
        Start();
    }
}