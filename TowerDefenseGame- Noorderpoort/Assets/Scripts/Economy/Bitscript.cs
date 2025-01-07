using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bitscript : MonoBehaviour
{
    public float bitIndex;
    public float discountAmount = 0f; // In total percentage
    public float multiplierAmount = 0; // Multiplier
    public float starterMoney = 100;
    public TMP_Text bitsText;

    [SerializeField] private
    float maxDiscountAmount = 30f;

    [SerializeField] private
    float maxMultiplierAmount = 5f;

    private bool isDiscountActive = true; // The state of the Discount

    //UI doesnt work for me - Rudo
    void Start()
    {
        bitIndex = starterMoney;
    }

    private void FixedUpdate()
    {
        bitsText.text = bitIndex.ToString();
    }

    public void SetDiscountActivity(bool state) // Set the status of the discount
    {
        isDiscountActive = state;
        if (state == false) discountAmount = 0f;
    }
    public void AddDiscount(float amount)
    {
        discountAmount = Mathf.Clamp(discountAmount + amount, 0f, maxDiscountAmount);
    }
    public void RemoveDiscount(float amount)
    {
        discountAmount = Mathf.Clamp(discountAmount - amount, 0f, maxDiscountAmount);
    }

    public void AddMultiplier(float amount)
    {
        multiplierAmount = Mathf.Clamp(multiplierAmount + amount, 0f, maxMultiplierAmount);
    }
    public void RemoveMultiplier(float amount)
    {
        multiplierAmount = Mathf.Clamp(multiplierAmount - amount, 0f, maxMultiplierAmount);
    }

    public bool RemoveBits(float amount)
    {
        if ((bitIndex - amount) > -1f)
        {
            if (isDiscountActive)
            {
                float discountedAmount = amount * (discountAmount / 100);
                bitIndex -= (amount - discountedAmount);
            }
            else
            {
                bitIndex -= amount;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddBits(float amount)
    {
        if (multiplierAmount > 1f)
        {
            bitIndex += amount * multiplierAmount;
        }
        else
        {
            bitIndex = bitIndex + amount;
        }
    }
}
