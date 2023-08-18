using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mapHandler : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    public Camera minicam;
    public GameObject soldier;
    public Camera viewcam;



    
    private void Start()
    {

    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
           

            if (EventSystem.current.IsPointerOverGameObject())
            {
                //instantiate cude in minimap
                var minicamPos = toMinicamPos(toMinimapPos(Input.mousePosition));
                var ray = minicam.ScreenPointToRay(minicamPos);
                rayToCude(ray, Color.yellow);
               if(rayTohit(ray,out var hit))
                {
                    cameraToClick(hit.point);
                }
               
            }
            else
            {
                Debug.LogWarning(Input.mousePosition);
                // instantiate cude in view 
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                rayToCude(ray,Color.red);

            }
            

        }
        if (Input.GetMouseButtonUp(0))
        {
            cam.GetComponent<Camera>().depth = 1;
            viewcam.GetComponent<Camera>().depth = 0;
        }


    }
    void cameraToClick(Vector3 pos)
    {
        cam.GetComponent<Camera>().depth = 0;
        viewcam.GetComponent<Camera>().depth = 1;
        var offset=cam.GetComponent<CameraFollow>().offset;
        viewcam.transform.position = pos+offset;
    }

    bool rayTohit(Ray ray ,out RaycastHit hit)
    {
         if(Physics.Raycast(ray,out hit))
        {
            return true;
        }
        return false;
    } 
    void rayToCude(Ray ray,Color cudeColor )
    {
        if (Physics.Raycast(ray, out var hit))
        {
            cudeToInstantiate(hit.point, cudeColor);
        }
        else
        {
            Debug.LogError("invalid ray");
        }
    }

    private void cudeToInstantiate(Vector3 pos,Color color)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = pos;
        obj.GetComponent<Renderer>().material.color = color;
    }
    
    Vector2 toMinimapPos(Vector2 campos)
    {
        return (new Vector2(campos.x, campos.y + transform.GetComponent<RectTransform>().rect.height - cam.pixelHeight));
    }

    Vector2 getMinicamScale()
    {
        return new Vector2(minicam.pixelWidth / transform.GetComponent<RectTransform>().rect.width, minicam.pixelHeight / transform.GetComponent<RectTransform>().rect.height);
    }
   
    Vector2 toMinicamPos(Vector2 pos)
    {
        return Vector2.Scale(pos, getMinicamScale());
    }
  

}

            