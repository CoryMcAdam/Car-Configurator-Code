using UnityEngine;

public class CarPart : MonoBehaviour
{
    private Renderer _renderer;
    private MeshFilter _meshFilter;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _meshFilter = GetComponent<MeshFilter>();
    }

    public void SetMesh(Mesh mesh)
    {
        _meshFilter.mesh = mesh;
    }

    public void SetMaterial(Material mat)
    {
        _renderer.material = mat;
    }
}