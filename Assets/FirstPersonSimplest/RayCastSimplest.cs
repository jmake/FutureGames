using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastSimplest : MonoBehaviour
{
//  [SerializeField]
  private Camera mainCamera;
  private Color originalColor; 

  private Transform _selected; 

  void Start()
  {
    print( "[RayCastSimplest] '"+ Camera.main +"' ");
    mainCamera = Camera.main;  
  }

  void Update()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    Transform cameraTransform = Camera.main.transform;

    RaycastHit HitInfo;

    bool hitting1 = Physics.Raycast(cameraTransform.position,cameraTransform.forward, out HitInfo, 100.0f);
    bool hitting2 = Physics.Raycast(ray, 100);

    if(hitting1)       
      Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.red);
    else
      Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.blue);

    if(_selected != null)
    {
      _selected.GetComponent<Renderer>().material.color = originalColor;
    }


    if(hitting1)
    {
      var selection = HitInfo.transform; 
      var selectionRenderer = selection.GetComponent<Renderer>(); 

      if(selectionRenderer != null)
      { 
        originalColor = selectionRenderer.material.color;

        print( "[RayCastSimplest.Update] '" +  originalColor +"' ");
        selection.GetComponent<Renderer>().material.color = Color.yellow;

        _selected = selection;
      } 
    }

/*
    var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit; 
    bool touching = Physics.Raycast(ray, out hit);  

    Debug.DrawRay(ray.direction, hit.point, Color.yellow);  

    if(touching)
    {
      print( "[RayCastSimplest.Update] ");
    }
*/
 
  }
}
