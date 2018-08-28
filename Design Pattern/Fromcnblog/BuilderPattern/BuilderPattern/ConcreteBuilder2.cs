namespace BuilderPattern
{
    public class ConcreteBuilder2 : Builder
    {
        Computer computer = new Computer();

        public override void BuildPartCpu()
        {
            computer.Add("CPU2");
        }

        public override void BuildPartMainBoard()
        {
            computer.Add("Main Board2");
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }
}
