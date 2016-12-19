using System.ComponentModel;

namespace Day11
{
    public enum ItemType
    {
        [Description("G")]
        Generator,
        [Description("M")]
        Chip,
        [Description("E")]
        Elevator
    }

    class Item : ItemBase
    {
        public bool IsShielded;
        public ItemType Type;
        public string Id;
        
        public Item(string id, ItemType type)
        {
            Id = id;
            Type = type;
        }

        public override string GetName()
        {
            return Id.Substring(0, System.Math.Min(Id.Length, 2)) + "-" + Type.GetDescription();
        }
    }

    abstract class ItemBase
    {
        public abstract string GetName();
    }

    class Dummy: ItemBase
    {
        public override string GetName()
        {
            return "    ";
        }
    }

    
}
