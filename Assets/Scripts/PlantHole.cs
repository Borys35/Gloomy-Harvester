using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHole : MonoBehaviour
{
    public float plantGrowSpeed = 6;
    public GameObject growingPlant;
    public GameObject grownPlant;
    public bool planted = false;

    [SerializeField]
    float plantProgress = 0;
    float maxPlantProgress = 100;
    UIManager uiManager;
    bool interactable;
    bool plantGrown;
    PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        inventory = FindObjectOfType<PlayerInventory>();

        if (plantProgress == 0) {
            growingPlant.SetActive(false);
            grownPlant.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable) {
            if (planted) {
                uiManager.ShowTooltip("Harvest", "E");
            
                if (Input.GetKeyDown("e")) {
                    Harvest();
                }
            } else {
                uiManager.ShowTooltip("Plant", "E");   
            
                if (Input.GetKeyDown("e")) {
                    if (inventory.GetSeedsCount() >= 1) {
                        Plant();
                    }
                }
            }
        }

        if (!plantGrown && planted) {
            plantProgress += plantGrowSpeed * Time.deltaTime;

            plantGrown = plantProgress >= maxPlantProgress;
            if (plantGrown) {
                growingPlant.SetActive(false);
                grownPlant.SetActive(true);
            }
        }
    }

    public void KillPlant() {
        planted = false;
        plantProgress = 0;

        plantGrown = false;

        grownPlant.SetActive(false);
        growingPlant.SetActive(false);
    }

    void Plant() {
        planted = true;
        plantProgress = 0;
        inventory.RemoveSeedCount(1);

        growingPlant.SetActive(true);
    }

    void Harvest() {
        if (plantGrown) {
            inventory.AddCollectedPlants();
            inventory.AddSeedCount(2);
        } else {
            inventory.AddSeedCount(1);
        }

        KillPlant();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") {
            uiManager.HideTooltip();
            interactable = false;
        }
    }
}
