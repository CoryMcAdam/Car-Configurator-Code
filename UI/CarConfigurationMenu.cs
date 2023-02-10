using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarConfigurationMenu : MonoBehaviour
{
    [SerializeField] private CarManager carManager;

    [SerializeField] private GameObject changeCarMenu;

    [SerializeField] private TextMeshProUGUI carSelectionText;
    [SerializeField] private TextMeshProUGUI partSelectionText;

    [SerializeField] private List<UIPaintToggle> paintToggles = new List<UIPaintToggle>();
    [SerializeField] private List<UICarPartToggle> carPartToggles = new List<UICarPartToggle>();

    [Header("Cost")]
    [SerializeField] private TextMeshProUGUI carNameText;
    [SerializeField] private TextMeshProUGUI carCostText;
    [SerializeField] private TextMeshProUGUI modificationCountText;
    [SerializeField] private TextMeshProUGUI modificationCostText;
    [SerializeField] private TextMeshProUGUI totalCostText;

    private void OnEnable()
    {
        carManager.CarChangedEvent += CarManager_CarChangedEvent;
        carManager.PartSelectionChangedEvent += CarManager_PartSelectionChangedEvent;
        carManager.PartChangedEvent += CarManager_PartChangedEvent;
    }

    private void OnDisable()
    {
        if (carManager != null)
        {
            carManager.CarChangedEvent -= CarManager_CarChangedEvent;
            carManager.PartSelectionChangedEvent -= CarManager_PartSelectionChangedEvent;
            carManager.PartChangedEvent -= CarManager_PartChangedEvent;
        }
    }

    private void CarManager_CarChangedEvent(CarDataSO carData)
    {
        carNameText.text = carData.CarName;
        carSelectionText.text = carData.CarName;

        RefreshPaintsList();
        RefreshPartsList();
        RefreshCost();
    }

    private void CarManager_PartSelectionChangedEvent(ECarPart carPart)
    {
        partSelectionText.text = carPart.ToString();

        changeCarMenu.SetActive(carPart == ECarPart.Base);

        RefreshPaintsList();
        RefreshPartsList();
    }

    private void CarManager_PartChangedEvent(ECarPart carPart, CarPartSO partData)
    {
        RefreshCost();
    }

    private void RefreshPaintsList()
    {
        List<CarPaintSO> paints = carManager.GetPaints();

        for (int i = 0; i < paintToggles.Count; i++)
        {

            if (i > paints.Count - 1)
            {
                paintToggles[i].Clear();
                continue;
            }

            paintToggles[i].Setup(paints[i], i);
        }

        //Get the current paint for the selected part and select the relevant button.
        int currentPaintIndex = carManager.GetCurrentPaintIndex(carManager.CurrentPartSelection);
        paintToggles[currentPaintIndex].Select();
    }

    public void RefreshPartsList()
    {
        if (carManager.CurrentPartSelection == ECarPart.Base)
        {
            for (int i = 0; i < carPartToggles.Count; i++)
            {
                carPartToggles[i].ClearToggle();
            }

            carPartToggles[0].SetupPart(carManager.CurrentCarData);
            carPartToggles[0].Select();
        }
        else
        {
            List<CarPartSO> carParts = carManager.GetAllParts(carManager.CurrentPartSelection);

            for (int i = 0; i < carPartToggles.Count; i++)
            {

                if (i > carParts.Count - 1)
                {
                    carPartToggles[i].ClearToggle();
                    continue;
                }

                carPartToggles[i].SetupPart(carParts[i], i);
            }

            int currentPartIndex = carManager.GetCurrentPartIndex(carManager.CurrentPartSelection);

            carPartToggles[currentPartIndex].Select();
        }
    }

    private void RefreshCost()
    {
        carCostText.text = carManager.GetBaseCost().ToString("£00,000");

        modificationCostText.text = carManager.GetTotalModificationCost().ToString("£00,000");
        modificationCountText.text = $"{carManager.GetModificationCount().ToString()} Modifications";

        totalCostText.text = carManager.GetTotalCost().ToString("£00,000");
    }

    #region UI Calls

    public void SelectNextCar()
    {
        carManager.SelectCar(carManager.CurrentCarIndex + 1);
    }

    public void SelectPreviousCar()
    {
        carManager.SelectCar(carManager.CurrentCarIndex - 1);
    }

    public void SetPartColourIndex(int paintIndex)
    {
        carManager.SetPaintIndex(carManager.CurrentPartSelection, paintIndex);
    }

    public void SetPartIndex(int index)
    {
        carManager.SetPartIndex(carManager.CurrentPartSelection, index);
    }

    public void SetSelection(ECarPart partType)
    {
        carManager.SetSelectedPart(partType);
    }

    #endregion
}