using System.IO;
using UnityEngine;

public class CarConfigurationOutput : MonoBehaviour
{
    [SerializeField] private CarManager carManager;

    public void SaveConfiguration()
    {
        string carData = GetConfigurationData();
        string path = $"{Application.dataPath}/../CarConfiguration.txt";

        if (path.Length != 0)
        {
            if (carData.Length != 0)
                File.WriteAllText(path, carData);
        }
    }

    public string GetConfigurationData()
    {
        string data = "";

        data += $"{carManager.CurrentCarData.CarName} ({carManager.GetConfiguredPaint(ECarPart.Base).PaintName}) - {carManager.CurrentCarData.BaseCost.ToString("£00,000")}";

        data += GetPartText(ECarPart.Wheel);
        data += GetPartText(ECarPart.Bumper);
        data += GetPartText(ECarPart.BackBumper);
        data += GetPartText(ECarPart.Hood);
        data += GetPartText(ECarPart.Spoiler);
        data += GetPartText(ECarPart.Skirts);

        data += $"\n TOTAL COST: {carManager.GetTotalCost().ToString("£00,000")}";

        return data;
    }

    public string GetPartText(ECarPart carPart)
    {
        CarPartSO partData = carManager.GetConfiguredPart(carPart);
        CarPaintSO paintData = carManager.GetConfiguredPaint(carPart);

        return $"\n{carPart.ToString()}: {partData.PartName} ({paintData.PaintName}) - {partData.Cost.ToString("£00,000")}";
    }
}
