using UnityEngine;

public class FirePoints : MonoBehaviour
{
    [SerializeField]
    private Transform [] points;

    public Transform GetRandomPoint()
    {
        return points [Random.Range (0, points.Length - 1)];
    }
}
