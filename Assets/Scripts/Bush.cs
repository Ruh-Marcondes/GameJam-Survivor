using UnityEngine;

public class Bush : BiomeMeneger {
    
    [SerializeField] //mostra a variavel no inpect do unity 
    private  SpriteRenderer spriterender;


    [SerializeField]
    private Sprite stage1,stage2,stage3;
    
    private float time;
    private bool isHarvest = false; //pode colher?

    private void Start() {
        spriterender = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate() {
        // tranforma em real time
        time += Time.deltaTime;
        changeState();
        
    }
    private void Update() { 
      
    }

    void changeState(){
        if(time>=3 && time < 10){
            spriterender.sprite = stage2;
        }else{
            if(time>=10){
                spriterender.sprite = stage3;
                isHarvest = true;
            }
        }
        
     
    }
    private void OnTriggerStay2D(Collider2D other) {
         if(other.CompareTag("Player")){ 
              if(isHarvest){ 
               getKeyE();
              }
            }
        }


        void getKeyE(){
            if(Input.GetKey(KeyCode.E)){
                spriterender.sprite = stage1;
                isHarvest = false;
                time = 0;
                print("Coletado!");
                
                }
        }
}