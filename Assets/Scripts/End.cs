using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public int flowersToEnd = 50;

    UIManager uiManager;
    PlayerInventory inventory;
    bool interactable = false;

    void Start() {
        uiManager = FindObjectOfType<UIManager>();
        inventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
    }

    void Update() {
        if (interactable && Input.GetKeyDown("e") && inventory.GetCollectedPlants() >= flowersToEnd) {
            SceneManager.LoadScene(2);
        }
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            interactable = true;
            uiManager.ShowTooltip("Give flowers - " + flowersToEnd, "E");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") {
            interactable = false;
            uiManager.HideTooltip();
        }
    }
}
