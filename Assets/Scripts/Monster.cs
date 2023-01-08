using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Monster : MonoBehaviour
{
    public float minKillingTime = 8f;
    public float maxKillingTime = 16f;
    public float maxBetweenAttackTime = 30f;
    public Transform monster;
    public AudioClip mainClip;
    public AudioClip monsterClip;
    
    [SerializeField]
    Animator anim;

    PlantHole[] allPlantHoles;
    PlantHole attackedHole;
    bool outing = false;
    float timer = 0;
    float betweenAttackTime;
    PlayerInventory inventory;
    AudioSource source;

    PostProcessVolume m_Volume;
    Vignette m_Vignette;

    void Start() {
        betweenAttackTime = maxBetweenAttackTime;
        allPlantHoles = FindObjectsOfType<PlantHole>();
        inventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();

        source = GameObject.FindWithTag("AudioManager").GetComponent<AudioSource>();

        // Create an instance of a vignette
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.intensity.Override(0.5f);
        m_Vignette.color.Override(new Color(1, 0, 0, 1));
        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    void ShowVignette() {
        m_Vignette.enabled.Override(true);
    }

    void HideVignette() {
        m_Vignette.enabled.Override(false);
    }

    void Update() {
        if (!attackedHole && !outing)
            timer += Time.deltaTime;

        if (timer >= betweenAttackTime) {
            timer = 0;
            TryToAttack();
        }
        
        if (attackedHole) {
            if (!attackedHole.planted && !outing) {
                StopAllCoroutines();
                StartCoroutine(Out());
            }
        }
    }

    IEnumerator Out() {
        outing = true;
        yield return new WaitForSeconds(1);
        if (attackedHole.planted) {
            attackedHole.KillPlant();
        }
        attackedHole = null;
        anim.SetTrigger("out");
        HideVignette();
        source.clip = mainClip;
        source.Play();
        PlayerMovement.Running = false;
        yield return new WaitForSeconds(2);
        monster.gameObject.SetActive(false);
        outing = false;
    }

    void TryToAttack() {
        
        List<PlantHole> holes = new List<PlantHole>();
        foreach (PlantHole plantHole in allPlantHoles)
        {
            if (plantHole.planted) {
                holes.Add(plantHole);
            }
        }

        if (holes.Count == 0) return;

        ShowVignette();
        source.clip = monsterClip;
        source.Play();
        PlayerMovement.Running = true;

        attackedHole = holes[Random.Range(0, holes.Count)];

        monster.gameObject.SetActive(true);
        transform.position = attackedHole.transform.position;

        StartCoroutine(Killing());
    }
    
    IEnumerator Killing() {
        yield return new WaitForSeconds(Random.Range(minKillingTime, maxKillingTime));

        attackedHole.KillPlant();
        
        outing = true;
        attackedHole = null;
        anim.SetTrigger("out");
        HideVignette();
        source.clip = mainClip;
        source.Play();
        PlayerMovement.Running = false;
        yield return new WaitForSeconds(2);
        monster.gameObject.SetActive(false);
        outing = false;
    }
}
