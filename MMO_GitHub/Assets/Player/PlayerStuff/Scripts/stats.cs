using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour
{
    GameObject Healthbar;
    [SerializeField]
    private int Health;
    [SerializeField]
    private int MaxHealth;
    [SerializeField]
    private int dropXP;
    [SerializeField]
    private int dropGold;
    [SerializeField]
    private int dropHonor;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if( Health <= 0)
        {
            if(gameObject.tag == "Player")
            {
                Debug.Log("You died!");
                Health = MaxHealth;
                gameObject.transform.position = new Vector3(255, 31, 215);

                return;
            }
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
    public void UpdateHealthbar()
    {
        GameObject HealthbarUpdate = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectedHealthBarPanel/HealthBar");
        BarSrcipt scriptUpdate = HealthbarUpdate.GetComponent<BarSrcipt>();
        scriptUpdate.MaxValue = MaxHealth;
        scriptUpdate.ChangeValue(scriptUpdate.MapHealth(Health, 0, MaxHealth, 0, 1));

        GameObject HealthbarTextUpdate = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectedHealthBarPanel/HealthBar/Mask/Text");
        Text healthText = HealthbarTextUpdate.GetComponent<Text>();
        healthText.text = " " + Health + " / " + MaxHealth;
    }


#region Drops

    void mobDrop(string mobname)
    {

        ClientTCP.PACKAGE_SendDrops(mobname);
    }

#endregion
}
