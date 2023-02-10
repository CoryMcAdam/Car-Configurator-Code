using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPaintToggle : MonoBehaviour
{
    [SerializeField] private Image previewImage;
    [SerializeField] private UnityEvent<int> ToggledOnEvent = new UnityEvent<int>();

    private Toggle _toggle;

    private int _paintIndex;

    public void Setup(CarPaintSO paintData, int paintIndex)
    {
        _paintIndex = paintIndex;

        previewImage.color = paintData.PreviewColour;
    }

    public void Clear()
    {
        _toggle.interactable = false;
        previewImage.sprite = null;
    }

    public void Select()
    {
        _toggle.SetIsOnWithoutNotify(true);
    }

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnToggled);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnToggled);
    }

    public void OnToggled(bool isOn)
    {
        if (!isOn)
            return;

        ToggledOnEvent?.Invoke(_paintIndex);
    }
}
