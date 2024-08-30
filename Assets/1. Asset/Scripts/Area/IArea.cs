using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArea
{
    void InteractArea(); // NPC ��ȣ�ۿ�
    void ArrowActive(bool ison); // Arrow Setactive
    void MoveNextArea(); // NPC ��ȣ�ۿ�
    void ManagedByPlayer();
    void WaitForPlayer();
}
