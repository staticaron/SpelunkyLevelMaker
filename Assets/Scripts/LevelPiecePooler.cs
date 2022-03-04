using UnityEngine;
using System.Collections.Generic;

public class LevelPiecePooler : MonoBehaviour
{
    [SerializeField] GameObject[] LevelPieces_LR;
    [SerializeField] GameObject[] LevelPieces_LD;
    [SerializeField] GameObject[] LevelPieces_RD;
    [SerializeField] GameObject[] LevelPieces_UL;
    [SerializeField] GameObject[] LevelPieces_UR;
    [SerializeField] GameObject[] LevelPieces_UD;

    public GameObject LevelPieceTemplate;

    [SerializeField] int poolSize = 3;

    [SerializeField] List<GameObject> LR_Pool;
    [SerializeField] List<GameObject> LD_Pool;
    [SerializeField] List<GameObject> RD_Pool;
    [SerializeField] List<GameObject> UL_Pool;
    [SerializeField] List<GameObject> UR_Pool;
    [SerializeField] List<GameObject> UD_Pool;
    [SerializeField] List<GameObject> Base_Pool;

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

        GeneratePool();
    }

    private void OnEnable()
    {
        piecePoolerChannelSO.ERequestPoolObjectFromGates += RequestObjectFromPool;
        piecePoolerChannelSO.EGetRandomPoolObject += GetRandomObjectFromPool;
    }

    private void OnDisable()
    {
        piecePoolerChannelSO.ERequestPoolObjectFromGates -= RequestObjectFromPool;
        piecePoolerChannelSO.EGetRandomPoolObject -= GetRandomObjectFromPool;
    }

    [ContextMenu("Generate Pool")]
    private void GeneratePool()
    {
        for (int i = 0; i < poolSize * 3; i++)
        {
            GameObject instance = Instantiate<GameObject>(LevelPieceTemplate);
            instance.SetActive(false);
            instance.transform.SetParent(this.transform);

            Base_Pool.Add(instance);
        }

        GeneratePool(LevelPieces_LR, ref LR_Pool);
        GeneratePool(LevelPieces_LD, ref LD_Pool);
        GeneratePool(LevelPieces_RD, ref RD_Pool);
        GeneratePool(LevelPieces_UD, ref UD_Pool);
        GeneratePool(LevelPieces_UL, ref UL_Pool);
        GeneratePool(LevelPieces_UR, ref UR_Pool);
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
    private void GeneratePool(GameObject[] levelPieces, ref List<GameObject> poolObject)
    {
        for (int i = 0; i < poolSize; i++)
        {
            int rand = Random.Range(0, levelPieces.Length);
            GameObject instance = Instantiate(levelPieces[rand]);
            instance.SetActive(false);
            instance.transform.SetParent(this.transform);
            poolObject.Add(instance);
        }
    }

    //Returns a disabled level piece that matches the starting and ending gate
    public GameObject RequestObjectFromPool(GateType startingGame, GateType endingGate)
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
            default:
                GameObject instance = Instantiate(LevelPieceTemplate);
                instance.SetActive(false);
                instance.transform.SetParent(this.transform);
                return instance;
        }
    }
}
