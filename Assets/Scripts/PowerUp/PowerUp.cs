using System;
using TMPro;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public event Action OnDeactivate = delegate { };

    public enum Increment
    {
        Add,
        Multiply,
    };

    [SerializeField] private GameObject quad;
    [SerializeField] private TextMeshPro powerUpText;
    [SerializeField] private Increment increment;
    [SerializeField] private int amount;

    private bool addition;
    private int value;

    public bool Addition { get => addition; set => addition = value; }
    public int Value { get => value; set => this.value = value; }

    private void OnEnable()
    {
        GetIncrement();
    }

    private void GetIncrement()
    {
        if (increment == Increment.Add)
        {
            addition = true;
            value = 0 + amount;
            powerUpText.text = "+ " + amount;
        }
        else if (increment == Increment.Multiply)
        {
            addition = false;
            value = 1 * amount;
            powerUpText.text = "x " + amount;
        }
    }

    public void InvokeEvent()
    {
        OnDeactivate?.Invoke();
    }
}
