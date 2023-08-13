using System;
using FMODUnity;
using UnityEngine;


public class FootStep : MonoBehaviour
{
 
    
    public LayerMask ground;
    private MainPlayer mainPlayer;

    [SerializeField] [Range(0, 1.5f)] private float speed;

    private bool grounded;
    private bool played;

    private void Awake()
    {
        mainPlayer = GetComponent<MainPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 0.1f, ground))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            
        }
    }

    public void FootSteps(AnimationEvent e)
    {
        if (e.animatorClipInfo.weight > 0.5)
        {
            speed = mainPlayer.CurrentAccelaration;
            string currentTerrain = AudioPool.inst.terainValue.ToString();
            AudioPlayer.PlayOneShotWithParameters(AudioPool.inst.footSteps, transform.position, "GroundValue", currentTerrain,("Speed", speed));
        }
    }
}