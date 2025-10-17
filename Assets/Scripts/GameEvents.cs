using System;
using UnityEngine;


public static class GameEvents
{
    public static Action<int> OnSpawnAnimals;
    public static Action OnLevelTimeRanOut;
    public static Action OnDisableInput;
    public static Action<int> OnReportAliveAnimalsCount;
}