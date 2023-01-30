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
        
    }

    private void FixedUpdate()
    {
        if (Editor.Input.GetPointer(0))
        {
            Ray ray = Window.Camera.ScreenPointToRay(Input.mousePosition);

            var hits = Physics.RaycastAll(ray);
            var hit = GetClosestHit(hits, ActiveTargets[0]);

            if (hit.HasValue) 
                Snap(hit.Value, ray.direction);
        }
    }

    private void Snap(RaycastHit hit, Vector3 direction)
    {
        var y = hit.normal;
        var x = Vector3.Cross(y, direction).normalized;

        var lookRotation = Quaternion.LookRotation(x, y);

        RuntimeTool tool = Editor.Tools.Current;
        switch (tool)
        {
            case RuntimeTool.SnapX:
                if (Editor.Tools.InvertSnapping)
                    lookRotation *= Quaternion.Euler(new Vector3(0, 90, 0));
                else
                    lookRotation *= Quaternion.Euler(new Vector3(0, -90, 0));
                break;
            case RuntimeTool.SnapY:
                if (Editor.Tools.InvertSnapping)
                    lookRotation *= Quaternion.Euler(new Vector3(-90, 0, 0));
                else
                    lookRotation *= Quaternion.Euler(new Vector3(90, 0, 0));
                break;
            case RuntimeTool.SnapZ:
                break;
        }

        ActiveTargets[0].SetPositionAndRotation(hit.point, lookRotation);

    }

    private RaycastHit? GetClosestHit(RaycastHit[] hits, Transform selectedObject)
    {
        if (hits == null || hits.Length == 0)
            return null;

        RaycastHit? closestHit = null;
        var closestDistance = float.PositiveInfinity;

        foreach (var hit in hits)
        {
            if (hit.transform.root == selectedObject)
                continue;

            if (hit.distance < closestDistance)
            {
                closestDistance = hit.distance;
                closestHit = hit;
            }
        }

        return closestHit;
    }
}
