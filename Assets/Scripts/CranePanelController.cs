using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CranePanelController : MonoBehaviour
{
    public void MoveUp() => CraneEvents.InvokeMoveUp();
    public void MoveDown() => CraneEvents.InvokeMoveDown();
    public void MoveEast() => CraneEvents.InvokeMoveEast();
    public void MoveWest() => CraneEvents.InvokeMoveWest();
    public void MoveNorth() => CraneEvents.InvokeMoveNorth();
    public void MoveSouth() => CraneEvents.InvokeMoveSouth();
    public void StopMoving() => CraneEvents.InvokeStopMoving();
}
