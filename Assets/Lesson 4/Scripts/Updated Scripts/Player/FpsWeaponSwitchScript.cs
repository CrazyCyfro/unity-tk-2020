using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsWeaponSwitchScript : MonoBehaviour
{
    public Transform weaponPos;
    private Queue<int> slotQ;
    private Transform activeSlot;
    private FpsFireManagerScript fireManager;
    private Transform dropOffPos;
    private KeyCode[] NUMBER_KEYCODES = {
                    KeyCode.Alpha0,
                    KeyCode.Alpha1,
                    KeyCode.Alpha2,
                    KeyCode.Alpha3,
                    KeyCode.Alpha4,
                    KeyCode.Alpha5,
                    KeyCode.Alpha6,
                    KeyCode.Alpha7,
                    KeyCode.Alpha8,
                    KeyCode.Alpha9
                    };

    void Awake()
    {
        fireManager = GetComponent<FpsFireManagerScript>();
        dropOffPos = GetComponent<FpsPickupDropScript>().dropOffPos;

        // Default activeSlot to first slot
        activeSlot = weaponPos.GetChild(0);

        slotQ = new Queue<int>();

        slotQ.Enqueue(1);
        slotQ.Enqueue(1);

        AssignActiveWeapon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            AssignActiveWeapon(slotQ.Peek());
        }

        for (int i = 0; i < NUMBER_KEYCODES.Length; i++) {
            if (Input.GetKeyDown(NUMBER_KEYCODES[i])) {
                if (i == 0) {
                    AssignActiveWeapon(10);
                } else {
                    AssignActiveWeapon(i);
                }
                
            }
        }
    }

    public void AssignActiveWeapon(WeaponPickupScript newWeapon)
    {
        bool activeValid = false;
        // Search for empty, valid slot for new weapon, switch to it, then return
        foreach (int slot in newWeapon.slots) {
            if (activeSlot.GetSiblingIndex() == slot) activeValid = true;
            
            Transform parentSlot = weaponPos.GetChild(slot);
            
            if (parentSlot.childCount != 0) continue;
            
            // Check if activeSlot has an active weapon
            if (activeSlot.childCount > 0) {
                // Deactivate active weapon
                activeSlot.GetChild(0).gameObject.SetActive(false);  
            }

            // Assign new weapon to empty slot
            newWeapon.transform.SetParent(parentSlot, false);

            activeSlot = parentSlot;

            slotQ.Dequeue();
            slotQ.Enqueue(slot + 1);
            
            fireManager.AssignHeldWeapon();
            return;
        }

        // If all valid slots for new weapon are filled, proceed below.
        // Check if activeSlot is valid slot. Otherwise, choose first valid slot.
        if (!activeValid) {
            // Deactivate active weapon
            activeSlot.GetChild(0).gameObject.SetActive(false);
            // Find first valid slot for new weapon, assign it to activeSlot
            activeSlot = weaponPos.GetChild(newWeapon.slots[0]);
        }

        // Activate weapon, then drop it
        GameObject weaponToDrop = activeSlot.GetChild(0).gameObject;
        weaponToDrop.SetActive(true);
        weaponToDrop.GetComponent<WeaponPickupScript>().Drop(dropOffPos.position);

        // Assign new weapon to vacated slot
        newWeapon.transform.SetParent(activeSlot, false);

        fireManager.AssignHeldWeapon();
    }

    // Assign active weapon at slot
    public void AssignActiveWeapon(int num)
    {
        num -= 1;

        if (num >= weaponPos.childCount) return;
        Transform parentSlot = weaponPos.GetChild(num);
        if (parentSlot.childCount == 0) return;
        if (activeSlot == parentSlot) return;

        if (activeSlot.childCount != 0) activeSlot.GetChild(0).gameObject.SetActive(false);
        activeSlot = parentSlot;
        activeSlot.GetChild(0).gameObject.SetActive(true);
        
        slotQ.Dequeue();
        slotQ.Enqueue(num + 1);
        
        fireManager.AssignHeldWeapon();
    }

    // Assign next active weapon
    public void AssignActiveWeapon()
    {
        // Cycle through slots starting from next slot, then looping around at the last slot
        for (int i = 1; i <= weaponPos.childCount - 1; i++) {
            int slot = (activeSlot.GetSiblingIndex() + i) % weaponPos.childCount;
            Transform parentSlot = weaponPos.GetChild(slot);

            // Switch to valid slot if found
            if (parentSlot.childCount == 1) {
                if (activeSlot.childCount != 0) activeSlot.GetChild(0).gameObject.SetActive(false);
                activeSlot = parentSlot;
                activeSlot.GetChild(0).gameObject.SetActive(true);

                slotQ.Dequeue();
                slotQ.Enqueue(slot + 1);

                break;
            }
        }

        fireManager.AssignHeldWeapon();
    }
}
