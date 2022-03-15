using UnityEngine;
using System.Collections.Generic;

public class LevelPiecePooler : MonoBehaviour
{
    [Header("Level Pieces for Main path")]
    [SerializeField] GameObject[] LevelPieces_LR;
    [SerializeField] GameObject[] LevelPieces_LD;
    [SerializeField] GameObject[] LevelPieces_RD;
    [SerializeField] GameObject[] LevelPieces_UL;
    [SerializeField] GameObject[] LevelPieces_UR;
    [SerializeField] GameObject[] LevelPieces_UD;

    [Header("Level Pieces for Starting Tile")]
    [SerializeField] GameObject[] StartingLevelPieces_Down;
    [SerializeField] GameObject[] StartingLevelPieces_Left;
    [SerializeField] GameObject[] StartingLevelPieces_Right;

    [Header("Level Pieces for Ending Tile")]
    [SerializeField] GameObject[] EndingLevelPieces_Up;
    [SerializeField] GameObject[] EndingLevelPieces_Left;
    [SerializeField] GameObject[] EndingLevelPieces_Right;

    [Header("Misc. Level Piece GOs")]
    public GameObject LevelPieceTemplate;
    public GameObject Hurdle;

    [SerializeField] int poolSize = 3;

    [Header("Pool for path level pieces")]
    [SerializeField] List<GameObject> LR_Pool;
    [SerializeField] List<GameObject> LD_Pool;
    [SerializeField] List<GameObject> RD_Pool;
    [SerializeField] List<GameObject> UL_Pool;
    [SerializeField] List<GameObject> UR_Pool;
    [SerializeField] List<GameObject> UD_Pool;
    [SerializeField] List<GameObject> Base_Pool;
    [SerializeField] List<GameObject> Hurdles_Pool;

    //SOs
    [SerializeField] PiecePoolerChannelSO piecePoolerChannelSO;

    private void Awake()
    {
        LR_Pool = new List<GameObject>();
        LD_Pool = new List<GameObject>();
        RD_Pool = new List<GameObject>();
        UL_Pool = new List<GameObject>();
        UR_Pool = new List<GameObject>();
        UD_Pool = new List<GameObject>();
        Base_Pool = new List<GameObject>();
        Hurdles_Pool = new List<GameObject>();

        GeneratePool();
    }

    private void OnEnable()
    {
        piecePoolerChannelSO.ERequestPoolObjectFromGates += RequestObjectFromPool;
        piecePoolerChannelSO.ERequestsLevelPieceFromType += GetGameObjectFromType;
        piecePoolerChannelSO.EGetRandomPoolObject += GetRandomObjectFromPool;
        piecePoolerChannelSO.EGetTerminalLevelPiece += GetTerminalLevelPiece;
    }

    private void OnDisable()
    {
        piecePoolerChannelSO.ERequestPoolObjectFromGates -= RequestObjectFromPool;
        piecePoolerChannelSO.ERequestsLevelPieceFromType -= GetGameObjectFromType;
        piecePoolerChannelSO.EGetRandomPoolObject -= GetRandomObjectFromPool;
        piecePoolerChannelSO.EGetTerminalLevelPiece -= GetTerminalLevelPiece;
    }

    [ContextMenu("Generate Pool")]
    private void GeneratePool()
    {
        GeneratePool(new GameObject[] { LevelPieceTemplate }, ref Base_Pool, poolSize);
        GeneratePool(new GameObject[] { Hurdle }, ref Hurdles_Pool, poolSize);

        GeneratePool(LevelPieces_LR, ref LR_Pool, poolSize);
        GeneratePool(LevelPieces_LD, ref LD_Pool, poolSize);
        GeneratePool(LevelPieces_RD, ref RD_Pool, poolSize);
        GeneratePool(LevelPieces_UD, ref UD_Pool, poolSize);
        GeneratePool(LevelPieces_UL, ref UL_Pool, poolSize);
        GeneratePool(LevelPieces_UR, ref UR_Pool, poolSize);

    }

    [ContextMenu("Check Names")]
    private void CheckNames()
    {
        foreach (GameObject g in LR_Pool)
        {
            Debug.Log(g.name);
        }

        foreach (GameObject h in UL_Pool)
        {
            Debug.Log(h.name);
        }
    }

    // Generate a pool for the passed level pieces stored at the passed poolObject
    private void GeneratePool(GameObject[] levelPieces, ref List<GameObject> poolObject, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int rand = Random.Range(0, levelPieces.Length);
            GameObject instance = Instantiate(levelPieces[rand]);
            instance.SetActive(false);
            instance.transform.SetParent(this.transform);
            poolObject.Add(instance);
        }
    }

    private GameObject GetTerminalLevelPiece(GateType endingGate, bool isStarting = true)
    {
        if (isStarting)
        {
            switch (endingGate)
            {
                case GateType.DOWN:
                    return GetRandomLevelPieceFromList(StartingLevelPieces_Down);
                case GateType.LEFT:
                    return GetRandomLevelPieceFromList(StartingLevelPieces_Left);
                case GateType.RIGHT:
                    return GetRandomLevelPieceFromList(StartingLevelPieces_Right);
                default:
                    return GetGameObjectFromType("");
            }
        }
        else
        {
            switch (endingGate)
            {
                case GateType.UP:
                    return GetRandomLevelPieceFromList(EndingLevelPieces_Up);
                case GateType.LEFT:
                    return GetRandomLevelPieceFromList(EndingLevelPieces_Left);
                case GateType.RIGHT:
                    return GetRandomLevelPieceFromList(EndingLevelPieces_Right);
                default:
                    print($"Returning Empty cz {endingGate.ToString()}");
                    return GetGameObjectFromType("");
            }
        }
    }

    //Returns a disabled level piece that matches the starting and ending gate
    private GameObject RequestObjectFromPool(GateType startingGame, GateType endingGate)
    {
        if (endingGate == GateType.None) endingGate = GateType.DOWN;

        if (startingGame == GateType.LEFT)
        {
            if (endingGate == GateType.DOWN)
            {
                return GetGameObjectFromType("LD");
            }
            else if (endingGate == GateType.RIGHT)
            {
                return GetGameObjectFromType("LR");
            }
            else
            {
                return GetGameObjectFromType("");
            }
        }
        else if (startingGame == GateType.RIGHT)
        {
            if (endingGate == GateType.DOWN)
            {
                return GetGameObjectFromType("RD");
            }
            else if (endingGate == GateType.LEFT)
            {
                return GetGameObjectFromType("LR");
            }
            else
            {
                return GetGameObjectFromType("");
            }
        }
        else if (startingGame == GateType.UP)
        {
            if (endingGate == GateType.DOWN)
            {
                return GetGameObjectFromType("UD");
            }
            else if (endingGate == GateType.RIGHT)
            {
                return GetGameObjectFromType("UR");
            }
            else if (endingGate == GateType.LEFT)
            {
                return GetGameObjectFromType("UL");
            }
            else
            {
                return GetGameObjectFromType("");
            }
        }
        else
        {
            return GetGameObjectFromType("");
        }
    }

    // Returns a random level piece from any pool
    public GameObject GetRandomObjectFromPool()
    {
        int rand = Random.Range(0, 6);
        switch (rand)
        {
            case 0:
                return GetGameObjectFromType("LR");
            case 1:
                return GetGameObjectFromType("LD");
            case 2:
                return GetGameObjectFromType("RD");
            case 3:
                return GetGameObjectFromType("UD");
            case 4:
                return GetGameObjectFromType("UL");
            case 5:
                return GetGameObjectFromType("UR");
            default:
                return GetGameObjectFromType("");
        }
    }

    // Returns the terminal level pieces
    private GameObject GetRandomLevelPieceFromList(GameObject[] arrayToSearch)
    {
        int rand = Random.Range(0, arrayToSearch.Length);

        GameObject g = Instantiate<GameObject>(arrayToSearch[rand]);
        g.SetActive(false);
        g.transform.SetParent(this.transform);

        return g;
    }

    // Returns the level piece according to thier type
    private GameObject GetGameObjectFromType(string levelPieceType)
    {
        switch (levelPieceType)
        {
            case "LR":
                foreach (GameObject g in LR_Pool)
                {
                    if (!g.activeInHierarchy)
                    {
                        return g;
                    }
                }
                GameObject instance_LR = Instantiate(LevelPieces_LR[Random.Range(0, LevelPieces_LR.Length)]);
                instance_LR.SetActive(false);
                instance_LR.transform.SetParent(this.transform);
                LR_Pool.Add(instance_LR);

                return instance_LR;
            case "LD":
                foreach (GameObject g in LD_Pool)
                {
                    if (!g.activeInHierarchy)
                    {
                        return g;
                    }
                }

                GameObject instance_LD = Instantiate(LevelPieces_LD[Random.Range(0, LevelPieces_LD.Length)]);
                instance_LD.SetActive(false);
                instance_LD.transform.SetParent(this.transform);
                LD_Pool.Add(instance_LD);

                return instance_LD;
            case "RD":
                foreach (GameObject g in RD_Pool)
                {
                    if (!g.activeInHierarchy)
                    {
                        return g;
                    }
                }

                GameObject instance_RD = Instantiate(LevelPieces_RD[Random.Range(0, LevelPieces_RD.Length)]);
                instance_RD.SetActive(false);
                instance_RD.transform.SetParent(this.transform);
                RD_Pool.Add(instance_RD);

                return instance_RD;
            case "UL":
                foreach (GameObject g in UL_Pool)
                {
                    if (!g.activeInHierarchy)
                    {
                        return g;
                    }
                }

                GameObject instance_UL = Instantiate(LevelPieces_UL[Random.Range(0, LevelPieces_UL.Length)]);
                instance_UL.SetActive(false);
                instance_UL.transform.SetParent(this.transform);
                UL_Pool.Add(instance_UL);

                return instance_UL;
            case "UR":
                foreach (GameObject g in UR_Pool)
                {
                    if (!g.activeInHierarchy)
                    {
                        return g;
                    }
                }

                GameObject instance_UR = Instantiate(LevelPieces_UR[Random.Range(0, LevelPieces_UR.Length)]);
                instance_UR.SetActive(false);
                instance_UR.transform.SetParent(this.transform);
                UR_Pool.Add(instance_UR);

                return instance_UR;
            case "UD":
                foreach (GameObject g in UD_Pool)
                {
                    if (!g.activeInHierarchy)
                    {
                        return g;
                    }
                }

                GameObject instance_UD = Instantiate(LevelPieces_UD[Random.Range(0, LevelPieces_UD.Length)]);
                instance_UD.SetActive(false);
                instance_UD.transform.SetParent(this.transform);
                UD_Pool.Add(instance_UD);

                return instance_UD;
            case "Hurdle":
                foreach (GameObject g in Hurdles_Pool)
                {
                    if (!g.activeInHierarchy)
                    {
                        return g;
                    }
                }

                GameObject instance_Hurdle = Instantiate(Hurdle);
                instance_Hurdle.SetActive(false);
                instance_Hurdle.transform.SetParent(transform);
                Hurdles_Pool.Add(instance_Hurdle);

                return instance_Hurdle;
            default:
                foreach (GameObject g in Base_Pool)
                {
                    if (!g.activeInHierarchy)
                    {
                        return g;
                    }
                }

                GameObject instance = Instantiate(LevelPieceTemplate);
                instance.SetActive(false);
                instance.transform.SetParent(this.transform);
                Base_Pool.Add(instance);

                return instance;
        }
    }
}
