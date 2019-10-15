namespace Albert.Demo.EntityFramework.Sqlite.Seed
{
    interface ISeedInitial
    {
        void Create(DemoContext context);
    }
}
