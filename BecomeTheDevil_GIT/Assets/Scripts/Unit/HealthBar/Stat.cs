using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
class Stat
{
    /// <summary>
    /// A reference to the bar that this stat is controlling
    /// </summary>
    [SerializeField]
    private BarScript bar;

    /// <summary>
    /// The max value of the stat
    /// </summary>
    [SerializeField]
    private float maxVal;

    /// <summary>
    /// The current value of the stat
    /// </summary>
    [SerializeField]
    private float currentVal;

    /// <summary>
    /// A Property for accessing and setting the current value
    /// </summary>
    public float CurrentValue
    {
        get
        {
            return currentVal;
        }
        set
        {
            //Clamps the current value between 0 and max
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);

            //Updates the bar
            Bar.Value = currentVal;
        }
    }

    /// <summary>
    /// A proprty for accessing the max value
    /// </summary>
    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            //Updates the bar's max value
            Bar.MaxValue = value;

            //Sets the max value
            this.maxVal = value;
        }
    }

    public BarScript Bar
    {
        get
        {
            return bar;
        }
    }

    /// <summary>
    /// Initializes the stat
    /// This function needs to be called in awake
    /// </summary>
    public void Initialize()
    {
        //Updates the bar
        this.MaxVal = maxVal;
        this.CurrentValue = currentVal;
    }
}

