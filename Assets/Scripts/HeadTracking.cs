using UnityEngine;

public class HeadTracking : MonoBehaviour
{
    public Transform tracking;
    private Vector3 targetPos;
    public Transform ResetPos;
    public Transform playerLookAt;
    Vector3 startingPos;
    public Camera cam;
    public float maxAngle;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = tracking.position;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Vector3.Angle(transform.forward, cam.transform.forward);
        if (angle < maxAngle)
        {
            targetPos = playerLookAt.position + cam.transform.forward;
            tracking.position = Vector3.Lerp(tracking.position, targetPos, Time.deltaTime * 10f);
        }
        else
        {
            targetPos = startingPos;
            tracking.position = Vector3.Lerp(tracking.position, ResetPos.position, Time.deltaTime * 3f);
        }
    }
}