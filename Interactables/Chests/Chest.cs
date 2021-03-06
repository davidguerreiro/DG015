﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    public int id;                                          // Chest id.

    [Header("Status")]
    public bool opened;                                     // Flag to control if this chest has already been opened.
    
    [Header("Data Source")]
    public InteractableData chestData;                      // Data source reference.

    [Header("Components")]
    public Loot loot;                                       // Loot class component reference.
    public Animator anim;                                   // Animator component reference.
    public ParticleSystem externalParticles;                // External chest particle system component reference.
    public GameObject innerLight;                           // Light inside the chest.
    public ParticleSystem[] openinigParticles;              // Opening particles system array reference.

    private AudioComponent _audio;                          // Audo component reference.
    private Coroutine _openChestRoutine;                    // Open chest coroutine reference.
    
    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        if ( ! opened && GamePlayUI.instance.interactNotification.displayed && GamePlayUI.instance.interactNotification.currentID == id ) {
            ListenForUserInput();
            ListenForCompleted();
        }
    }

    /// <summary>
    /// Listen for user input.
    /// </summary>
    private void ListenForUserInput() {
        // start open chest process.
        if ( Input.GetKey( "f" ) && ! GamePlayUI.instance.interactNotification.inProcess && ! GamePlayUI.instance.interactNotification.completed ) {
            GamePlayUI.instance.interactNotification.RunInteractProcess();
        }
    }

    /// <summary>
    /// Check if interact process from player
    /// is completed, so the chest can be opened.
    /// </summary>
    private void ListenForCompleted() {
        if (  GamePlayUI.instance.interactNotification.completed && _openChestRoutine == null ) {
            _openChestRoutine = StartCoroutine( OpenRoutine() );
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other) {
        
        // show interaction notification to player if this chest has not been opened and chest is in player vision range.
        if ( ! opened && other.gameObject.tag == "Player" && Player.onVisionRange == gameObject.tag && GamePlayUI.instance != null ) {
            GamePlayUI.instance.interactNotification.SetUp( id, chestData.labelEn, chestData.labelProgressEn, chestData.actionSpeed );
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other) {
        // show interaction notification to player if this chest has not been opened and chest is in player vision range.
        if ( ! opened && other.gameObject.tag == "Player" && Player.onVisionRange == gameObject.tag && GamePlayUI.instance != null ) {
            GamePlayUI.instance.interactNotification.SetUp( id, chestData.labelEn, chestData.labelProgressEn, chestData.actionSpeed );
        }

        // check if player is no longer looking at the chest.
        if ( Player.onVisionRange != gameObject.tag ) {
            GamePlayUI.instance.interactNotification.Hide();
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other) {
        // remove interaction notification if displayed.
        if ( ! opened && other.gameObject.tag == "Player" && GamePlayUI.instance != null && GamePlayUI.instance.interactNotification.displayed ) {
            GamePlayUI.instance.interactNotification.Hide();
        }
    }

    /// <sumamry>
    /// Open chest.
    /// </summary>
    public void Open() {
        if ( _openChestRoutine == null ) {
            _openChestRoutine = StartCoroutine( OpenRoutine() );
        }
    }

    /// <sumamry>
    /// Open chest coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator OpenRoutine() {
        opened = true;
        
        // play sound.
        _audio.PlaySound();

        // remove current chest particles.
        externalParticles.Stop();

        // open chest.
        anim.SetBool( "Open", true );
        yield return new WaitForSeconds( .6f );
        innerLight.SetActive( true );
        yield return new WaitForSeconds( 1.65f );

        // play particle effects.
        foreach ( ParticleSystem particles in openinigParticles ) {
            particles.gameObject.SetActive( true );
        }

        // drop chest content into game scene.
        loot.DropLoot();
    }

    /// <summary>
    /// Init class methdod.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    }
}
