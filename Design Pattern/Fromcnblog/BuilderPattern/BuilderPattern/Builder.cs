namespace BuilderPattern
{
    /// <summary>
    /// 抽象建造者，这个场景下为“组装人”，也可以定义为接口
    /// </summary>
    public abstract class Builder
    {
        public abstract void BuildPartCpu();

        public abstract void BuildPartMainBoard();

        public abstract Computer GetComputer();
    }
}
