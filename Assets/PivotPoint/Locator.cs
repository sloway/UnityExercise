using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private ILocator locator;

    private void Start()
    {
        //locator = new LocatorMeshFilter(target);
        locator = new LocatorCollider(target);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                var hitObject = hit.collider.gameObject;
                if(hitObject.name == "Ceiling")
                {
                    locator.LocateOnTheCeiling(hit.point);
                }
                else if(hitObject.name == "Floor")
                {
                    locator.LocateOnTheFloor(hit.point);
                }
                else
                {
                    // Do nothing
                }
            }
        }
    }
}


public interface ILocator
{
    void LocateOnTheCeiling(Vector3 position);
    void LocateOnTheFloor(Vector3 position);
}

public abstract class Locator2 : ILocator
{
    protected GameObject target;

    public Locator2(GameObject target)
    {
        this.target = target;
    }

    public abstract void LocateOnTheCeiling(Vector3 position);

    public abstract void LocateOnTheFloor(Vector3 position);    
}

public class LocatorMeshFilter : Locator2
{
    public LocatorMeshFilter(GameObject target) : base(target)
    {

    }

    public override void LocateOnTheCeiling(Vector3 position)
    {
        position.y -= GetOffsetFromPivotToTop(target);
        target.transform.position = position;
    }

    private float GetOffsetFromPivotToTop(GameObject target)
    {
        var bounds = target.GetComponent<MeshFilter>().sharedMesh.bounds;
        return (bounds.center.y - (-bounds.extents.y)) * target.transform.localScale.y;
    }

    public override void LocateOnTheFloor(Vector3 position)
    {
        position.y += GetOffsetFromBottomToPivot(target);
        target.transform.position = position;
    }

    private float GetOffsetFromBottomToPivot(GameObject target)
    {
        var bounds = target.GetComponent<MeshFilter>().sharedMesh.bounds;
        return (bounds.extents.y - bounds.center.y) * target.transform.localScale.y;
    }
}

public class LocatorCollider : Locator2
{
    public LocatorCollider(GameObject target) : base(target)
    {

    }

    public override void LocateOnTheCeiling(Vector3 position)
    {
        Bounds bounds = target.GetComponent<Collider>().bounds;
        position.y += target.transform.position.y - bounds.max.y;
        target.transform.position = position;
    }

    public override void LocateOnTheFloor(Vector3 position)
    {
        Bounds bounds = target.GetComponent<Collider>().bounds;
        position.y += target.transform.position.y - bounds.min.y;
        target.transform.position = position;
    }
}
