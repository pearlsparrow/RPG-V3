using FMOD;
using FMODUnity;
using UnityEngine;

public class PlayAudioTest : MonoBehaviour
{
    [SerializeField] private EventReference _name;


    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer.PlayOneShotWithParameters(_name, transform);
    }

    // Update is called once per frame
    void Update()
    {
    }
}