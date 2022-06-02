namespace Combined_XML_Program
{
    class Item
    {
        public string Name { get; set; }
        public bool Decay { get; set; }
        public bool PreventWarp { get; set; }
        public Item(string name)
        {
            Name = name;
        }
        public Item(string name, bool decay, bool preventWarp)
        {
            Name = name;
            Decay = decay;
            PreventWarp = preventWarp;
        }
    }
}
