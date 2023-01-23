using Battlehub.RTCommon;
using Battlehub.RTHandles;
using UnityEngine;


public class SnapHandle : BaseHandle
{
    [SerializeField] private LayerMask _modelsLayer;
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
                return;
        }
        
        Vector3 direction = Vector3.zero;
        RuntimeTool tool = Editor.Tools.Current;
        switch (tool)
        {
            case RuntimeTool.SnapX:
                if (Editor.Tools.InvertSnapping) 
                    direction = Quaternion.Euler(new Vector3(0, 90, 0)) * hit.normal;
                else
                    direction = Quaternion.Euler(new Vector3(0, -90, 0)) * hit.normal;
                break;
            case RuntimeTool.SnapY:
                if(Editor.Tools.InvertSnapping)
                    direction = Quaternion.Euler(new Vector3(-90, 0, 0)) * hit.normal;
                else
                    direction = Quaternion.Euler(new Vector3(90, 0, 0)) * hit.normal;
                break;
            case RuntimeTool.SnapZ:
                direction = hit.normal * (Editor.Tools.InvertSnapping ? -1 : 1);
                break;
        }

        foreach (var target in ActiveTargets)
        {
            target.SetPositionAndRotation(hit.point, Quaternion.LookRotation(direction));
        }
    }

}
