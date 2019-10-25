using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    bool attAgain;
    public static bool nextOperationAtt;
    [SerializeField]
    int damage;

    float timer = 0.0f;
    float waitTime = 2.4f;

    public static bool stoppAttack;

    Vector3 targetPoint;
    Quaternion targetRotation;

    #region InventoryBoni
    int inventoryCount = 0;
    public InventoryObject inventory;
    public int bonusDamage;
    #endregion


    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim =GetComponent<Animator>();
        inventory = Resources.Load<InventoryObject>("ScriptableObjects/Inventory/Player Inventory");

    }

    // Update is called once per frame
    void Update()
    {
        GetInvBoni();
        timer += Time.deltaTime;
        if(selectTarget.currentTarget != null)
        {
            LookAt(selectTarget.currentTarget.transform);

        }

        if (selectTarget.distance < 3.2f)
        {
            if (selectTarget.currentTarget != null)
            {
                if (timer > waitTime)
                {
                        if (selectTarget.dead == true) { attAgain = false; anim.SetBool("IsAttacking", false); }
                        else
                        {
                            if (Input.GetKeyDown("1"))
                            {
                            stoppAttack = false;
                                Transform target = selectTarget.currentTarget.transform;
                                transform.LookAt(target, Vector3.left);


                                Attack(selectTarget.currentTarget);
                                anim.SetBool("IsWalking", false);
                                anim.SetBool("IsAttacking", true);
                                timer = 0;
                                attAgain = true;
                                return;
                            }
                            if (attAgain)
                            {
                            if (stoppAttack == true) { anim.SetBool("IsAttacking", false); return; }

                            //schaut auf das andere objekt
                            Transform target = selectTarget.currentTarget.transform;
                            //  transform.LookAt(target);
                                //greift das ausgewählte objekt an
                                Attack(selectTarget.currentTarget);
                                //setzt den Timer zurück
                                timer = 0;
                                return;
                            }
                            if (nextOperationAtt)
                            {
                            if (stoppAttack == true) { anim.SetBool("IsAttacking", false); return; }

                            //schaut auf das andere objekt
                            Transform target = selectTarget.currentTarget.transform;
                                transform.LookAt(target);
                                //change animation
                                anim.SetBool("IsWalking", false);
                                anim.SetBool("IsAttacking", true);
                                //attacks the target
                                Attack(selectTarget.currentTarget);
                                attAgain = true;
                                //resets the timer
                                timer = 0;
                                nextOperationAtt = false;
                                return;
                            }
                            else
                            {
                                anim.SetBool("IsWalking", false);
                                anim.SetBool("IsAttacking", false);
                                attAgain = false;
                            }


                        }
                    
                }
            }
        }
        else if (Input.GetKeyDown("1"))
        {
            if(selectTarget.currentTarget == null) { return; }
            //Legt das neue Ziel fest
            GameObject target = selectTarget.currentTarget;
            stoppAttack = false;
            clickToMove.newDestination(target);
            nextOperationAtt = true;

        }else
        {
            attAgain = false;
            anim.SetBool("IsAttacking", false);
        }
    }



    void Attack(GameObject obj)
    {
        try
        {
            if(obj != null)
            {
                if (obj.tag =="otherPlayers")
                {
                    ClientTCP.PACKAGE_DealDamage(damage,obj.name);
                    Debug.Log("try to attack");
                }
                else if(obj.tag == "mob" || obj.tag == "enemy")
                {
                    ClientTCP.PACKAGE_DealDamageTo(damage, obj.GetComponent<stats>().type, obj.GetComponent<stats>().MobID);
                    //obj.GetComponent<stats>().changeHealth(damage);

                }

            }

        }
        catch (System.Exception)
        {

            throw;
        }

    }
    void GetInvBoni()
    {

        if (inventory.Container.Count > inventoryCount)
        {
            bonusDamage = 0;
            foreach (var Container in inventory.Container)
            {
                if (Container.item.type == ItemType.Equipment)
                {
                    EquipmentObject equipment = (EquipmentObject)Container.item;
                    bonusDamage += equipment.attBonus;
                    Debug.Log("bonus damage" + equipment.attBonus);

                }
                else if(Container.item.type == ItemType.Default)
                {

                }
                else if(Container.item.type == ItemType.Food)
                {

                }
            }
        }

    }
    void LookAt(Transform target_)
    {
        transform.LookAt(target_);
        
    }
    void AttackMob(GameObject obj)
    {
        try
        {
            if (obj != null)
            {
                if (obj.tag == "mob" )
                {

                    ClientTCP.PACKAGE_DealDamageTo(damage, obj.GetComponent<stats>().type, obj.GetComponent<stats>().MobID);
                    Debug.Log("try to attack");
                }
                else
                {
                    obj.GetComponent<stats>().changeHealth(damage);

                }

            }

        }
        catch (System.Exception)
        {

            throw;
        }

    }

}
