using UnityEngine;

public class CarDisplay : MonoBehaviour
{
    [SerializeField] private CarManager carManager;
    [SerializeField] private Transform carHolder;

    private CarObject _currentCar;

    private void Awake()
    {
        carManager.CarChangedEvent += CarManager_CarChangedEvent;
        carManager.PartChangedEvent += CarManager_PartChangedEvent;
        carManager.PaintChangedEvent += CarManager_PaintChangedEvent;
        carManager.PartSelectionChangedEvent += CarManager_PartSelectionChangedEvent;
    }

    private void OnDestroy()
    {
        if (carManager != null)
        {
            carManager.CarChangedEvent -= CarManager_CarChangedEvent;
            carManager.PartChangedEvent -= CarManager_PartChangedEvent;
            carManager.PaintChangedEvent -= CarManager_PaintChangedEvent;
            carManager.PartSelectionChangedEvent -= CarManager_PartSelectionChangedEvent;
        }
    }

    private void CarManager_CarChangedEvent(CarDataSO carData)
    {
        //Destroy current car if it exists.
        if (_currentCar != null)
            _currentCar.Destroy();

        //Spawn the new car.
        _currentCar = Instantiate(carData.CarPrefab, carHolder);

        //Update the camera
        _currentCar.SetCamera(carManager.CurrentPartSelection);
    }

    private void CarManager_PartChangedEvent(ECarPart carPart, CarPartSO partData)
    {
        if (_currentCar != null)
            _currentCar.SetCarPart(carPart, partData.PartMesh);
    }

    private void CarManager_PaintChangedEvent(ECarPart carPart, CarPaintSO paintData)
    {
        if (_currentCar != null)
            _currentCar.SetPartColour(carPart, paintData);
    }

    private void CarManager_PartSelectionChangedEvent(ECarPart carPart)
    {
        _currentCar.SetCamera(carPart);
    }
}


