using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform testTarget;
    public Camera cam;
    public float speed;
    public List<Transform> cameraPos = new List<Transform>();
    private int currIndex = 0;
    private bool isMoving = false;
    private float _t;


    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RotateCamera());


        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (isMoving) return;

            int nextIndex;
            if (currIndex == 0)
            {
                nextIndex = cameraPos.Count - 1;
            }
            else
            {
                nextIndex = currIndex - 1;
            }

            StartCoroutine(MoveCamera(nextIndex));

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (isMoving) return;

            int nextIndex;
            if (currIndex == (cameraPos.Count -1))
            {
                nextIndex = 0;
            }
            else
            {
                nextIndex = currIndex + 1;
            }

            StartCoroutine(MoveCamera(nextIndex));
        }
    }


    IEnumerator MoveCamera(int nextIndex)
    {
        currIndex = nextIndex;
        isMoving = true;
        Vector3 nextPos = cameraPos[nextIndex].position;
     
        while (cam.transform.position != nextPos)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, nextPos, Time.deltaTime * speed);

            yield return null;
        }

        isMoving = false;

    }

    IEnumerator RotateCamera()
    {

        float lerpTime = 5f;
        float currentLerpTime = 0f;

        Vector3 relativePos = testTarget.position - cam.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion current = cam.transform.localRotation;

        while (lerpTime > 0)
        {
            lerpTime -= Time.deltaTime;
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float t = currentLerpTime / lerpTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f); //Calculates the speed somehow

            cam.transform.localRotation = Quaternion.Slerp(current, rotation, t);

            yield return null;

            relativePos = testTarget.position - cam.transform.position;
            rotation = Quaternion.LookRotation(relativePos);
        }

    }


}
