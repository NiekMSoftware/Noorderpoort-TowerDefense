using UnityEngine;
using TMPro;

public class Bitscript : MonoBehaviour
{
    public static Bitscript instance;

    [Header("Money")]
    public float bitIndex;
    public float discountAmount = 0f; 
    private float actualDiscount = 0;
    private float multiplierAmount = 0;
    public float starterMoney = 100;
    public TMP_Text bitsText;

    [SerializeField] private
    float maxDiscountAmount = 30f;

    [SerializeField] private
    float maxMultiplierAmount = 5f;

    void Start()
    {
        bitIndex = starterMoney;
        instance = this;
        bitsText.text = bitIndex.ToString();
    }

    /// <summary>
    /// Returns the input with discount. Mainly for UI stuff
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
    public float CalculateWithDiscount(int cost)
    {
        float discountedAmount = cost * (discountAmount / 100);
        float newCost = (cost - (int)(discountedAmount));
        return newCost;
    }

    /// <summary>
    /// Adds a discount, then clamps it. Updates the shop with it too
    /// </summary>
    /// <param name="amount"></param>
    public void AddDiscount(float amount)
    {
        discountAmount = Mathf.Clamp(discountAmount + amount, 0f, maxDiscountAmount);
        actualDiscount += amount;
        ShopReferences.Instance.UpdateCosts();
    }

    /// <summary>
    /// Removes discount based on the actual discount, so if you remove 1 of 3 MMM's you end up with 20% instead of 15, since the max is 25%
    /// </summary>
    /// <param name="amount"></param>
    public void RemoveDiscount(float amount)
    {
        actualDiscount -= amount;

        if(actualDiscount > maxDiscountAmount)
        {
            discountAmount = maxDiscountAmount;
        }
        else
        {
            discountAmount = actualDiscount;
        }
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
        float discountedAmount = amount * (discountAmount / 100);
        if ((bitIndex - (amount - discountedAmount)) > -1f)
        {
            bitIndex -= (amount - (int)(discountedAmount));
            bitsText.text = bitIndex.ToString();
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
            bitIndex += amount;
        }
        bitsText.text = bitIndex.ToString();
    }
}
