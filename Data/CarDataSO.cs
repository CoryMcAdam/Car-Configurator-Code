using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Car/Car Data")]
public class CarDataSO : ScriptableObject
{
    //Car info.
    [SerializeField] private string carName;
    [SerializeField] private int baseCost;
    [SerializeField] private Sprite carPreview;
    [SerializeField] private CarObject carPrefab;
    [SerializeField] private CarPaintSO defaultPaint;

    //Car parts.
    [SerializeField] private List<CarPartSO_Wheel> wheelParts;
    [SerializeField] private List<CarPartSO_BackBumper> backBumperParts;
    [SerializeField] private List<CarPartSO_Bumper> bumperParts;
    [SerializeField] private List<CarPartSO_Hood> hoodParts;
    [SerializeField] private List<CarPartSO_Skirts> skirtsParts;
    [SerializeField] private List<CarPartSO_Spoiler> spoilerParts;

    //Properties.
    public string CarName { get { return carName; } }
    public int BaseCost { get { return baseCost; } }
    public Sprite CarPreview { get { return carPreview; } }
    public CarObject CarPrefab { get { return carPrefab; } }
    public CarPaintSO DefaultPaint { get { return defaultPaint; } }

    public CarPartSO GetPart(ECarPart carPart, int partIndex)
    {
        switch (carPart)
        {
            case ECarPart.Wheel:
                return wheelParts[partIndex];
            case ECarPart.BackBumper:
                return backBumperParts[partIndex];
            case ECarPart.Bumper:
                return bumperParts[partIndex];
            case ECarPart.Hood:
                return hoodParts[partIndex];
            case ECarPart.Skirts:
                return skirtsParts[partIndex];
            case ECarPart.Spoiler:
                return spoilerParts[partIndex];
        }

        return null;
    }

    public int GetPartIndex(ECarPart carPart, CarPartSO partData)
    {
        switch (carPart)
        {
            case ECarPart.Wheel:
                return wheelParts.IndexOf((CarPartSO_Wheel)partData);
            case ECarPart.BackBumper:
                return backBumperParts.IndexOf((CarPartSO_BackBumper)partData);
            case ECarPart.Bumper:
                return bumperParts.IndexOf((CarPartSO_Bumper)partData);
            case ECarPart.Hood:
                return hoodParts.IndexOf((CarPartSO_Hood)partData);
            case ECarPart.Skirts:
                return skirtsParts.IndexOf((CarPartSO_Skirts)partData);
            case ECarPart.Spoiler:
                return spoilerParts.IndexOf((CarPartSO_Spoiler)partData);
        }

        return -1;
    }

    public List<CarPartSO> GetParts(ECarPart partType)
    {
        switch (partType)
        {
            case ECarPart.Wheel:
                return wheelParts.ConvertAll(x => (CarPartSO)x);
            case ECarPart.BackBumper:
                return backBumperParts.ConvertAll(x => (CarPartSO)x);
            case ECarPart.Bumper:
                return bumperParts.ConvertAll(x => (CarPartSO)x);
            case ECarPart.Hood:
                return hoodParts.ConvertAll(x => (CarPartSO)x);
            case ECarPart.Skirts:
                return skirtsParts.ConvertAll(x => (CarPartSO)x);
            case ECarPart.Spoiler:
                return spoilerParts.ConvertAll(x => (CarPartSO)x);
        }

        return new List<CarPartSO>();
    }
}