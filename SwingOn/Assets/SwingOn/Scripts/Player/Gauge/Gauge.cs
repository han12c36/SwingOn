using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Image targetUI;
    public Canvas canvas = null;
    public RectTransform targetRect = null;

    public Vector3 target;

    void Start()
    {
        target = CameraManager.Instance.targetCamera.ScreenToViewportPoint(targetUI.rectTransform.position);
        Debug.Log(target.x + "" + target.y + " " + target.z);
        //screenWidth = Screen.width;
        //screenHeight = Screen.height;
        //screenPos = targetUI.rectTransform;
        //Debug.Log(screenPos.rect.x + " " + screenPos.rect.y);

        //Vector3 vec = CameraManager.Instance.targetCamera.
        //    .ScreenToWorldPoint(targetUI.transform.position);
        //vec = new Vector3(vec.x, vec.y, vec.z);
        //targetPos = vec;


    }

    void Update()
    {
        //if (Input.GetMouseButton(0))  // 마우스가 클릭 되면
        //{
            Vector3 mos = Input.mousePosition;
            Debug.Log(mos);
            //mos.z = GetComponent<Camera>().farClipPlane; //카메라가 보는 방향과 시야를 가져옴
            mos.z = CameraManager.Instance.targetCamera.farClipPlane; //카메라가 보는 방향과 시야를 가져옴
            Debug.Log(mos.z);
            Vector3 dir = CameraManager.Instance.targetCamera.ScreenToWorldPoint(mos);
            Debug.Log(dir);
            
            
            //월드의 좌표를 클릭했을 때 화면에 자신이 보고있는 화면에 맞춰 좌표를 바꿔준다.

            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, dir, out hit, mos.z))
            //{
                //target.position = hit.point; // 타겟을 레이캐스트가 충돌된 곳으로 옮긴다.
            //}
        //}
    }
    void LateUpdate()
    {
    }
}
