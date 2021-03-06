﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARController : MonoBehaviour
{
    //We will fill this list with the planes that ARCore detected in the current frame
    private List<DetectedPlane> m_NewTrackedPlanes = new List<DetectedPlane>();

    public GameObject GridPrefab;

    public GameObject Portal;

    public GameObject ARCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Checking ARCore session status
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        //The following function will fill m_NewTrackedPlanes with the planes that ARCore detected in 
        //the current frame
        Session.GetTrackables<DetectedPlane>(m_NewTrackedPlanes, TrackableQueryFilter.New);

        //To instantiate a grid for each DetectedPlane in m_NewTrackedPlanes
        for (int i = 0; i < m_NewTrackedPlanes.Count; i++)
        {
            GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);

            //This function will set the position of grid and modify the vertices of the attached mesh
            grid.GetComponent<GridVisualiser>().Initialize(m_NewTrackedPlanes[i]);
        }

        //Check wthether the user has touched the screen 
        Touch touch;

        if(Input.touchCount<1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        //Let's now check wthether the use has touched any of the Detected planes
        TrackableHit hit;

        if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit)) ;
        {
            //Let's now place the portal on top of the tracked plane that we touched

            //Enable the portal
            Portal.SetActive(true);

            //Creating a new Anchor
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            //Set the position of the portal to be the same as the hit position
            Portal.transform.position = hit.Pose.position;
            Portal.transform.rotation = hit.Pose.rotation;

            //We want the Portal to face the Camera
            Vector3 cameraPosition = ARCamera.transform.position;

            //The Portal should only rotate along the Y-axis
            cameraPosition.y = hit.Pose.position.y;

            //Rotate the Portal to face the Camera
            Portal.transform.LookAt(cameraPosition, Portal.transform.up);

            //ArCore will keep understanding the world and update the anchors accordingly hence we need to attach our portal to the anchor
            Portal.transform.parent = anchor.transform;


        
        }


    }
}
