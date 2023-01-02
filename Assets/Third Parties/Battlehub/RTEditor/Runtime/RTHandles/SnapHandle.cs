using Battlehub.RTCommon;
using Battlehub.RTHandles;
using UnityEngine;
using HolofairStudio;


public class SnapHandle : BaseHandle
{
    [SerializeField] private LayerMask _modelsLayer;
    private bool _invert;
    public override RuntimeTool Tool
    {
        get { return RuntimeTool.SnapX; }
    }

    protected override void Update()
    {
        if (Editor.Input.GetPointer(0))
        {
            Ray ray = Window.Camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 100, _modelsLayer))
                Control(hit);
        }
    }

    private void Control(RaycastHit hit)
    {
        foreach (var target in ActiveTargets)
        {
            if(hit.transform.root == target)
            {
                Debug.Log("same obj");
                return;
            }
        }

        Vector3 direction = Vector3.zero;
        RuntimeTool tool = Editor.Tools.Current;

        switch (tool)
        {
            case RuntimeTool.SnapX:
                direction = Quaternion.Euler(new Vector3(0, 90 * (_invert ? -1 : 1), 0)) * hit.normal;
                break;
            case RuntimeTool.SnapY:
                direction = Quaternion.Euler(new Vector3(90 * (_invert ? 1 : -1), 0, 0)) * hit.normal;
                break;
            case RuntimeTool.SnapZ:
                direction = hit.normal * (_invert ? -1 : 1);
                break;
        }

        foreach (var target in ActiveTargets)
        {
            target.SetPositionAndRotation(hit.point, Quaternion.LookRotation(direction));
        }
    }

}
