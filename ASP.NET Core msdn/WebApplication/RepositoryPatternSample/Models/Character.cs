using System;

namespace RepositoryPatternSample.Models
{
    public class Character
    {
        public Character(string name)
        {
            Name = name;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
    }
}
