using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _gameplayUI;
    public static GameManager Instance { get; private set; }

    public Transform GameplayUI => _gameplayUI;
    public Vector4 CameraEdges { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CameraEdges = new Vector4(
            Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0)).y,
            Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0)).x,
            Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).y,
            Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0)).x
        );
    }
}
