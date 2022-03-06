using UnityEngine;


[System.Serializable]
public enum EnemyState { Idle, Guard, Patroll, Suspicious, Alert, Chasing, NPC_EVENT }
public interface IEnemy 
{


    public void Idle();

    public void Guard();

    public  void Patroll();

    public void Suspicious();

    public void Alert();

    public void Chasing();

    public void NPC_Event();



    public void OnPlayerEnterVision(GameObject g);
    public void OnPlayerStayInVision(GameObject g);
    public void OnPlayerExitVision(GameObject g);

    public void OnPlayerEnterPeripheralVision(Vector3 suspos);
    public void OnPlayerStayInPeripheralVision(Vector3 suspos);
    public void OnPlayerExitPeripheralVision(Vector3 suspos);

    public void SuspiciousPoint(Vector3 point);
    public void AlertPoint(Vector3 AlerPoint);
}
