using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject guideObject;

    UIManager uiManager;
    bool interactable = false;
    bool open = false;

    void Start() {
        uiManager = FindObjectOfType<UIManager>();
        guideObject.SetActive(false);
    }

    void Update() {
        if (interactable && Input.GetKeyDown("e")) {
            if (!open) {
                open = true;
                PlayerMovement.CanMove = false;
                guideObject.SetActive(true);
            } else {
                open = false;
                PlayerMovement.CanMove = true;
                guideObject.SetActive(false);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            interactable = true;
            uiManager.ShowTooltip("Examine", "E");
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
