using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float baseWidth = 1080;
    public float baseHeight = 2280;
    public float baseOrthographicSize = 5;


    void Awake() {
        float cameraAspect = GetComponent<UnityEngine.Camera>().aspect;
        //Debug.Log("aspect = " + cameraAspect);
        cameraAspect = this.baseWidth / this.baseHeight;
        //Debug.Log("aspect2 = " + cameraAspect);

        float newOrthographicSize = (float)Screen.height / 
            (float)Screen.width * this.baseWidth / this.baseHeight * 
            this.baseOrthographicSize;
        GetComponent<UnityEngine.Camera>().orthographicSize = Mathf.Max(newOrthographicSize , this.baseOrthographicSize);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
