using UnityEngine;

[CreateAssetMenu(fileName = "PiecePoolerChannelSO", menuName = "SpelunkyLevelMaker/PiecePoolerChannelSO", order = 0)]
public class PiecePoolerChannelSO : ScriptableObject
{
    public delegate GameObject RequestPoolObjectFromGates(GateType startingGate, GateType endingGate);
    public event RequestPoolObjectFromGates ERequestPoolObjectFromGates;

    public delegate GameObject GetRandomPoolObject();
    public event GetRandomPoolObject EGetRandomPoolObject;

    public delegate GameObject GetTerminalLevelPiece(GateType endingGate, bool isStarting);
    public event GetTerminalLevelPiece EGetTerminalLevelPiece;

    public delegate GameObject GetLevelPieceFromType(string type);
    public event GetLevelPieceFromType ERequestsLevelPieceFromType;

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

    public GameObject RaiseGetTerminalLevelPiece(GateType endingGate, bool isStarting)
    {
        if (EGetTerminalLevelPiece == null)
        {
            Debug.Log("Piece Pooler doesn't exist to handle this function call");
            return null;
        }
        else
        {
            return EGetTerminalLevelPiece(endingGate, isStarting);
        }
    }

    public GameObject RaiseRequestPooledObjectFromType(string type)
    {
        if (ERequestsLevelPieceFromType == null)
        {
            Debug.Log("Piece Pooler doesn't exist to handle this function call");
            return null;
        }
        else
        {
            return ERequestsLevelPieceFromType(type);
        }
    }
}