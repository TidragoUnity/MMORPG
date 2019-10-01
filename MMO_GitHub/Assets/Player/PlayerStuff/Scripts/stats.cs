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

    float timer;
    float timerMax;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            string mobName = transform.name;
            selectTarget.deselectTarget = true;
            mobName = mobName.Replace("(Clone)", "");
            mobDrop(mobName);
            anim.SetBool("dead", true);
            Waited(50f);
            selectTarget.dead = true;
            GameObject.Destroy(gameObject);

        }

        if(tag == "Player")
        {
            UiPlayer();
        }
    }

    int GetHealth()
    {
        return Health;
    }

    public void changeHealth(int value)
    {
        Healthbar = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectPanel/SelectedHealthBarPanel/HealthBar");

        BarSrcipt script = Healthbar.GetComponent<BarSrcipt>();
        script.MaxValue = MaxHealth;
        Health -= value;
        script.ChangeValue(script.MapHealth(Health, 0, MaxHealth, 0, 1));
        GameObject HealthbarText = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectPanel/SelectedHealthBarPanel/HealthBar/Mask/Text");
        Text healthText = HealthbarText.GetComponent<Text>();
        healthText.text = " " + Health + " / " + MaxHealth;

    }
    public void UpdateHealthbar()
    {
        GameObject HealthbarUpdate = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectPanel/SelectedHealthBarPanel/HealthBar");
        BarSrcipt scriptUpdate = HealthbarUpdate.GetComponent<BarSrcipt>();
        scriptUpdate.MaxValue = MaxHealth;
        scriptUpdate.ChangeValue(scriptUpdate.MapHealth(Health, 0, MaxHealth, 0, 1));

        GameObject HealthbarTextUpdate = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectPanel/SelectedHealthBarPanel/HealthBar/Mask/Text");
        Text healthText = HealthbarTextUpdate.GetComponent<Text>();
        healthText.text = " " + Health + " / " + MaxHealth;
    }
    public void takeDMG(int value)
    {
        Health -= value;
    }


#region Drops

    void mobDrop(string mobname)
    {

        ClientTCP.PACKAGE_SendDrops(mobname);
    }

    #endregion
    private bool Waited(float seconds)
    {
        timerMax = seconds;

        timer += Time.deltaTime;

        if (timer >= timerMax)
        {
            return true; //max reached - waited x - seconds
        }

        return false;
    }


    #region Player

    void UiPlayer()
    {
        GameObject HealthbarUpdate = GameObject.Find("Canvas/OtherInterfaces/Bars/HealthBar/HealthBarPanel/HealthBar");
        BarSrcipt scriptUpdate = HealthbarUpdate.GetComponent<BarSrcipt>();
        scriptUpdate.MaxValue = MaxHealth;
        scriptUpdate.ChangeValue(scriptUpdate.MapHealth(Health, 0, MaxHealth, 0, 1));

        GameObject HealthbarTextUpdate = GameObject.Find("Canvas/OtherInterfaces/Bars/HealthBar/HealthBarPanel/HealthBar/Mask/Text");
        Text healthText = HealthbarTextUpdate.GetComponent<Text>();
        healthText.text = " " + Health + " / " + MaxHealth;
    }
    #endregion

}
