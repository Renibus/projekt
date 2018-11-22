using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class tiling : MonoBehaviour {

    public int offsetX = 2;

    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    public bool reverseScale = false; //kiedy obiekt nie podlega tileingowi

    private float spriteWidth = 0f; //dlugosc emelemtu
    private Camera cam;
    private Transform myTransform;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start () {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        //co potrzebuje "koleszkow"
        if (hasALeftBuddy == false || hasARightBuddy == false)
        {
            //polowa widoku kamery (polowa szerokosci)
            float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;

            //obliczanie pozycji x gdzie kamera widzi koniec sprite'a
            float edgeVisablePosRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtent;
            float edgeVisablePosLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtent;

            //sprawdzanie czy czas na nowego koleszke
            if (cam.transform.position.x >= edgeVisablePosRight-offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisablePosLeft+offsetX && hasALeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    void MakeNewBuddy(int rightOrLeft)
    {
        //obliczanie pozycki nowych spriteow "koleszkow"
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        //tworzenie koleszki i chowanie go w zmiennej
        Transform newBuddy = Instantiate(myTransform,newPosition,myTransform.rotation) as Transform; 

        //obracanie obiektu
        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;

        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<tiling>().hasARightBuddy = true;
        }
    }
}
