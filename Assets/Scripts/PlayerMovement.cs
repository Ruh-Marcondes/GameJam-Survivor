using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public  float speed; //velocidade
    public Rigidbody2D playerRb; // Pegar o componente que adiciona fisica  


    public float hInput; // Horizontal Input
    public float vInput;// Vertical Input

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis ("Horizontal"); // Pega o Inpute horizontal e passa para a váriavel.
        vInput = Input.GetAxis("Vertical");

       
    
    }
    private void FixedUpdate() {
        //a Debug.Log(hInput+","+vInput);
        playerRb.velocity = new Vector2(hInput*speed,vInput*speed);  //Faz o player mexer
    }
}
