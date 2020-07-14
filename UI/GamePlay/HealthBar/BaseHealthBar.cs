﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour {
    [Header("Required Components")]
    public PlayerStats playerData;                                  // Player stats scriptable object reference.
    public GameObject hpDisplayed;                                  // Current player HP value displayed in-game.

    [Header("HP Value Settings")]
    public bool toHideHP = true;                                    // Whether the hp text has to be hidden or not.
    public float hpSecondsVisible = 3f;                             // Seconds the hp text is visible in the screen.
    public float fadeOutSpeed = 0.3f;                              // HP text fa
    private Slider _slider;                                         // UI slider component reference.
    private TextComponent _hpText;                                  // HP displayed text component reference.
    private Animator _hpAnimator;                                   // HP displayed animator component reference.

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    private void Init() {

        // get slider component reference.
        _slider = GetComponent<Slider>();

        if ( hpDisplayed != null ) {

            // get text component from hp text gameObject.
            _hpText = GetComponent<TextComponent>();

            // get animator component from hp text gameObject.
            _hpAnimator = GetComponent<Animator>();

            // set fade out speed animation parameter.

        }
    }
}
