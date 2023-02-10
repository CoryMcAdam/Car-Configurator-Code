using Cinemachine;
using UnityEngine;

public class CarObject : MonoBehaviour
{
    [Header("Base Parts")]
    [SerializeField] private MeshRenderer[] renderers;

    [Header("Other parts")]
    [SerializeField] private CarPartWheel[] wheels;
    [SerializeField] private CarPartBackBumper backBumper;
    [SerializeField] private CarPartBumper bumper;
    [SerializeField] private CarPartHood hood;
    [SerializeField] private CarPartSkirts skirts;
    [SerializeField] private CarPartSpoiler spoiler;

    [Header("Camera Angles")]
    [SerializeField] private CinemachineVirtualCamera defaultCamera;
    [SerializeField] private CinemachineVirtualCamera wheelCamera;
    [SerializeField] private CinemachineVirtualCamera backBumperCamera;
    [SerializeField] private CinemachineVirtualCamera bumperCamera;
    [SerializeField] private CinemachineVirtualCamera hoodCamera;
    [SerializeField] private CinemachineVirtualCamera skirtsCamera;
    [SerializeField] private CinemachineVirtualCamera spoilerCamera;

    public Transform GetCameraForPart(ECarPart carPart)
    {
        switch (carPart)
        {
            case ECarPart.Base:
                return defaultCamera.transform;
            case ECarPart.Wheel:
                return wheelCamera.transform;
            case ECarPart.BackBumper:
                return backBumperCamera.transform;
            case ECarPart.Bumper:
                return bumperCamera.transform;
            case ECarPart.Hood:
                return hoodCamera.transform;
            case ECarPart.Skirts:
                return skirtsCamera.transform;
            case ECarPart.Spoiler:
                return spoilerCamera.transform;
        }

        return null;
    }

    public void SetCamera(ECarPart carPart)
    {
        defaultCamera.Priority = carPart == ECarPart.Base ? 15 : 10;
        wheelCamera.Priority = carPart == ECarPart.Wheel ? 15 : 10;
        backBumperCamera.Priority = carPart == ECarPart.BackBumper ? 15 : 10;
        bumperCamera.Priority = carPart == ECarPart.Bumper ? 15 : 10;
        hoodCamera.Priority = carPart == ECarPart.Hood ? 15 : 10;
        skirtsCamera.Priority = carPart == ECarPart.Skirts ? 15 : 10;
        spoilerCamera.Priority = carPart == ECarPart.Spoiler ? 15 : 10;
    }

    public void SetCarPart(ECarPart partType, Mesh mesh)
    {
        switch (partType)
        {
            case ECarPart.Wheel:
                {
                    for (int i = 0; i < wheels.Length; i++)
                    {
                        wheels[i].SetMesh(mesh);
                    }
                }
                break;
            case ECarPart.BackBumper:
                backBumper.SetMesh(mesh);
                break;
            case ECarPart.Bumper:
                bumper.SetMesh(mesh);
                break;
            case ECarPart.Hood:
                hood.SetMesh(mesh);
                break;
            case ECarPart.Skirts:
                skirts.SetMesh(mesh);
                break;
            case ECarPart.Spoiler:
                spoiler.SetMesh(mesh);
                break;
        }
    }

    public void SetPartColour(ECarPart partType, CarPaintSO paintData)
    {
        switch (partType)
        {
            case ECarPart.Base:
                {
                    for (int i = 0; i < renderers.Length; i++)
                    {
                        Material[] tempMats = renderers[i].materials;
                        tempMats[0] = paintData.Material;
                        renderers[i].materials = tempMats;
                    }
                }
                break;
            case ECarPart.Wheel:
                {
                    for (int i = 0; i < wheels.Length; i++)
                    {
                        wheels[i].SetMaterial(paintData.Material);
                    }
                }
                break;
            case ECarPart.BackBumper:
                backBumper.SetMaterial(paintData.Material);
                break;
            case ECarPart.Bumper:
                bumper.SetMaterial(paintData.Material);
                break;
            case ECarPart.Hood:
                hood.SetMaterial(paintData.Material);
                break;
            case ECarPart.Skirts:
                skirts.SetMaterial(paintData.Material);
                break;
            case ECarPart.Spoiler:
                spoiler.SetMaterial(paintData.Material);
                break;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}