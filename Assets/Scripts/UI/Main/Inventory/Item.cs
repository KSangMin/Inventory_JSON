using UnityEngine;

public class Item
{
    public int id;
    public string displayName;
    public Sprite icon;
    public string description;
    public int attack;
    public int defense;
    public int hp;
    public int critRate;

    public Item(ItemData item)
    {
        id = item.id;
        displayName = item.name;
        icon = Resources.Load<Sprite>($"Items/{item.spriteName}");
        description = item.description;
        attack = item.attack;
        defense = item.defense;
        hp = item.hp;
        critRate = item.critRate;
    }

    public Item(Item item)
    {
        id = item.id;
        displayName = item.displayName;
        icon = item.icon;
        description = item.description;
        attack = item.attack;
        defense = item.defense;
        hp = item.hp;
        critRate = item.critRate;
    }
}
