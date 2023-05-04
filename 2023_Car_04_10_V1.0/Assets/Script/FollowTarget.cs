using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("ī�޶� ��ġ,����,FOV----------")]
    [SerializeField] Vector3 postion = new Vector3(0, 3.6f, -7.8f);
    [SerializeField] Vector3 rotation = new Vector3(14, 0, 0);
    [SerializeField] [Range(10, 100)] float fov = 30f;

    [Header("ī�޶� �̵� �� ȸ���ӵ�-------------")]
    [SerializeField] float movespeed = 10f;
    [SerializeField] float trunspeed = 10f;

    Transform target;   //�������
    Transform cam;      //ī�޶�
    Transform pivot;    //ī�޶� �̵� �� ȸ����
    Transform pivotRot;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Car").transform;
        InitCamera();
    }

    void InitCamera()
    {
        cam = Camera.main.transform;
        cam.GetComponent<Camera>().fieldOfView = fov;  //fieldOfView = ���� �ܾƿ� ����,������Ʈ���� fieldOfView�� �����´�

        pivot = new GameObject("Pivot").transform;     
        pivot.position = target.position;

        pivotRot = new GameObject("PivotRot").transform;
        pivotRot.position = target.position;
        pivotRot.parent = pivot;           //Pivot �� �ڽ����� PivRot

        cam.parent = pivotRot;             //PivotRot �� �ڽ����� ī�޶�
        cam.localPosition = postion;
        cam.localEulerAngles = rotation;
    }

    private void LateUpdate()
    {
       
        Vector3 pos = target.position;
        Quaternion rot = target.rotation;

        pivot.position = Vector3.Lerp(pivot.position, pos, movespeed * Time.deltaTime);     //Lerp ���� ��ġ���� target�� ��ġ�� �̵�
        pivot.rotation = Quaternion.Lerp(pivot.rotation, rot, trunspeed * Time.deltaTime);
      
    }
    void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel") * 20;
        fov = Mathf.Clamp(fov - zoom, 10, 100);               //Clamp �� ���� fov -zoom �� ������ 10�� 100���̷� ����
        cam.GetComponent<Camera>().fieldOfView = fov;

        if (!Input.GetMouseButton(1))
        {
            return;
        }
        float x = Input.GetAxis("Mouse Y") * 2;
        float y = Input.GetAxis("Mouse X") * 2;

        Vector3 ang = pivotRot.localEulerAngles + new Vector3(x, y, 0);

        if(ang.x > 180)
        {
            ang.x = ang.x - 360;
        }

        ang.x = Mathf.Clamp(ang.x, -24, 20);

        pivotRot.localEulerAngles = ang;
    } 
}
