using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class FishText : MonoBehaviour
{
    private TextMeshPro _text;
    private void Start()
    {
        _text = GetComponent<TextMeshPro>();
    }

    public void ShowText(int value, Quaternion rotation)
    {
        _text.enabled = true;
        _text.text = "+ " + value.ToString();
        var angle = rotation.eulerAngles.y != 0 ? 0 : 0;
        _text.transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
