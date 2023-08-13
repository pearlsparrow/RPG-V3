using UnityEngine;

public class TrackWalls : MonoBehaviour
{
    public LayerMask layerMaskWall;
    public LayerMask layerMaskGround;

    public float distance;
    public float radius;
    public Vector3 offset;

    public bool foundWallTop;
    public bool foundWallMid;
    public bool foundWallLow;
    public bool foundGroundBelow;

    private RaycastHit hitTop;
    private RaycastHit hitMid;
    private RaycastHit hitLow;
    private RaycastHit hitBelow;

    public GameObject raycastSpot;
    public GameObject raycastSpotMid;
    public GameObject raycastSpotLow;
    public GameObject raycastSpotBelow;

    public GameObject objectForHit;


    private float hitDistanceTop = 10f;
    private float hitDistanceMid = 10f;
    private float hitDistanceLow = 10f;

    public float distanceFromWallToClimb;
    public float distanceFromGroundToWalk;

    public bool CanCheckGround { get; set; } = false;
    public float HitDistanceBelow { get; set; } = 10f;

    public RaycastHit HitTop
    {
        get => hitTop;
        set => hitTop = value;
    }

    public RaycastHit HitMid
    {
        get => hitMid;
        set => hitMid = value;
    }


    // Update is called once per frame
    void Update()
    {
        TrackWall();
    }

    private void TrackWall()
    {
        foundWallTop = Physics.Raycast(raycastSpot.transform.position, raycastSpot.transform.forward, out hitTop,
            distance, layerMaskWall);
        hitDistanceTop = foundWallTop ? HitTop.distance : distance;

        foundWallMid = Physics.Raycast(raycastSpotMid.transform.position, raycastSpotMid.transform.forward, out hitMid,
            distance, layerMaskWall);
        if (foundWallMid)
        {
            hitDistanceMid = hitMid.distance;
            objectForHit.transform.position = hitMid.point + hitMid.normal * 0.45f;
        }
        else
        {
            hitDistanceMid = distance;
        }

        foundWallLow = Physics.Raycast(raycastSpotLow.transform.position, raycastSpotLow.transform.forward, out hitLow,
            distance, layerMaskWall);
        hitDistanceLow = foundWallLow ? hitLow.distance : distance;

        foundGroundBelow = Physics.Raycast(raycastSpotBelow.transform.position, -raycastSpotBelow.transform.up,
            out hitBelow, distance, layerMaskGround);
        HitDistanceBelow = foundGroundBelow ? hitBelow.distance : distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.color = foundWallTop ? Color.red : Color.green;

        var position = raycastSpot.transform.position;
        Gizmos.DrawLine(position, position + raycastSpot.transform.forward * hitDistanceTop);

        Gizmos.color = foundWallMid ? Color.red : Color.green;

        var position1 = raycastSpotMid.transform.position;
        Gizmos.DrawLine(position1, position1 + raycastSpotMid.transform.forward * hitDistanceMid);

        Gizmos.color = foundWallLow ? Color.red : Color.green;

        var position2 = raycastSpotLow.transform.position;
        Gizmos.DrawLine(position2, position2 + raycastSpotLow.transform.forward * hitDistanceLow);

        Gizmos.color = foundGroundBelow ? Color.red : Color.green;

        var position3 = raycastSpotBelow.transform.position;
        Gizmos.DrawLine(position3, position3 - raycastSpotBelow.transform.up * HitDistanceBelow);
    }

    public bool CanClimb()
    {
        if (hitDistanceMid <= distanceFromWallToClimb)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanGoBackToGround()
    {
        if (HitDistanceBelow <= distanceFromGroundToWalk)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}