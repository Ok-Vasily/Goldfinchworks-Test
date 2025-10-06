using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneEvents : MonoBehaviour
{
    public static event System.Action OnMoveUp;
    public static event System.Action OnMoveDown;
    public static event System.Action OnMoveEast;
    public static event System.Action OnMoveWest;
    public static event System.Action OnMoveNorth;
    public static event System.Action OnMoveSouth;
    public static event System.Action OnStopMoving;

    public static void InvokeMoveUp() => OnMoveUp?.Invoke();
    public static void InvokeMoveDown() => OnMoveDown?.Invoke();
    public static void InvokeMoveEast() => OnMoveEast?.Invoke();
    public static void InvokeMoveWest() => OnMoveWest?.Invoke();
    public static void InvokeMoveNorth() => OnMoveNorth?.Invoke();
    public static void InvokeMoveSouth() => OnMoveSouth?.Invoke();
    public static void InvokeStopMoving() => OnStopMoving?.Invoke();
}
