using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {


    public GameObject playerHand;
    public GameObject Weapon;
    public bool weaponEquipped;

    private void Start()
    {
         weaponEquipped=false;

}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeWeapon();

        }
        if (Input.GetMouseButtonDown(0) && weaponEquipped == true)
            WeaponAttack();
         
    }

    public void TakeWeapon() {
        if(Weapon != null)
        {
            Destroy(playerHand.transform.GetChild(0).gameObject);
            weaponEquipped = false;
        }
        else {
            Weapon = (GameObject)Instantiate(Resources.Load<GameObject>("weapons/Sword"),playerHand.transform.position, playerHand.transform.rotation);
            Weapon.transform.SetParent(playerHand.transform);
            weaponEquipped = true;
        }
    }
		
	public void WeaponAttack()
    {
        Weapon.GetComponent<IWeapon>().Attack();
    }
}
