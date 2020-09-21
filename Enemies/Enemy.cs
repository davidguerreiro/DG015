﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    // TODO: Display both animation and sound from child class when the enemy is hit.
    //       Disable collider when diying based on collider enum class.
    //       Display animation when death.

    [SerializeField]
    protected int publicID;                                         // Public id, this is unique for each enemy instance created, only by scene. 

    [Header("Data Source")]
    [SerializeField]
    protected EnemyData data;                                       // Enemy data scriptable object. Contains all this enemy default values.
    protected float currentHp;                                      // Current enemy HP.
    protected float maxHp;                                          // Max enemy hp.


    [Header("Status")]
    [SerializeField]
    protected bool isAlive = true;                             // Whether the enemy is alive or has already died.

    protected enum State {
        watching,                                           // Enemy does not move, just observe the enviroment.
        patrolling,                                         // Enemy is patrolling an area.
        combat,                                             // Enemy is enaged in combat withing the player or another target.
    };

    [SerializeField]
    protected State currentState = new State();                // Enemy state.

    protected enum ColliderType {
        sphere,
        box,
        capsule,
        mesh,
    };

    [SerializeField]
    protected ColliderType colliderType = new ColliderType();   // Collider type. Used to check which collider we have to disable.



    [Header("UI")]    
    [SerializeField]
    protected EnemyHPBar enemyHPBar;

    [Header("Settings")]
    [SerializeField]
    protected float timeToDissapear = 5f;                      // How long till the 3D model of this enemy is removed from the scene after it gets defeated.

    /// <summary>
    /// Get damage method.
    /// </summary>
    /// <param name="externalImpactValue">float - damage value caused external attacker, usually the player.</param>
    public virtual void GetDamage( float externalImpactValue ) {
        if ( isAlive ) {
            float damageReceived = ( externalImpactValue / data.defense ) + UnityEngine.Random.Range( 0f, .5f );
            currentHp -= damageReceived;

            if ( currentHp <= 0f ) {
                StartCoroutine( Die() );
            } else {

                UpdateUI();
            }
        }
    }

    /// <summary>
    /// Update UI when the 
    /// enemy gets damaged by the player.
    /// </summary>
    private void UpdateUI() {

        // Update UI.
        if ( enemyHPBar.enemyID != publicID ) {
            enemyHPBar.SetUp( publicID, data.enemySprite, currentHp, maxHp );
        }

        if ( ! enemyHPBar.displayed ) {
            enemyHPBar.Display();
        }

        enemyHPBar.UpdateHP( currentHp );
    }

    /// <summary>
    /// Die method.
    /// </summary>
    public virtual IEnumerator Die() {
        isAlive = false;
        currentHp = 0f;

        yield return null;
    }

    
    /// <summary>
    /// Init class method.
    /// </summary>
    public virtual void Init() {
        currentHp = data.hp;
        maxHp = data.hp;
    }

}
