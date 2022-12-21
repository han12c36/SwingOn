using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Manager<CameraManager>
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float turnSpeed = 4.0f;
    [SerializeField]
    private float cameraFollowSpeed = 0.1f;
    [SerializeField]
    private Vector3 cameraFollowVelocity = Vector3.zero;
    [SerializeField]
    private LayerMask collisionLayers;

    private void Awake()
    {
        if (GameManager.Instance.SceneCtrl.CurSceneIndex == SceneIndex.InGame)
        {
            target = GameObject.Find("Player");
        }

    }
    private void Start()
    {
    }
    private void Update()
    {
        //ZoomCamera();
    }
    private void LateUpdate()
    {
        FollowTarget();
        RotateCamera();
        //HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        //임시로 스무스 껏음

        //SmoothDamp(현재위치,목표위치,현재 카메라 속도,목표위치까지 도달할 시간)
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, target.transform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        //transform.position = targetPosition;
        transform.position = target.transform.position;

        //transform.LookAt(targetPosition);
    }

    private void RotateCamera()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
            float yRotate = transform.eulerAngles.y + yRotateSize;
            transform.eulerAngles = new Vector3(0, yRotate, 0);

            //target.transform.eulerAngles = new Vector3(0, yRotate, 0);
        }
    }



    //private void HandleCameraCollisions()
    //{
    //    float targetPosition = defaultPosition;
    //    RaycastHit hit;
    //    Vector3 direction = cameraTransform.position - cameraPivot.position;
    //    direction.Normalize();
    //
    //    if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
    //    {
    //        float distance = Vector3.Distance(cameraPivot.position, hit.point);
    //        targetPosition = -(distance - cameraCollisionOffset);
    //    }
    //
    //    if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
    //    {
    //        targetPosition = targetPosition - minimumCollisionOffset;
    //    }
    //
    //    cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
    //    cameraTransform.localPosition = cameraVectorPosition;
    //}
    //private void ZoomCamera()
    //{
    //    if (Inventory.inventoryActivated) return;
    //    defaultPosition += Input.GetAxis("Mouse ScrollWheel") * ZoomSensitivity;
    //    if (defaultPosition > -1.0f) defaultPosition = -1.0f;
    //    if (defaultPosition < -16.0f) defaultPosition = -16.0f;
    //}
}
