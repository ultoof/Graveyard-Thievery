using UnityEngine;
using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class Soul : MonoBehaviour
{
        // Properties 
        bool inrange = false;
        public string displayName;
        public GameObject vfx;
        public TextMeshProUGUI stealText;
        private PlayerController playerController;
        private float startingMoney = 0;

        private void Awake()
        {
            if (DataManager.instance){
               startingMoney = DataManager.instance.money; // The starting money 
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) // If the player enters the radius
        {
          if (collision.gameObject.CompareTag("Player"))
          {
            playerController = collision.gameObject.GetComponent<PlayerController>(); // Gets the collider from player
            stealText.text = $"Press E To Steal {displayName}"; // Shows the text to steal
            inrange = true;
            
          }
        }

        private void OnTriggerExit2D(Collider2D collision) { // when the player leaves the pickup radius
            if (collision.gameObject.CompareTag("Player"))
            {
                inrange = false;
                stealText.text = "";
            }
        }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame) // Checks if E is pressed down 
        {
            if (inrange == true)
            {
                GameObject clonedVFX = Instantiate(vfx, transform.position, Quaternion.identity);
                playerController.soul = true;
                Destroy(gameObject); // MAkes the item dissapear 
                Destroy(clonedVFX,2);
            }
        }
    }
}
