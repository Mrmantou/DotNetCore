namespace BuilderPattern
{
    public class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildPartCpu();
            builder.BuildPartMainBoard();
        }
    }
}
