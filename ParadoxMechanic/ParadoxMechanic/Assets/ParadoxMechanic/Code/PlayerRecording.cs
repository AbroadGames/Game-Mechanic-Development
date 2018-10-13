﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerRecording : MonoBehaviour
{

    //VisualEffects
    [SerializeField]private PostProcessingProfile profileNormal;
    [SerializeField]private PostProcessingProfile profileRecording;
    [SerializeField] private PostProcessingBehaviour ppb;


    //Varribles
    [SerializeField] private List<Vector3> recordedPoints;
    [SerializeField] private List<Quaternion> recordedRotation;
    private Vector3 point;
    private Quaternion rotation;

    [SerializeField] private float recordAccuracy;
    [SerializeField] private float maxPoints;
    [SerializeField] private float recordTime;

    private bool isRecording = false;
    private ParadoxCreator paradoxCreator;
      

    public List<Vector3> GetPoints
    {
        get
        {
            return recordedPoints;
        }
    }

    public List<Quaternion> GetRotationPoints
    {
        get
        {
            return recordedRotation;
        }
    }


    // Use this for initialization
    void Start () 
    {
        paradoxCreator = GetComponent<ParadoxCreator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
    

		if(Input.GetKeyDown(KeyCode.R) && isRecording == false) 
        { 
            StartCoroutine("RecordPoints");
            recordedPoints.Clear();
            recordedRotation.Clear();
            isRecording = true;
            Invoke("RecordEnd", recordTime);

            ppb.profile = profileRecording;
        }
             
    }

    void RecordEnd()
    {
        StopCoroutine("RecordPoints");
        
        
        isRecording = false;
        transform.position = recordedPoints[0];
        
        paradoxCreator.CreateClone();

        ppb.profile = profileNormal;
    }
    


    IEnumerator RecordPoints()
    {
        for(; ; )
        {
            
            //if(recordedPoints.Count >= maxPoints)
            //{
            //    recordedPoints.RemoveAt(0);
            //}

            point = transform.position;

            
            rotation = transform.rotation;
             
            recordedPoints.Add(point);
            recordedRotation.Add(rotation);

            Debug.DrawRay(point, transform.up, Color.red, 15);

            yield return new WaitForSeconds(recordAccuracy);
   

        }
        
    }

    
   
}
