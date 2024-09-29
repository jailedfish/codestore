using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    private int Health = 100;
    private bool isGraphics = true;
    public int health {
        get
        {
            return Health;
        }
        set
        {
            if (value > 100)
            {
                Health = 100;
            }
            else
            {
                Health = value;
            }
        }
    }
    private int heal_hp = 15;
    public int max_hp { get; private set; }
    public Slider healthBar;
    public Text MessageField;

    public delegate void Handler();
    public event Handler? onDeath;
    public event Handler? onHeal;

    public HealthManager (Slider healthBar, Text messageField) {
        this.Health = 100;
        this.healthBar = healthBar;
        MessageField = messageField;
        max_hp = Health;
    }

    public HealthManager() {
        this.Health = 100;
        max_hp = 100;
        isGraphics = false;
    }

    private IEnumerator displaymessage(string msg)
    {

        MessageField.text = msg;
        MessageField.enabled = true;
        yield return new WaitForSeconds(3f);
        MessageField.enabled = false;
    }

    private void Start()
    {
        if (isGraphics)
        {
            healthBar.maxValue = Health;
            healthBar.value = Health;
        }

        max_hp = Health;
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        if (isGraphics)
        {
            healthBar.value = Health;
        }

        if (Health <= 0)
        {
            onDeath?.Invoke();
        }
    }
    public void Heal()
    {
        
        if (Health + heal_hp > max_hp && !isGraphics)
        {
            StartCoroutine(displaymessage("suck dick"));
            return;
        }

        onHeal?.Invoke();
        Health += heal_hp;
        if (isGraphics)
        {
            healthBar.value = Health;
        }

    }
}