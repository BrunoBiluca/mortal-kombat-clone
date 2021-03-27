using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanPoint : MonoBehaviour {

    [SerializeField] private Transform referenceMeanPoint;
    [SerializeField] private Transform referenceA; 
    [SerializeField] private Transform referenceB;

    [SerializeField] private bool moveOnX;
    [SerializeField] private bool moveOnY;
    [SerializeField] private bool moveOnZ;

    public Vector3 Distance { get; private set; }

    void Update() {
        Distance = (referenceA.position - referenceB.position) / 2;

        var mostLeftReference = referenceA.position.x < referenceB.position.x
            ? referenceA
            : referenceB;

        transform.position = new Vector3(
            moveOnX ? mostLeftReference.position.x + Mathf.Abs(Distance.x) : transform.position.x,
            moveOnY ? mostLeftReference.position.y + Mathf.Abs(Distance.y) : transform.position.y,
            moveOnZ ? mostLeftReference.position.z + Mathf.Abs(Distance.z) : transform.position.z
        );

        if(referenceMeanPoint != null) {
            referenceMeanPoint.position = new Vector3(
                moveOnX 
                    ? mostLeftReference.position.x + Mathf.Abs(Distance.x) 
                    : referenceMeanPoint.position.x,
                moveOnY 
                    ? mostLeftReference.position.y + Mathf.Abs(Distance.y) : 
                    referenceMeanPoint.position.y,
                moveOnZ 
                    ? mostLeftReference.position.z + Mathf.Abs(Distance.z) 
                    : referenceMeanPoint.position.z
            );
        }
    }
}
