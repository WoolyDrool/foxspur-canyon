using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class TimeManager : MonoBehaviour
{
    [Header("Time")] 
    [Range(0.0f, 24f)] public float currentTime;
    public float dayLength;
    public float startTime;
    public float currentMinute;
    public float currentHour;

    [Header("Days")] 
    public int currentDay;
    public int dayOfTheWeek = 1;
    public int dayOfTheMonth = 1;
    public int currentWeek = 1;
    public int currentMonth = 1;

    public DaysOfTheWeek day;
    public enum DaysOfTheWeek {Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday}

    public TimeOfDay time;
    public enum TimeOfDay { Midnight, Night, Twilight, Morning, Noon, Afternoon, Evening}

    public Months month;
    public enum Months {January, February, March, April, May, June, July, August, September, October, November, December}

    #region Events

    [HideInInspector] public UnityEvent midnightEvent = new UnityEvent();
    [HideInInspector] public UnityEvent nightEvent = new UnityEvent();
    [HideInInspector] public UnityEvent twilightEvent = new UnityEvent();
    [HideInInspector] public UnityEvent morningEvent = new UnityEvent();
    [HideInInspector] public UnityEvent noonEvent = new UnityEvent();
    [HideInInspector] public UnityEvent afternoonEvent = new UnityEvent();
    [HideInInspector] public UnityEvent eveningEvent = new UnityEvent();
    
    #endregion
    
    #region Internal Variables
    
    //Variables
    private float _timeRate;
    private float _minutesInHour;
    private float _currentHourMinute;
    private int _currentTimeAsInt;

    [SerializeField] private bool _midnight = false;
    [SerializeField] private bool _twilight = false;
    [SerializeField] private bool _morning = false;
    [SerializeField] private bool _noon = false;
    [SerializeField] private bool _afternoon = false;
    [SerializeField] private bool _evening = false;
    [SerializeField] private bool _night = false;

    #endregion

    private void Awake()
    {
        _timeRate = 0.25f / dayLength;
        currentTime = startTime;
    }

    private void Start()
    {
        _currentTimeAsInt = (int)currentTime;
        UpdateTOD();
    }

    void Update()
    {
        //Increment time
        currentTime += _timeRate * Time.deltaTime;
        _currentTimeAsInt = (int)currentTime;
        
        //Define time scale
        _minutesInHour = currentTime % 1;
        _currentHourMinute = (_minutesInHour % 120);
        
        //Set the current minute as an int
        currentMinute = (int)(_currentHourMinute * 60);
        currentHour = (int)currentTime;
        
        UpdateTOD();
        
        //Reset the timer at midnight
        if (currentTime >= dayLength)
        {
            currentTime = 0.0f;
            currentDay++;
            dayOfTheWeek++;
            dayOfTheMonth++;
            UpdateDay();
        }
            
        //Increment current week
        if (dayOfTheWeek > 7)
        {
            dayOfTheWeek = 1;
            UpdateDay();
            currentWeek++;
        }
        
        //Increment current month
        if (dayOfTheMonth > 30)
        {
            UpdateMonth();
        }
    }

    void UpdateTOD()
    {
        //Increment time of day events
        switch (_currentTimeAsInt)
        {
            case 0:
            {
                time = TimeOfDay.Midnight;
                if (!_midnight || _currentTimeAsInt >= 0)
                {
                    _midnight = true;
                    midnightEvent?.Invoke();
                }
                
                break;
            }

            case 4:
            {
                time = TimeOfDay.Twilight;
                if (!_twilight || _currentTimeAsInt > 4)
                {
                    _twilight = true;
                    twilightEvent?.Invoke();
                }
                break;
            }

            case 7:
            {
                time = TimeOfDay.Morning;
                if (!_morning || _currentTimeAsInt > 7)
                {
                    _morning = true;
                    morningEvent?.Invoke();
                }
                break;
            }

            case 12:
            {
                time = TimeOfDay.Noon;
                if (!_noon || _currentTimeAsInt > 12)
                {
                    _noon = true;
                    noonEvent?.Invoke();
                }
                break;
            }

            case 13:
            {
                time = TimeOfDay.Afternoon;

                if (!_afternoon || _currentTimeAsInt > 13)
                {
                    _afternoon = true;
                    afternoonEvent?.Invoke();
                }
                break;
            }

            case 17:
            {
                time = TimeOfDay.Evening;
                if (!_evening || _currentTimeAsInt > 17)
                {
                    _evening = true;
                    eveningEvent?.Invoke();
                }
                break;
            }

            case 20:
            {
                time = TimeOfDay.Night;
                if (!_night || _currentTimeAsInt > 20)
                {
                    _night = true;
                    nightEvent?.Invoke();
                }
                break;
            }
        }
    }

    void UpdateDay()
    {
        _midnight = false;
        _twilight = false;
        _morning = false;
        _noon = false;
        _afternoon = false;
        _evening = false;
        _night = false;
        switch (dayOfTheWeek)
        {
            case 1:
            {
                day = DaysOfTheWeek.Monday;
                break;
            }
            case 2:
            {
                day = DaysOfTheWeek.Tuesday;
                break;
            }
            case 3:
            {
                day = DaysOfTheWeek.Wednesday;
                break;
            }
            case 4:
            {
                day = DaysOfTheWeek.Thursday;
                break;
            }
            case 5:
            {
                day = DaysOfTheWeek.Friday;
                break;
            }
            case 6:
            {
                day = DaysOfTheWeek.Saturday;
                break;
            }
            case 7:
            {
                day = DaysOfTheWeek.Sunday;
                break;
            }
        }
    }

    void UpdateWeek()
    {}

    void UpdateMonth()
    {
        currentWeek = 1;
        currentMonth++;
        dayOfTheWeek = 1;
        dayOfTheMonth = 1;
        UpdateDay();
        
        switch (currentMonth)
        {
            case 1:
            {
                month = Months.January;
                break;
            }
            case 2:
            {
                month = Months.February;
                break;
            }
            case 3:
            {
                month = Months.March;
                break;
            }
            case 4:
            {
                month = Months.April;
                break;
            }
            case 5:
            {
                month = Months.May;
                break;
            }
            case 6:
            {
                month = Months.June;
                break;
            }
            case 7:
            {
                month = Months.July;
                break;
            }
            case 8:
            {
                month = Months.August;
                break;
            }
            case 9:
            {
                month = Months.September;
                break;
            }
            case 10:
            {
                month = Months.October;
                break;
            }
            case 11:
            {
                month = Months.November;
                break;
            }
            case 12:
            {
                month = Months.December;
                break;
            }
            case 13:
            {
                currentMonth = 1;
                UpdateMonth();
                break;
            }
        }
    }
    

}
