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


    #region Player
    Animator anim;

    GameObject playerInv;
    bool openInv;
    public static bool isSitting;
    public InventoryObject inventory;
    #region CombatStats

    #endregion
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        Health = MaxHealth;
        if(tag == "Player")
        {
            playerInv = GameObject.Find("PlayerInv");
            playerInv.SetActive(false);
            openInv = false;
            Debug.Log("Trying to Select the Inventory");
            inventory = Resources.Load<InventoryObject>("ScriptableObjects/Inventory/Player Inventory");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( Health <= 0)
        {
            if(gameObject.tag == "Player"|| gameObject.tag == "otherPlayers")
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
        UpdateHealthbar();
        if(tag == "Player")
        {
            GetInvStats();
            UiPlayer();
            openPlayerInv();
            if (Input.GetKeyDown("2"))
            {
                clickToMove.newDestination(gameObject);
                isSitting = true;

            }
            if (isSitting)
            {
                Debug.Log("trying to sit");
                IsSitting();
            }
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
        if(selectTarget.currentTarget == gameObject)
        {
            GameObject HealthbarUpdate = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectPanel/SelectedHealthBarPanel/HealthBar");
            BarSrcipt scriptUpdate = HealthbarUpdate.GetComponent<BarSrcipt>();
            scriptUpdate.MaxValue = MaxHealth;
            scriptUpdate.ChangeValue(scriptUpdate.MapHealth(Health, 0, MaxHealth, 0, 1));

            GameObject HealthbarTextUpdate = GameObject.Find("Canvas/OtherInterfaces/Selected/SelectPanel/SelectedHealthBarPanel/HealthBar/Mask/Text");
            Text healthText = HealthbarTextUpdate.GetComponent<Text>();
            healthText.text = " " + Health + " / " + MaxHealth;
        }
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

    void openPlayerInv()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(openInv== false)
            {

                playerInv.SetActive(true);
                openInv = true;
                Debug.Log("opend Inv");
                return;
            }

        }
        if (Input.GetKeyDown(KeyCode.I)|| Input.GetKeyDown(KeyCode.Escape))
        {
            if (openInv)
            {
                playerInv.SetActive(false);
                openInv = false;

            }
        }


   

    }

    void IsSitting()
    {
        anim.SetBool("IsSitting", true);
        if (Health < MaxHealth)
        { 
                 
            Health++;
        }
        else{
            anim.SetBool("IsSitting", false);
            isSitting = false;
        }
    }

    void GetInvStats()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            
        }
        foreach (var Container in inventory.Container)
        {
            Debug.Log(Container.item.name);
        }
    }
    #endregion

}
