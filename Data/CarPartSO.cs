using UnityEngine;

public abstract class CarPartSO : ScriptableObject
{
    [SerializeField] private string partName;
    [SerializeField] private int cost;
    [SerializeField] private Sprite icon;
    [SerializeField] private Mesh partMesh;

    public string PartName { get { return partName; } }
    public int Cost { get { return cost; } }
    public Sprite Icon { get { return icon; } set { icon = value; } }
    public Mesh PartMesh { get { return partMesh; } }
}