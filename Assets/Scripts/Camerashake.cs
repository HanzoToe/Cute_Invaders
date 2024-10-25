using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//anv�nder Unitys Cinemachine addon
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField]
    private float ShakeIntensity = 0.5f;
    [SerializeField]
    private float ShakeTime = 0.5f;
    [SerializeField]
    private float sugarRushShakeIntesnity = 1f; 
    //h�r kan man �ndra olika aspekter av VCAM

    private float timer;
    private float originalShakeIntensity; 
    private CinemachineBasicMultiChannelPerlin _cbmcp;
    //timer �r f�r att shaketime, origianlshakeintensity vid vanlugt mode, multichannelperlin �r ett s�tt f�r cinemachine att generare randomness
    private SugarRush sugarRushScript; 
    //m�ste ha sugarushscript f�r att ge sugarrush en unik shakeintensity

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        sugarRushScript = GameObject.Find("Player").GetComponent<SugarRush>();
        originalShakeIntensity = ShakeIntensity; 

        //h�r f�r scriptet VCAM komponenten, och f�r sugarushscruptet fr�n Player objektet
    }
    private void Start()
    {
        StopShake();
        //ser till att den inte skakar i b�rjan
    }
    public void ShakeCamera() {
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();                                                                                                                                                                                                                                                                                                                                                                                                              
    // h�r f�r metoden CMBCP komponenten
     _cbmcp.m_AmplitudeGain = ShakeIntensity;
    // CBMCP komponentens egna amplitudegain �r = v�ran shakeintensity

     timer = ShakeTime;
    //h�r ser vi att timer = shaketime

    }
        void StopShake()
        {
            CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        // h�r f�r metoden CMBCP komponenten
        _cbmcp.m_AmplitudeGain = 0f;
        // = ingen shakeintensity 
            timer = 0;
        // = timern resetar
        }
       
        
        void SugarRushShake()
        {


          if(sugarRushScript != null)
          {
            if (sugarRushScript.sugarRushModeActive)
                ShakeIntensity = sugarRushShakeIntesnity;
            else
                ShakeIntensity = originalShakeIntensity;
          }
        //ser till att spelaren har sugarrushscript.active aktiverat f�r att sugarrushshakeintensity ska byta ut orignalshakeintensity
        //h�r ser man ocks� att om det inte �r aktiverat �r shake intensity likamed orignal shake intensity
        }


        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                ShakeCamera();
            }
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    StopShake();
                }
                // om space trycks ner aktiverar metoden shakekamera
                // om timer �r mer �n 0 g�r den tiden ner�t, om den �r eller �r mindre �n 0 slutar den skaka.
            }

          SugarRushShake(); 

        //att scriptet alltid �r redo att bli aktiverat s� fort det blir aktiverat i spelarscriptet.
        }

       
        
       
    }

