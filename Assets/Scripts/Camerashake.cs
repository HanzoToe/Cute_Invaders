using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//använder Unitys Cinemachine addon
public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField]
    private float ShakeIntensity = 0.5f;
    [SerializeField]
    private float ShakeTime = 0.5f;
    [SerializeField]
    private float sugarRushShakeIntesnity = 1f; 
    //här kan man ändra olika aspekter av VCAM

    private float timer;
    private float originalShakeIntensity; 
    private CinemachineBasicMultiChannelPerlin _cbmcp;
    //timer är för att shaketime, origianlshakeintensity vid vanlugt mode, multichannelperlin är ett sätt för cinemachine att generare randomness
    private SugarRush sugarRushScript; 
    //måste ha sugarushscript för att ge sugarrush en unik shakeintensity

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        sugarRushScript = GameObject.Find("Player").GetComponent<SugarRush>();
        originalShakeIntensity = ShakeIntensity; 

        //här får scriptet VCAM komponenten, och får sugarushscruptet från Player objektet
    }
    private void Start()
    {
        StopShake();
        //ser till att den inte skakar i början
    }
    public void ShakeCamera() {
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();                                                                                                                                                                                                                                                                                                                                                                                                              
    // här får metoden CMBCP komponenten
     _cbmcp.m_AmplitudeGain = ShakeIntensity;
    // CBMCP komponentens egna amplitudegain är = våran shakeintensity

     timer = ShakeTime;
    //här ser vi att timer = shaketime

    }
        void StopShake()
        {
            CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        // här får metoden CMBCP komponenten
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
        //ser till att spelaren har sugarrushscript.active aktiverat för att sugarrushshakeintensity ska byta ut orignalshakeintensity
        //här ser man också att om det inte är aktiverat är shake intensity likamed orignal shake intensity
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
                // om timer är mer än 0 går den tiden neråt, om den är eller är mindre än 0 slutar den skaka.
            }

          SugarRushShake(); 

        //att scriptet alltid är redo att bli aktiverat så fort det blir aktiverat i spelarscriptet.
        }

       
        
       
    }

