using UnityEngine;

[CreateAssetMenu(menuName = "Car/Paint")]
public class CarPaintSO : ScriptableObject
{
    [SerializeField] private string paintName;
    public string PaintName { get { return paintName; } }

    [SerializeField] private int cost;
    public int Cost { get { return cost; } }

    [SerializeField] private Material mat;
    public Material Material { get { return mat; } }

    [SerializeField] private Color previewColour;
    public Color PreviewColour { get { return previewColour; } }
}
