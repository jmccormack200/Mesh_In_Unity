﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeDataDisplay : MonoBehaviour {
    //public GameObject TextCamera;
    //Use this for initialization
    public bool hasCompass = true;
	public bool hasTherm = true;
    //public bool hasMagnetometer = true;
    //public bool hasBarometer = true;
    //public bool hasGPS = true;

    //Temporary Values for creating debugging
    //Should be removed and made into a separate Test
    //Data set ASAP.
    public double compass;
	public double temp;

    //Materials for Dictionary
    //Need to manually get Files 
    //From Inspector 
    public Material CompassTex;
	public Material ThermTex;

    //Material Dictionary
    public Dictionary<string, Material> Logos;


    public GameObject LogoBox;
	public Material material;
    private Node node;
	public GameObject CurrentNodeObject;

    // cycle will count upwards until reaching the last
    // sensor and then reset to 0. 
    private int cycle;

    private SmartTextMesh smartTextMesh;

    //Set Delay of Coroutine
    public float delay = 2.0f;

    //Materials
    public Material ActivatedNode;
    public Material DeActivatedNode;

    void Start () {

        smartTextMesh = GetComponentInChildren<SmartTextMesh>();

        //node = FindObjectOfType<Node>();
        //compass = node.pointToSensor().compSensor.orientation;

        //Build Dictionary of Textures
        Logos = new Dictionary<string, Material>();
        Logos.Add ("compass", CompassTex);
		Logos.Add ("thermometer", ThermTex);
        cycle = 0;
        StartCoroutine("DisplayFeedLoop");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
       
    }

    IEnumerator DisplayFeedLoop()
    {
        while (true)
        {
            if (CurrentNodeObject)
            {
                if (hasCompass == true)
                {
                    smartTextMesh.UnwrappedText = "Compass Heading=\n" + compass.ToString() + "\nDegrees";
                    smartTextMesh.NeedsLayout = true;
                    Logos.TryGetValue("compass", out material);
                    LogoBox.GetComponent<MeshRenderer>().material = material;
                    yield return new WaitForSeconds(delay);
                }

                if (hasTherm == true)
                {
                    smartTextMesh.UnwrappedText = "Temp =\n" + temp.ToString() + "\nDegrees C";
                    smartTextMesh.NeedsLayout = true;
                    Logos.TryGetValue("thermometer", out material);
                    LogoBox.GetComponent<MeshRenderer>().material = material;
                    yield return new WaitForSeconds(delay);
                }
            }
            yield return new WaitForSeconds(delay);
        }
    }

	public void setGameObject(string newNodeName){
        try
        {
            CurrentNodeObject.GetComponent<MeshRenderer>().material = DeActivatedNode;
        }
        catch
        {

        }

        CurrentNodeObject = GameObject.Find(newNodeName);
        CurrentNodeObject.GetComponent<MeshRenderer>().material = ActivatedNode;
        //DummyData 
        compass = CurrentNodeObject.GetComponent<DummyData>().compass;
        temp = CurrentNodeObject.GetComponent<DummyData>().temperature;
	}
}
