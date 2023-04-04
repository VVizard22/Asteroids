using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This SO is responsible of the comunication from the input system to everything that should need the Input data
 * This helps decoupling the Input processor and reader.
 * If a non-related system needs to know this data in the future it can access this SO.
 */
//[CreateAssetMenu()] Commented because usually you only need one of this SO's
public class InputDataHolderSO : ScriptableObject
{
    public event EventHandler OnStartShoot;
    public event EventHandler OnCancelShoot;

    public float rotation = 0f;
    public bool isImpulsing = false;

    public void UpdateData(float rotation) =>
        this.rotation = rotation;

    public void UpdateData(bool isImpulsing) =>
        this.isImpulsing = isImpulsing;

    public void UpdateData(float rotation, bool isImpulsing)
    {
        this.rotation = rotation;
        this.isImpulsing = isImpulsing;
    }

    public void StartShoot()
    {
        OnStartShoot?.Invoke(this, EventArgs.Empty);
    }

    public void CancelShoot()
    {
        OnCancelShoot?.Invoke(this, EventArgs.Empty);
    }

}
