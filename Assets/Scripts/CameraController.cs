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


    // Update is called once per frame
    void Update()
    {
        Vector3 lTargetDir = testTarget.position - cam.transform.position;
        lTargetDir.y = 0.0f;
        cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * speed);

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


}
