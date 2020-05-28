namespace BuilderPattern
{
    public class ConcreteBuilder1 : Builder
    {
        Computer computer = new Computer();

        public override void BuildPartCpu()
        {
            computer.Add("CPU1");
        }

        public override void BuildPartMainBoard()
        {
            computer.Add("Main Board1");
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }
}
