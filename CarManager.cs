using System;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    //Editor fields.
    [SerializeField] private List<CarDataSO> cars = new List<CarDataSO>();
    [SerializeField] private List<CarPaintSO> paints = new List<CarPaintSO>();

    private int _currentCarIndex; //Index of current car, used for cycling through selected cars.
    private ECarPart _currentPartSelection = ECarPart.Base; //Current part being modified, used by other components.
    private CarDataSO _currentCarData; //For getting car data.

    private bool _hasStarted = false;

    //For getting the car configuration.
    private Dictionary<ECarPart, CarPartConfiguration> _selectedPartsDictionary = new Dictionary<ECarPart, CarPartConfiguration>();

    //Properties.
    public int CurrentCarIndex { get { return _currentCarIndex; } }
    public CarDataSO CurrentCarData { get { return _currentCarData; } }
    public ECarPart CurrentPartSelection { get { return _currentPartSelection; } }

    //Events.
    public event Action<CarDataSO> CarChangedEvent;
    public event Action<ECarPart> PartSelectionChangedEvent;
    public event Action<ECarPart, CarPartSO> PartChangedEvent;
    public event Action<ECarPart, CarPaintSO> PaintChangedEvent;

    private void Awake()
    {
        InitializeDefaultSettings();
    }

    private void Start()
    {
        _hasStarted = true;
        SelectCar(0, true);
    }

    public void SelectCar(int index, bool force = false)
    {
        //Stop out of bounds index being selected.
        int newIdex = ClampCarIndex(index);

        //Return if trying to select the same car.
        if (!force && _currentCarIndex == newIdex)
            return;

        //Update current data.
        _currentCarIndex = newIdex;
        _currentCarData = cars[_currentCarIndex];

        //Reset all the selected parts.
        ResetCarParts();

        CarChangedEvent?.Invoke(_currentCarData);

        ResetCarPaints();
    }

    #region Resetting

    private void InitializeDefaultSettings()
    {
        _currentCarData = cars[0];

        _selectedPartsDictionary = new Dictionary<ECarPart, CarPartConfiguration>();

        AddDefaultPartToDictionary(ECarPart.Base);
        AddDefaultPartToDictionary(ECarPart.Wheel);
        AddDefaultPartToDictionary(ECarPart.BackBumper);
        AddDefaultPartToDictionary(ECarPart.Bumper);
        AddDefaultPartToDictionary(ECarPart.Hood);
        AddDefaultPartToDictionary(ECarPart.Skirts);
        AddDefaultPartToDictionary(ECarPart.Spoiler);
    }

    private void AddDefaultPartToDictionary(ECarPart carPart)
    {
        _selectedPartsDictionary.Add(carPart, new CarPartConfiguration(_currentCarData.GetPart(carPart, 0), _currentCarData.DefaultPaint));
    }

    private void ResetCarParts()
    {
        SetPartIndex(ECarPart.Base, 0, false);
        SetPartIndex(ECarPart.Wheel, 0, false);
        SetPartIndex(ECarPart.BackBumper, 0, false);
        SetPartIndex(ECarPart.Bumper, 0, false);
        SetPartIndex(ECarPart.Hood, 0, false);
        SetPartIndex(ECarPart.Skirts, 0, false);
        SetPartIndex(ECarPart.Spoiler, 0, false);
    }

    private void ResetCarPaints()
    {
        int defaultPaintIndex = GetDefaultPaintIndex();

        SetPaintIndex(ECarPart.Base, defaultPaintIndex);
        SetPaintIndex(ECarPart.Wheel, defaultPaintIndex);
        SetPaintIndex(ECarPart.BackBumper, defaultPaintIndex);
        SetPaintIndex(ECarPart.Bumper, defaultPaintIndex);
        SetPaintIndex(ECarPart.Hood, defaultPaintIndex);
        SetPaintIndex(ECarPart.Skirts, defaultPaintIndex);
        SetPaintIndex(ECarPart.Spoiler, defaultPaintIndex);
    }

    #endregion

    #region Dictionary Helpers

    private void StoreConfiguredPart(ECarPart partType, CarPartSO partData)
    {
        if (_selectedPartsDictionary.ContainsKey(partType))
            _selectedPartsDictionary[partType].CarPart = partData;
        else
            _selectedPartsDictionary.Add(partType, new CarPartConfiguration(partData, _currentCarData.DefaultPaint));
    }

    private void StoreConfiguredPaint(ECarPart partType, CarPaintSO paintData)
    {
        if (_selectedPartsDictionary.ContainsKey(partType))
            _selectedPartsDictionary[partType].CarPaint = paintData;
        else
            _selectedPartsDictionary.Add(partType, new CarPartConfiguration(_currentCarData.GetPart(partType, 0), paintData));
    }

    #endregion

    #region Setters

    public void SetPartIndex(ECarPart carPart, int partIndex, bool invokeEvent = true)
    {
        if (!_hasStarted)
            return;

        if (carPart == ECarPart.Base)
            return;

        CarPartSO partData = _currentCarData.GetPart(carPart, partIndex);

        StoreConfiguredPart(carPart, partData);

        if (invokeEvent)
            PartChangedEvent?.Invoke(carPart, partData);
    }

    public void SetPaintIndex(ECarPart carPart, int paintIndex, bool invokeEvent = true)
    {
        if (!_hasStarted)
            return;

        CarPaintSO paintData = paints[paintIndex];

        StoreConfiguredPaint(carPart, paintData);

        if (invokeEvent)
            PaintChangedEvent?.Invoke(carPart, paintData);
    }

    public void SetSelectedPart(ECarPart partType, bool invokeEvent = true)
    {
        if (!_hasStarted)
            return;

        _currentPartSelection = partType;

        if (invokeEvent)
            PartSelectionChangedEvent?.Invoke(partType);
    }

    #endregion

    #region Getters

    public List<CarPartSO> GetAllParts(ECarPart partType)
    {
        if (_currentCarData == null)
            return new List<CarPartSO>();

        return _currentCarData.GetParts(partType);
    }

    public List<CarPaintSO> GetPaints()
    {
        return paints;
    }

    public CarPartSO GetConfiguredPart(ECarPart partType)
    {
        if (_selectedPartsDictionary.ContainsKey(partType))
            return _selectedPartsDictionary[partType].CarPart;

        return null;
    }

    public CarPaintSO GetConfiguredPaint(ECarPart partType)
    {
        if (_selectedPartsDictionary.ContainsKey(partType))
            return _selectedPartsDictionary[partType].CarPaint;

        return null;
    }

    public int GetModificationCount()
    {
        int count = 0;

        foreach (var (carPart, configuration) in _selectedPartsDictionary)
        {
            if (carPart == ECarPart.Base)
                continue;

            count += configuration.CarPart.Cost != 0 ? 1 : 0;
        }

        return count;
    }

    #endregion

    #region Get Index

    public int GetCurrentPartIndex(ECarPart carPart)
    {
        CarPartSO partData = _selectedPartsDictionary[carPart].CarPart;
        return _currentCarData.GetPartIndex(carPart, partData);
    }

    public int GetCurrentPaintIndex(ECarPart carPart)
    {
        CarPaintSO paintData = _selectedPartsDictionary[carPart].CarPaint;
        return paints.IndexOf(paintData);
    }

    public int GetDefaultPaintIndex()
    {
        CarPaintSO paintData = _currentCarData.DefaultPaint;
        return paints.IndexOf(paintData);
    }

    #endregion

    #region Cost Methods

    public int GetBaseCost()
    {
        return _currentCarData.BaseCost;
    }

    public int GetModificationCost(ECarPart carPart)
    {
        if (_selectedPartsDictionary.ContainsKey(carPart))
            return _selectedPartsDictionary[carPart].CarPart.Cost;

        return -1;
    }

    public int GetTotalModificationCost()
    {
        int cost = 0;

        foreach (var (carPart, configuration) in _selectedPartsDictionary)
        {
            if (carPart == ECarPart.Base)
                continue;

            cost += configuration.CarPart.Cost;
        }

        return cost;
    }

    public int GetTotalCost()
    {
        int cost = 0;

        cost += GetBaseCost();
        cost += GetTotalModificationCost();

        return cost;
    }

    #endregion

    #region Utility

    private int ClampCarIndex(int index)
    {
        if (index < 0)
            return cars.Count - 1;

        if (index >= cars.Count)
            return 0;

        return index;
    }

    #endregion
}
