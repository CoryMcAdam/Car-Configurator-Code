using System;

[Serializable]
public class CarPartConfiguration
{
    public CarPartSO CarPart;
    public CarPaintSO CarPaint;

    public CarPartConfiguration(CarPartSO carPart, CarPaintSO carPaint)
    {
        CarPart = carPart;
        CarPaint = carPaint;
    }
}