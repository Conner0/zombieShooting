using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{

    public Transform panelImg;
    public Camera cam;
    public Transform canvas;

    public Transform markImg;

    private const float width = 3f;
    private const int unit = 1000;

    public int healthValue = 5000;
    public int currentHealthValue = 10000;

    List<Transform> markList;
    List<Transform> currentMarkList;

    List<Vector3> posesList;
    List<Vector3> currentPosesList;

    public int value = 10000;





    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning(panelImg.GetComponent<RectTransform>().anchoredPosition3D);

        initMarkToPos();
        
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
         
            MarkNumChange();
        }
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        healthLoolAt(panelImg, cam);

    }    
    
    void MarkNumChange()
    {
        if (currentHealthValue > healthValue)
        {
            instantiateCurrentMark();
            //移动旧的mark去到新的pos
            for(int i = 0; i < markList.Count; i++)
            {
                markList[i].GetComponent<RectTransform>().anchoredPosition3D = panelImg.GetComponent<RectTransform>().anchoredPosition3D+currentPosesList[i];
            }
            for(int i = markList.Count; i< currentMarkList.Count; i++)
            {
                currentMarkList[i].GetComponent<RectTransform>().anchoredPosition3D = panelImg.GetComponent<RectTransform>().anchoredPosition3D+currentPosesList[i];

            }
        }
        else
        {
            initMarkToPos();
        }
    }
    void instantiateCurrentMark()
    {
        setCurrentPoses(currentHealthValue);
        currentMarkList = instantiateMark(currentPosesList.Count);
        if (currentMarkList.Count != currentPosesList.Count)
        {
            Debug.LogWarning("current mark number error");
        }
        for (int i = 0; i < currentPosesList.Count; i++)
        {

            currentMarkList[i].GetComponent<RectTransform>().anchoredPosition3D = panelImg.GetComponent<RectTransform>().anchoredPosition3D + currentPosesList[i];

        }
    }
    void initMarkToPos()
    {
        setPoses(healthValue);
        markList= instantiateMark(posesList.Count);
        if (markList.Count != posesList.Count)
        {
            Debug.LogWarning("mark number error");
        }
        for(int i = 0; i < markList.Count;i++)
        {
            
            markList[i].GetComponent<RectTransform>().anchoredPosition3D = panelImg.GetComponent<RectTransform>().anchoredPosition3D + posesList[i];

        }
        
    }
     void setCurrentPoses(int currentHealth)
    {
       currentPosesList= calculateMarkPoses(currentHealth);
    }
    void setPoses(int healthValue)
    {
        posesList = calculateMarkPoses(healthValue);
    }
    List<Transform> instantiateMark(int count)
    {
        var list= new List<Transform>();
        for(int i=0; i < count; i++)
        {
            Transform mark = Instantiate(markImg);
            mark.transform.SetParent(panelImg);
            mark.transform.localRotation = panelImg.localRotation;
            mark.transform.localScale = panelImg.localScale;
            list.Add(mark);
        }
        
        return list;

    }

    void healthLoolAt(Transform panelImg, Camera cam)
    {
        canvas.LookAt(canvas.position + cam.transform.forward);
    }
    

    List<Vector3> calculateMarkPoses(int health)
    {
        int count = health / unit;
        float interval = width/count;

        var list = new List<Vector3>();
        var start = new Vector3(0, 0, 0);
        for(int i = 0; i < count-1; i++)
        {
            start = start + new Vector3(interval, 0, 0);
            list.Add(start );
            Debug.LogWarning(start);
            
        }
        return list;
    }


   
}