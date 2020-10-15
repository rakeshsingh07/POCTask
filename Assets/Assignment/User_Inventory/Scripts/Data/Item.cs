namespace Inventory.Data
{
    /// <summary>
    /// Stoe item structure
    /// Id : unique for each store item
    /// Name : Unique for each store item
    /// Price : Item price
    /// </summary>
    [System.Serializable]
    public struct Item
    {
        public string id;
        public string name;
        public float price;
    }
}