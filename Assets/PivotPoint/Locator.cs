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
        var lowerCenter = GetLowerCenter();
        target.transform.position = position - lowerCenter;
    }

    private Vector3 GetLowerCenter()
    {
        var bounds = target.GetComponent<MeshFilter>().mesh.bounds;
        var localScale = target.transform.localScale;
        var temp = (bounds.center - new Vector3(0, bounds.extents.y, 0));
        temp.y *= localScale.y;
        return temp;
    }
}
