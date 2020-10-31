﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContent : MonoBehaviour {

    [HideInInspector]
    public int currentID = 0;                                   // Current active section ID.
    public MenuContentSection[] sections;                       // Menu sections reference.

    [Header("Settings")]
    public float waitInBetween = 1f;                            // How long between each section is hide and displayed.                                 
    
    private Coroutine _animRoutine;                             // Switch section coroutine.
    private AudioComponent _audio;                              // Audio component reference.

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        Init();
    }

    /// <summary>
    /// Switch section wrapper.
    /// </summary>
    /// <param name="sectionID">int - section id</param>
    public void SwitchSection( int sectionID ) {
        if ( _animRoutine == null ) {
            _animRoutine = StartCoroutine( SwitchSectionRoutine( sectionID ) );
        }
    }

    /// <summary>
    /// Switch section coroutine.
    /// </summary>
    /// <param name="sectionID">int - section id</param>
    /// <returns>IEnumerator</returns>
    private IEnumerator SwitchSectionRoutine( int sectionID ) {
        _audio.PlaySound();

        // hide current section.
        sections[currentID].Hide();
        yield return new WaitForSeconds( waitInBetween );
        sections[currentID].gameObject.SetActive( false );

        // display new section.
        sections[sectionID].gameObject.SetActive( true );
        sections[sectionID].Display();

        _animRoutine = null;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get audio component reference.
        _audio = GetComponent<AudioComponent>();
    } 
}