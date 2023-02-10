using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectionToggle : MonoBehaviour
{
    [SerializeField] private ECarPart carPart;

    [SerializeField] private UnityEvent<ECarPart> ToggledOnEvent = new UnityEvent<ECarPart>();

    private Toggle _toggle;

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

        ToggledOnEvent?.Invoke(carPart);
    }
}
