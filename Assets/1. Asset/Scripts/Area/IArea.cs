using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArea
{
    void InteractArea(); // NPC 상호작용
    void ArrowActive(bool ison); // Arrow Setactive
    void MoveNextArea(Customer cust); // NPC 상호작용
    void ManagedByPlayer();
    void WaitForPlayer();

}
