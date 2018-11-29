using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour {

    public Transform[] backgrounds; //lista wszystkich spriteow do paralaxy
    private float[] parallaxScales; //proporcje ruchow obiekotw do ich odleglosci
    public float smoothing = 1f; //wygladzanie parallaxy (zawsze powyzej 0!!!!)

    private Transform cam; //odwolanie do kamery
    private Vector3 previousCamPos; //pozycja kamery w poprzedniej klatce

    //odpala sie przed startem
    private void Awake()
    {
        //odwolanie do kamery
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        //poprzednia klatka
        previousCamPos = cam.position;

        //ustawieanie skali
        parallaxScales=new float[backgrounds.Length];
        for (int i=0; i< backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		//dla kazdego tla
        for (int i=0; i < backgrounds.Length; i++)
        {
            //parallaxa jest przeciwna ruchowi kamery
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //ustawianie nowej pozycji kamery
        previousCamPos = cam.position;
	}
}
