﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : Item {

    /// <summary>
    /// Use item action.
    /// </summary>
    public override void Use() {
        return;
    }

    /// <summary>
    /// Use item action coroutine.
    /// Item use logic goes here.
    /// </summary>
    /// <returns>IEnumerator</returns>
    protected override IEnumerator UseRoutine() {
        yield break;
    }
}
