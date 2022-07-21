using UnityEngine;

public class Cross : MonoBehaviour
{
    public enum Turn
    {
        TurnLeft,
        TurnRight
    };

    public Turn turn;

    private Vector3 turnDirection;

    public Vector3 GetTurnDirection()
    {
        return turn switch
        {
            Turn.TurnLeft => new Vector3(0, -90, 0),
            Turn.TurnRight => new Vector3(0, 90, 0),
            _=>Vector3.zero,
        };
    }
}
