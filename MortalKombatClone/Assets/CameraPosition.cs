using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CameraPosition : MonoBehaviour {

    private Animator animatorController;
    private MeanPoint meanPoint;

    void Start() {
        var mainCamera = Camera.main;

        animatorController = mainCamera.GetComponent<Animator>();
        meanPoint = mainCamera.GetComponent<MeanPoint>();
    }

    void Update() {
        var xDistance = Mathf.Abs(meanPoint.Distance.x);

        if(xDistance < 15) 
            animatorController.SetFloat("distance", 0);
        else if(xDistance >= 15) 
            animatorController.SetFloat("distance", 1);
    }
}
