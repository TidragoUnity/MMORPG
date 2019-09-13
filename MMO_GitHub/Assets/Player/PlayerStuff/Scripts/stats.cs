using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour
{
     GameObject Healthbar;
    [SerializeField]
    private int Health;
    private int MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        if( Health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    int GetHealth()
    {
        return Health;
    }

    public void changeHealth(int value)
    {
        Healthbar = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectedHealthBarPanel/HealthBar");

        BarSrcipt script = Healthbar.GetComponent<BarSrcipt>();
        script.MaxValue = MaxHealth;
        Health -= value;
        script.ChangeValue(script.MapHealth(Health, 0, MaxHealth, 0, 1));
        GameObject HealthbarText = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectedHealthBarPanel/HealthBar/Mask/Text");
        Text healthText = HealthbarText.GetComponent<Text>();
        healthText.text = " " + Health + " / " + MaxHealth;

    }
}
