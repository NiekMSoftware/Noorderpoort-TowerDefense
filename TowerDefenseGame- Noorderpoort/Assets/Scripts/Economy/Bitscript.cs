using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bitscript : MonoBehaviour
{
    public float BitIndex;
    public float DiscountAmount = 10f; // In total percentage
    public float MultiplierAmount = 0; // Multiplier
    public float starterMoney = 100;
    public TMP_Text Bitsamount;

    [SerializeField] private
    float MaxDiscountAmount = 30f; // Discount cap

    [SerializeField] private
    float MaxMultiplierAmount = 5f; // Discount cap

    private bool IsDiscountActive = false; // The state of the Discount

    //UI doesnt work for me - Rudo
    void Start()
    {
        BitIndex = starterMoney;
    }

    private void FixedUpdate()
    {
        Bitsamount.text = BitIndex.ToString();
    }

    public void SetDiscountActivity(bool state) // Set the status of the discount
    {
        IsDiscountActive = state;
        if (state == false)
        {
            DiscountAmount = 0f;
        }
    }
    public void AddDiscount(float amount)
    {
        DiscountAmount = Mathf.Clamp(DiscountAmount + amount, 0f, MaxDiscountAmount);
    }
    public void RemoveDiscount(float amount)
    {
        DiscountAmount = Mathf.Clamp(DiscountAmount - amount, 0f, MaxDiscountAmount);
    }

    public void AddMultiplier(float amount)
    {
        MultiplierAmount = Mathf.Clamp(MultiplierAmount + amount, 0f, MaxMultiplierAmount);
    }
    public void RemoveMultiplier(float amount)
    {
        MultiplierAmount = Mathf.Clamp(MultiplierAmount - amount, 0f, MaxMultiplierAmount);
    }

    public bool RemoveBits(float amount)
    {
        if ((BitIndex - amount) > 0f)
        {
            if (IsDiscountActive)
            {
                float discountedAmount = amount * (DiscountAmount / 100);
                BitIndex -= (amount - discountedAmount);
            }
            else
            {
                BitIndex -= amount;
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
        if (MultiplierAmount > 1f)
        {
            BitIndex = (BitIndex + amount) * MultiplierAmount;
        }
        else
        {
            BitIndex = BitIndex + amount;
        }
    }
}
