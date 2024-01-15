using TMPro;
using UnityEngine;


[ExecuteInEditMode]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textLabel;

    private Vector2Int _coordinates;

    
    private void Start()
    {
        _textLabel.enabled = false;
    }

    
    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _textLabel.enabled = !_textLabel.enabled;
        }
    }

    
    private void DisplayCoordinates()
    {
        var position = transform.position;
        _coordinates.x = Mathf.RoundToInt(position.x / 10);
        _coordinates.y = Mathf.RoundToInt(position.z / 10);
        _textLabel.text = _coordinates.ToString();
    }

    
    private void UpdateObjectName()
    {
        name = _coordinates.ToString();
    }
}
