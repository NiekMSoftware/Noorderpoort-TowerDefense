using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bitscript : MonoBehaviour
{
    [Header("Money")]
    public float BitIndex;
    public float starterMoney = 100;

    [Header("Multiplier")]
    [SerializeField] private float MaxMultiplierAmount = 5f;
    public float MultiplierAmount = 0;

    [Header("Discount")]
    public float DiscountAmount = 10f;
    [SerializeField] private float MaxDiscountAmount = 30f;
    private bool IsDiscountActive = false;

    [Header("Other")]
    public TMP_Text Bitsamount;
    void Start()
    {
        //Starting Money
        BitIndex =starterMoney;
    }

    private void FixedUpdate()
    {
        //Money Text
        Bitsamount.text = BitIndex.ToString();
    }

    public void SetDiscountActivity(bool state)
    {
        //Discount
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

    //Multiplier
    public void AddMultiplier(float amount)
    {
        MultiplierAmount = Mathf.Clamp(MultiplierAmount + amount, 0f, MaxMultiplierAmount);
    }
    public void RemoveMultiplier(float amount)
    {
        MultiplierAmount = Mathf.Clamp(MultiplierAmount - amount, 0f, MaxMultiplierAmount);
    }

    //No more money
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

    //More money
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
