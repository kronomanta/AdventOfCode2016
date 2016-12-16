using System.Collections.Generic;

namespace Day10
{
    abstract class Node
    {
        protected readonly List<int> Chips = new List<int>();
        public Node Lower;
        public Node Higher;
        public string Id { get; private set; }

        public Node(string id)
        {
            Id = id;
        }

        public IList<int> GetChips() { return Chips.AsReadOnly(); }

        public abstract void AddChip(int chip);
    }

    class OutputBin : Node
    {
        public OutputBin(string id) : base(id) { }
        public override void AddChip(int chip)
        {
            Chips.Add(chip);
        }
    }

    class Bot : Node
    {
        public Bot(string id) : base(id) { }

        public override void AddChip(int chip)
        {
            Chips.Add(chip);
            if (Chips.Count < 2) return;

            if (Chips[0] > Chips[1])
            {
                Lower.AddChip(Chips[1]);
                Higher.AddChip(Chips[0]);
            }
            else
            {
                Lower.AddChip(Chips[0]);
                Higher.AddChip(Chips[1]);
            }
        }
    }
}
