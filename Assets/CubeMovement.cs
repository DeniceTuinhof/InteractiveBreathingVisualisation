using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    //public int _band;
    //public float _startScale, _scaleMultiplier;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //  transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._freqBand[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
        transform.Translate(Time.deltaTime * 0F, 0F, AudioPeer._cubeMovement * 1F);
    }
}