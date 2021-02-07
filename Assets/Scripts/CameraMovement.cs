using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;
    void Update()
    {
        var newPosition = new Vector3(transform.position.x,
                                      _target.position.y,
                                      transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, _speed * Time.deltaTime);
    }
}
