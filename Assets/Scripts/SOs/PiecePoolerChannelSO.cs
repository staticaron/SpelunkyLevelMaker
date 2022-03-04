using UnityEngine;

[CreateAssetMenu(fileName = "PiecePoolerChannelSO", menuName = "SpelunkyLevelMaker/PiecePoolerChannelSO", order = 0)]
public class PiecePoolerChannelSO : ScriptableObject
{
    public delegate GameObject RequestPoolObjectFromGates(GateType startingGate, GateType endingGate);
    public event RequestPoolObjectFromGates ERequestPoolObjectFromGates;

    public delegate GameObject GetRandomPoolObject();
    public event GetRandomPoolObject EGetRandomPoolObject;

    public GameObject RaiseRequestPoolObjectFromGates(GateType startingGate, GateType endingGate)
    {
        if (ERequestPoolObjectFromGates == null)
        {
            Debug.Log("No Piece Pooler exist to handle this function call");
            return null;
        }
        else
        {
            return ERequestPoolObjectFromGates(startingGate, endingGate);
        }
    }

    public GameObject RaiseGetRandomPoolObject()
    {
        if (EGetRandomPoolObject == null)
        {
            Debug.Log("No Piece Pooler exist to handle this function call");
            return null;
        }
        else
        {
            return EGetRandomPoolObject();
        }
    }
}