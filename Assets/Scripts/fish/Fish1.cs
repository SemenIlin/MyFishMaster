using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Fish1 : MonoBehaviour, IFish
{
    [SerializeField] private GameObject _fishTextPrefab;
    [SerializeField] private int _reward;
    [Header("X begin depth Y - end depth")]
    [SerializeField] private Vector2 _depthOfHabitat;

    private Animator _animator;
    private bool _isRight = true;
    private float _speed;
    public int Reward => _reward;

    public Vector2 DepthOfHabitat => _depthOfHabitat;
    private GameObject _text;

    private void Start()
    {
        _text = Instantiate(_fishTextPrefab, transform);
        _animator = GetComponent<Animator>();
        _speed = Random.Range(1.1f, 7.2f);
    }

    public void DoUpdate()
    {
        Move();
        ChangeDirection(transform);        
    }

    public void SetDeptOfHabitat(Vector2 depth)
    {
        _depthOfHabitat = depth;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void CatchFish(Transform hook)
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
        _animator.SetBool("isCatch", true);

        transform.position = hook.position;
    }

    public void ShowText(Transform parent)
    {
        var textFish = _text.GetComponent<FishText>();
        textFish.ShowText(Reward, transform.rotation);

        _text.transform.parent = parent;
    }

    private void Move()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + _speed * Time.fixedDeltaTime,
                                                    GameManager.Instance.CameraEdges.w,
                                                    GameManager.Instance.CameraEdges.y),
                                        transform.position.y);
    }

    private void ChangeDirection(Transform transform)
    {
        if (transform.position.x == GameManager.Instance.CameraEdges.w ||
           transform.position.x == GameManager.Instance.CameraEdges.y)
        {
            _speed = -_speed;
            _animator.SetBool("isRight", !_isRight);
            _isRight = !_isRight;
        }
    }
}
