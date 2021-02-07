using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class fishHookHandle : MonoBehaviour
{
	[SerializeField] private FishManager _fishManager;

	[SerializeField] private float _fallQuicklySpeed;
	[SerializeField] private float _upSpeed;

	private float _currentSpeed;

	private List<Fish1> _caughtFish = new List<Fish1>();

	private PolygonCollider2D _collider;
	private Price _price = new Price();
	private UIManager _UI;

	private int _depth;
	private int _strengthMax;
	private int _currentStrength = 0;


	private bool _isClick = false;
	private bool _wasOnBottom = false;

	private Vector2 _mousePosition;
	private Vector2 offset;
	private bool clicked;

	private void Start()
	{
		_collider = GetComponent<PolygonCollider2D>();
		_UI = UIManager.Instance;

		_depth = _price.Length;
		_strengthMax = _price.Strength;

		UpdateUI();
		_UI.ShowMainScreen(true);


		offset = transform.position;
	}

	private void Update()
	{
        if (_isClick)
        {
			// Move to bottom
            if (transform.position.y >= -_depth && !_wasOnBottom)
            {
				ChangeSpeed(_wasOnBottom);
                Move();
            }
            if (transform.position.y <= -_depth)
            {
				_collider.enabled = true;
                _wasOnBottom = true;
            }
			// move to up
            if (_wasOnBottom)
            {
				if(_currentStrength >= _strengthMax)
                {
					_collider.enabled = false;
					ChangeSpeed();
                }
                else
                {
					ChangeSpeed(_wasOnBottom);
                }

                Move();
            }

            if (transform.position.y >= 0)
            {
                _UI.ShowMainScreen(true);
                _isClick = false;			
                _wasOnBottom = false;
                _currentSpeed = 0;
				_currentStrength = 0;
				AddMoney();
				SaveManager.Instance.SaveGame();
            }

			// change position x
			if (Input.GetMouseButton(0))
			{
				_mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				if (!clicked)
				{
					offset = (Vector2)transform.position - _mousePosition;
					clicked = true;
				}

				Vector2 newPos = new Vector2(
					Mathf.Clamp(_mousePosition.x + offset.x,
								GameManager.Instance.CameraEdges.w,
								GameManager.Instance.CameraEdges.y),
					transform.position.y
				);

				transform.position = newPos;
			} //Clicked
			else
			{
				clicked = false;
			} //Released
		}
	}

	public void StartGame()
    {
		_fishManager.Spawn();
		_collider.enabled = false;
		_isClick = true;
		_UI.ShowMainScreen(false);

		_depth = _price.Length;
		_strengthMax = _price.Strength;
		_UI.UpdateMaxFish(_strengthMax);
	}

	private void AddMoney()
    {
		foreach(var fish in _caughtFish)
        {
			PlayerProgress.Money += fish.Reward;
        }

		UpdateUI();

		_fishManager.DestroyFish(_caughtFish);
    }

	private void UpdateUI()
	{
		_UI.UpdateCurrentMoney();
		_UI.SetInteractableLengthButton(_price.HasBuyLength());
		_UI.SetInteractableStrengthButton(_price.HasBuyStrength());

		_UI.UpdateLength(_price.Length,
						 _price.NextLengthPrice);

		_UI.UpdateStrength(_price.NextStrength,
						   _price.NextStrengthPrice);

		_UI.UpdateCurrentFish(_currentStrength);
		_UI.UpdateMaxFish(_strengthMax);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Fish"))
		{
			Vibration.VibratePeek();

			var fishObj = other.gameObject;
			fishObj.transform.parent = transform;

			var fish = fishObj.GetComponent<Fish1>();
			fish.ShowText(_fishManager.transform);
			_caughtFish.Add(fish);
			++_currentStrength;
			_UI.UpdateCurrentFish(_currentStrength);

			_fishManager.RemoveFish(fish);
			fish.CatchFish(transform.GetChild(0));

			other.enabled = false;
		}
	}

	private void Move()
    {
		transform.position = new Vector3(transform.position.x,
										 transform.position.y + _currentSpeed * Time.deltaTime,
										 transform.position.z);
    }

	private void ChangeSpeed()
    {
		_currentSpeed = -_fallQuicklySpeed;
    }

	private void ChangeSpeed(bool isUp)
	{
		_currentSpeed = isUp ? _upSpeed : _fallQuicklySpeed;
	}
}
