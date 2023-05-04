using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour
{
    AudioSource sndEngine;

    // Start is called before the first frame update
    void Start()
    {
        sndEngine = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float vert = Mathf.Abs(Input.GetAxis("Vertical"));
        float horz = Mathf.Abs(Input.GetAxis("Horizontal"));

        float pitch = Mathf.Max(vert, horz);

        sndEngine.pitch = pitch + 1;
        sndEngine.volume = sndEngine.pitch * 0.6f;
    }
}
