using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
        
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
                    LocateOnTheCeiling(hit.point);
                }
                else if(hitObject.name == "Floor")
                {
                    LocateOnTheFloor(hit.point);
                }
                else
                {
                    // Do nothing
                }
            }
        }
    }

    private void LocateOnTheFloor(Vector3 position)
    {
        position.y += GetOffsetFromBottomToPivot(); 
        target.transform.position = position;
    }

    private float GetOffsetFromBottomToPivot()
    {
        var bounds = target.GetComponent<MeshFilter>().sharedMesh.bounds;        
        return (bounds.extents.y - bounds.center.y) * target.transform.localScale.y;
    }

    private void LocateOnTheCeiling(Vector3 position)
    {
        position.y -= GetOffsetFromPivotToTop();
        target.transform.position = position;
    }

    private float GetOffsetFromPivotToTop()
    {
        var bounds = target.GetComponent<MeshFilter>().sharedMesh.bounds;
        return (bounds.center.y - (-bounds.extents.y)) * target.transform.localScale.y;
    }
}
