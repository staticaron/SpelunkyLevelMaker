using UnityEngine;

public class SurroudingData
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public Vector2Int GetRandomDirection(bool completeGeneration)
    {
        if (down && left && right)
        {
            return Vector2Int.zero;
        }

        Vector2Int direction = Vector2Int.zero;

        int rand = Random.Range(0, 3);

        while (direction == Vector2Int.zero)
        {
            if (rand == 0 && down == false && completeGeneration == true)
            {
                direction = new Vector2Int(0, 1);
                break;
            }
            else if (rand == 0 && down == true && completeGeneration == false)
            {
                direction = Vector2Int.zero;
                Debug.Log("<b>Tried to go down, but it was already occupied</b>");
                break;
            }
            else if (rand == 1 && left == false)
            {
                direction = new Vector2Int(-1, 0);
                break;
            }
            else if (rand == 2 && right == false)
            {
                direction = new Vector2Int(1, 0);
                break;
            }
            else
            {
                rand = (rand + 1) % 3;
                continue;
            }
        }

        return direction;
    }
}
