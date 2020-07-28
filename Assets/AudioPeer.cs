using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;

    // Microphone input 
 
    public AudioClip _audioClip;
    public bool _useMicrophone;
    public string _selectedDevice;
    public AudioMixerGroup _mixerGroupMicrophone, _mixerGroupMaster;

    // spectrum data and amount of samples 
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[8];

    // Cube movement
    public static int _cubeMovement = new int();

    public string _breathingState;
    public string _calibrationBreathingState; 
    public static int _inCount = new int();
    public static int _outCount = new int();
    public static int _pauseCount = new int();
    public static int _thresholdCount = new int();
    public static int _startCalibrationTime = new int();
    public bool _breathingLock;
    public bool _calibrationFinished;
    public static double _thresholdMax = new double();
    public static double _thresholdMin = new double();
    public static double _maxFrequency = new double();

    void Start()
    {
        _inCount = 0;
        _outCount = 0;
        _pauseCount = 0;
        _thresholdCount = 0; 
        _breathingLock = false;
        _calibrationFinished = false; 
        _thresholdMax = 0.0;
        _thresholdMin = 0.0;
        _maxFrequency = 0;
        _startCalibrationTime = CurrentTime(); 

        _audioSource = GetComponent<AudioSource>();

        if (_useMicrophone)
        {
            if (Microphone.devices.Length > 0)
            {
                _selectedDevice = Microphone.devices[0].ToString();
                _audioSource.outputAudioMixerGroup = _mixerGroupMicrophone;
                _audioSource.clip = Microphone.Start(_selectedDevice, true, 1, AudioSettings.outputSampleRate);
                //_audioSource.clip = Microphone.Start(_selectedDevice, true, 1, AudioSettings.outputSampleRate);
            }
            else
            {
                _useMicrophone = false; 
            }
        }
        if (!_useMicrophone)
        {
            _audioSource.outputAudioMixerGroup = _mixerGroupMaster; 
            _audioSource.clip = _audioClip;
        }

        _audioSource.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource ();
        if (_calibrationFinished == false)
        {
            GetCallibrationData();
        }
        else
        {
            MakeFrequencyBands(); 
        }
    }


    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    public static int CurrentTime()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;

        return currentEpochTime;
    }

    void GetCallibrationData()
    {

        int calibrationTime = CurrentTime() - _startCalibrationTime;

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;

            _freqBand[i] = average * 10;


        }
        if ((calibrationTime > 0 && calibrationTime < 2) || (calibrationTime > 8 && calibrationTime < 10))
        {
            print("ADEM IN ");
            if (_freqBand[7] > _thresholdMin)
            {
                _thresholdMin = _freqBand[7]; 
            }
        }

        else if ((calibrationTime > 2 && calibrationTime < 4 ) || (calibrationTime > 10 && calibrationTime < 12))
        {
            print("PAUZE");
        }

        else if ((calibrationTime > 4 && calibrationTime < 6) || (calibrationTime > 12 && calibrationTime < 14))
        {
            print("ADEM UIT");
            if (_freqBand[7] > _thresholdMax)
            {
                _thresholdMax = _freqBand[7];
            }
        }

        else if (calibrationTime > 6 && calibrationTime < 8)
        {
            print("PAUZE");
        }
        else if (_calibrationFinished == false && calibrationTime > 14)
        {
            _thresholdMax = ((_thresholdMax / 100) * 10);
            _thresholdMin = ((_thresholdMin / 100) * 10);
            print("MAX" + _thresholdMax);
            print("MIN" + _thresholdMin);
            _calibrationFinished = true;
        }
    }



        void MakeFrequencyBands()
        {
            int count = 0;

            for (int i = 0; i < 8; i++)
            {
                float average = 0;
                int sampleCount = (int)Mathf.Pow(2, i) * 2;

                if (i == 7)
                {
                    sampleCount += 2;
                }
                for (int j = 0; j < sampleCount; j++)
                {
                    average += _samples[count] * (count + 1);
                    count++;
                }

                average /= count;

                _freqBand[i] = average * 10;

                if (_freqBand[4] < 0.06 && _freqBand[5] < 0.06 && _freqBand[6] < 0.06 && _freqBand[7] < 0.06)
                {
                    _pauseCount++;
                    if (_pauseCount > 100)
                    {
                        _cubeMovement = 0;
                        _inCount = 0;
                        _outCount = 0;
                        _breathingLock = false;
                    }

                }
                else if (_breathingLock == false)
                {

                    if (_freqBand[4] > _thresholdMax && _freqBand[5] > _thresholdMax && _freqBand[6] > _thresholdMax && _freqBand[7] > _thresholdMax)
                    {
                        _outCount++;
                        if (_outCount > 100)
                        {
                            _cubeMovement = 1;
                            _inCount = 0;
                            _pauseCount = 0;
                            _breathingLock = true;
                        }
                    }
                    else if (_freqBand[5] > _thresholdMin && _freqBand[6] > _thresholdMin && _freqBand[7] > _thresholdMin)
                    {
                        _inCount++;
                        if (_inCount > 100)
                        {
                            _cubeMovement = -1;
                            _outCount = 0;
                            _pauseCount = 0;
                            _breathingLock = true;
                        }

                    }
                }

            }
        }

    }



