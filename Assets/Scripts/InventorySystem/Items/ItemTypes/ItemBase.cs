using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Generic/Item")]
public class ItemBase : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite ImageUI;
    public bool IsStackable;
    public int Cost;

    private string name;
    private string description;
    private Sprite imageUI;
    private bool isStackable;
    private int cost;


}
