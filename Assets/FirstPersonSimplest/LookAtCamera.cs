using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
  private Vector3 newRotation;  
  //private Camera mainCamera; 

  [SerializeField]
  GameObject mainCamera;


  void Start()
  {
    print( "[LookAtCamera] 'Start' ");

    Camera[] allCameras = FindObjectsOfType<Camera>();
  }


  void Update()
  {
    newRotation = Quaternion.LookRotation(this.transform.position - mainCamera.transform.position).eulerAngles; 
    this.transform.rotation = Quaternion.Euler(newRotation);  

    //print( "[LookAtCamera.Update] '"+ this.transform.position +"' '"+ mainCamera.transform.position +"'  ");
    //print( "[LookAtCamera.Update] '"+  newRotation  +"' ");

  }
}
