using Battlehub.RTCommon;
using Battlehub.RTHandles;
using UnityEngine;
using HolofairStudio;


public class SnapHandle : BaseHandle
{
    [SerializeField] private LayerMask _modelsLayer;

    private ControlType _controlType = ControlType.X;

    public override RuntimeTool Tool
    {
        get { return RuntimeTool.SnapX; }
    }

    public override void BeginDrag()
    {
        base.BeginDrag();
        Debug.Log("begain");
    }

    public override void EndDrag()
    {
        base.EndDrag();
        Debug.Log("end");

    }

    protected override void Update()
    {
        if (Editor.Input.GetPointer(0))
        {
            Ray ray = Window.Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Control(_controlType, hit.point, hit.normal);
            }
        }
    }


    private void Control(ControlType controlType, Vector3 point, Vector3 normal)
    {
        Vector3 direction = Vector3.zero;

        switch (controlType)
        {
            case ControlType.none:
                break;
            case ControlType.X:
                direction = Quaternion.Euler(new Vector3(0, -90, 0)) * normal;
                break;
            case ControlType.InverseX:
                direction = Quaternion.Euler(new Vector3(0, 90, 0)) * normal;
                break;
            case ControlType.Y:
                direction = Quaternion.Euler(new Vector3(90, 0, 0)) * normal;
                break;
            case ControlType.InverseY:
                direction = Quaternion.Euler(new Vector3(-90, 0, 0)) * normal;
                break;
            case ControlType.Z:
                direction = normal;
                break;
            case ControlType.InverseZ:
                direction = -normal;
                break;
            default:
                break;
        }
        foreach (var target in ActiveTargets)
        {
            target.SetPositionAndRotation(point, Quaternion.LookRotation(direction));
        }
    }

}
