using UnityEngine;

public class BlockDrag : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] RectTransform parent;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rect.position = parent.position;
    }
}