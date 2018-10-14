using System;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    /// <summary>
    /// A reference to the content of the bar(the colored bar)
    /// </summary>
    [SerializeField]
    private Image content;

    /// <summary>
    /// A reference to the text on the bar for example. Health: 100
    /// </summary>
   // [SerializeField]
   // private Text valueText;

    /// <summary>
    /// This movement speed of the bar
    /// </summary>
    [SerializeField]
    private float lerpSpeed;

    /// <summary>
    /// The current fill amount of the bar
    /// </summary>
    private float fillAmount;

    /// <summary>
    /// Indicates if this bar will change color
    /// </summary>
    [SerializeField]
    private bool lerpColors;

    [SerializeField]
    private bool lerpBar;

    /// <summary>
    /// The color that the bar will have when it is full
    /// This is only in use if lerpColors is enabled
    /// </summary>
    [SerializeField]
    private Color fullColor;

    /// <summary>
    /// The color that the bar will have when it is low
    /// This is only in use if lerpColors is enabled
    /// </summary>
    [SerializeField]
    private Color lowColor;

    /// <summary>
    /// Inidcates the max value of the bar, this can reflect the player's max health etc.
    /// </summary>
    public float MaxValue { get; set; }

    /// <summary>
    /// A property for setting a the bar's value
    /// It makes sure to update the text on the bar and generete a new fill amount.
    /// </summary>
    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    void Start()
    {
        if (lerpColors) //Sets the standard color
        {
            content.color = fullColor;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Makes sure that handle bar is called.
        HandleBar();

    }

    /// <summary>
    /// Updates the bar
    /// </summary>
    private void HandleBar()
    {
        if (fillAmount != content.fillAmount) //If we have a new fill amount then we know that we need to update the bar
        {
            if (lerpBar)
            {
                //Lerps the fill amount so that we get a smooth movement
                content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
            }
            else
            {
                content.fillAmount = fillAmount;
            }

            if (lerpColors) //If we need to lerp our colors
            {   
                //Lerp the color from full to low
                content.color = Color.Lerp(lowColor, fullColor, fillAmount);
            }
           
        }
    }

    /// <summary>
    /// This method maps a range of number into another range
    /// </summary>
    /// <param name="value">The value to evaluate</param>
    /// <param name="inMin">The minimum value of the evaluated variable</param>
    /// <param name="inMax">The maximum value of the evaluated variable</param>
    /// <param name="outMin">The minum number we want to map to</param>
    /// <param name="outMax">The maximum number we want to map to</param>
    /// <returns></returns>
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
