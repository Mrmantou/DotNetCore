using RepositoryPatternSample.Models;
using System.Collections.Generic;

namespace RepositoryPatternSample.Interfaces
{
    public interface ICharacterRepository
    {
        IEnumerable<Character> ListAll();
        void Add(Character character);
    }
}
