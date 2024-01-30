using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public float fuerza;
    public GameObject camara;
    public GameObject[] obstaculos;
    public Text txtScore;
    public Text txtTotal;
    public Button btnReiniciar;
    private int score=0; 
    private int scoreMax;
    private Vector3 psicionInicial;
    //private Random rnd= new Random();
    private int aleatorio;
    void Start()
    {
        scoreMax=36;
        rb = GetComponent <Rigidbody>();  
        psicionInicial = camara.transform.position;    
        btnReiniciar.gameObject.SetActive(false); 
        txtTotal.gameObject.SetActive(false);
        // aleatorio=Random.Range(0,5);
        // obstaculos[aleatorio].gameObject.SetActive(false);
       
       for (int i = 0; i < 8; i++)
       {
            aleatorio=Random.Range(0,17);
            if (obstaculos[aleatorio].activeSelf==true)
            {
               switch (obstaculos[aleatorio].gameObject.tag)
               {
                case "coin":
                scoreMax-=1;
                break;

                case "coin2":
                scoreMax-=2;
                break;

                case "coin3":
                scoreMax-=3;
                break;
                
               } 
               obstaculos[aleatorio].gameObject.SetActive(false);
            }
       }
        Debug.Log(scoreMax);
        txtTotal.text="Total: "+scoreMax.ToString();
    }
    
    // Update is called once per frame
    void Update()
    {
        // float h = Input.GetAxis("Horizontal");
        // float v = Input.GetAxis("Vertical");
        // Vector3 vector = new Vector3(h,0.5f,v);

        Vector3 vector=Input.acceleration;
        vector=Quaternion.Euler(90,0,0)*vector;
        rb.AddForce(vector*fuerza*Time.deltaTime);

        camara.transform.position=this.transform.position+psicionInicial;
    }
    void OnTriggerEnter(Collider obj){
        switch (obj.gameObject.tag){
                case "coin":
                    score+=1;
                break;

                case "coin2":
                    score+=2;
                break;

                case "coin3":
                    score+=3;
                break;

            }
        txtScore.text="Puntaje: "+score.ToString();
        if (score==scoreMax)
        {
            txtScore.text="¡Ganaste!";
            score=0;
            btnReiniciar.gameObject.SetActive(true);
            txtTotal.gameObject.SetActive(true);
        }
        obj.gameObject.SetActive(false);        
    }

    public void ReloadGame(){
        Application.LoadLevel(Application.loadedLevel);
    }
}
