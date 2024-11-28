namespace CounterApp
{
    public class Counter
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Counter(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public void Increment()
        {
            Value++;
        }

        public void Decrement()
        {
            Value--;
        }
    }
}