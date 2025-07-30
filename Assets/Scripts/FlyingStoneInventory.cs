using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Weapon
{
    public string name;
    public string iconPath; // Optional: path to icon image
    public int damage;

    public Weapon(string name, string iconPath, int damage)
    {
        this.name = name;
        this.iconPath = iconPath;
        this.damage = damage;
    }
}

public class FlyingStoneInventory : MonoBehaviour
{
    private List<Weapon> weaponList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponList = new List<Weapon>
        {


            new Weapon("Sword", "Icons/sword.png", 25),
            new Weapon("Bow", "Icons/bow.png", 15),
            new Weapon("Axe", "Icons/axe.png", 30),
            new Weapon("Spear", "Icons/spear.png", 20)
        };

        // Sprite swordSprite = Resources.Load<Sprite>("Icons/sword");
        var root = GetComponent<UIDocument>().rootVisualElement;
        var scrollView = new ScrollView(ScrollViewMode.Horizontal);
        scrollView.style.height = 150;
        scrollView.style.flexDirection = FlexDirection.Row;

      

        foreach (var weapon in weaponList)
        {
            var button = new Button(() => SelectWeapon(weapon))
            {
                text = weapon.name
            };
            scrollView.Add(button);
        }

    }
    void SelectWeapon(Weapon weapon)
    {
        Debug.Log("Selected weapon: " + weapon.name);
        // Add logic to highlight or equip the weapon
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
