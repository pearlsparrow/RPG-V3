using FMODUnity;
using UnityEngine;


public class FootStep : MonoBehaviour
{
    [SerializeField] private EventReference _name;
    public LayerMask ground;
    private MainPlayer mainPlayer;

    [SerializeField] [Range(0, 1.5f)] private float speed;

    private bool grounded;
    private bool played;
    private string labelValue = "Water";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.1f, ground))
        {
            Debug.Log(labelValue);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            AudioPlayer.PlayOneShotWithParameters(_name, transform.position, "GroundValue", labelValue,
                ("Speed", speed));
        }
    }
}