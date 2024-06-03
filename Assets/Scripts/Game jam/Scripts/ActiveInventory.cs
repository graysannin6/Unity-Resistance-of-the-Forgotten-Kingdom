using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveInventory : Singleton<ActiveInventory>
{
    private int activeSlotIndexNum = 0;
    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Inventory.MouseRoll.performed += OnMouseRollPerformed;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Inventory.MouseRoll.performed -= OnMouseRollPerformed;
        playerControls.Disable();
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveHighlight(0);
    }

    private void OnMouseRollPerformed(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<float>();

        if (scrollValue > 0)
        {
            ScrollUp();
        }
        else if (scrollValue < 0)
        {
            ScrollDown();
        }
    }

    private void ScrollUp()
    {
        ToggleActiveHighlight(1);
    }

    private void ScrollDown()
    {
        ToggleActiveHighlight(-1);
    }

    private void ToggleActiveHighlight(int direction)
    {
        activeSlotIndexNum = (activeSlotIndexNum + direction + this.transform.childCount) % this.transform.childCount;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(activeSlotIndexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance == null)
        {
            Debug.LogError("ActiveWeapon instance is not set.");
            return;
        }

        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        InventorySlot currentSlot = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>();

        if (currentSlot == null)
        {
            Debug.LogWarning("No InventorySlot found in the current slot.");
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        WeaponInfo weaponInfo = currentSlot.GetWeaponInfo();

        if (weaponInfo == null)
        {
            Debug.LogWarning("No WeaponInfo found in the InventorySlot.");
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = weaponInfo.weaponPrefab;

        if (weaponToSpawn == null)
        {
            Debug.LogWarning("No weapon prefab found in the WeaponInfo.");
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);

        newWeapon.transform.parent = ActiveWeapon.Instance.transform;

        ActiveWeapon.Instance.Newweapon(newWeapon.GetComponent<MonoBehaviour>());
    }


}
