using UnityEngine;

public class UIMovable: MonoBehaviour, IMovable
{
    public void Move(Vector2 destination)
    {
        transform.position = destination;
    }
}
