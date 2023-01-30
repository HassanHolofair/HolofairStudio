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
            Snap(hit);

            /*foreach (var hit in hits)
            {
                if (hit.transform.root != ActiveTargets[0])
                {
                    Control(hit);
                    break;
                }
            }*/
        }
    }

    private void Snap(RaycastHit? hit)
    {
        if (!hit.HasValue)
            return;

        Vector3 direction = Vector3.zero;
        RuntimeTool tool = Editor.Tools.Current;
        switch (tool)
        {
            case RuntimeTool.SnapX:
                if (Editor.Tools.InvertSnapping)
                    direction = Quaternion.Euler(new Vector3(0, 90, 0)) * hit.Value.normal;
                else
                    direction = Quaternion.Euler(new Vector3(0, -90, 0)) * hit.Value.normal;
                break;
            case RuntimeTool.SnapY:
                if (Editor.Tools.InvertSnapping)
                    direction = Quaternion.Euler(new Vector3(-90, 0, 0)) * hit.Value.normal;
                else
                    direction = Quaternion.Euler(new Vector3(90, 0, 0)) * hit.Value.normal;
                break;
            case RuntimeTool.SnapZ:
                direction = hit.Value.normal * (Editor.Tools.InvertSnapping ? -1 : 1);
                break;
        }

        direction = Quaternion.Euler(new Vector3(90, 0, 0)) * hit.Value.normal;
        ActiveTargets[0].SetPositionAndRotation(hit.Value.point, Quaternion.LookRotation(direction));

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
