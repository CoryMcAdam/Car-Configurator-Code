using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICarPartToggle : MonoBehaviour
{
    [SerializeField] private Image partImage;

    [SerializeField] private UnityEvent<int> ToggledOnEvent = new UnityEvent<int>();

    private Toggle _toggle;
    private int _partIndex;

    public void SetupPart(CarPartSO carPart, int index)
    {
        _partIndex = index;
        partImage.sprite = carPart.Icon;
        _toggle.interactable = true;
    }

    public void SetupPart(CarDataSO carData)
    {
        partImage.sprite = carData.CarPreview;
        _toggle.interactable = true;
    }

    public void ClearToggle()
    {
        _toggle.interactable = false;
        partImage.sprite = null;
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

        ToggledOnEvent?.Invoke(_partIndex);
    }
}