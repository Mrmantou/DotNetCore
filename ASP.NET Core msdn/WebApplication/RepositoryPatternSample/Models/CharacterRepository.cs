using RepositoryPatternSample.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPatternSample.Models
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext dbContext;
        public CharacterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Character character)
        {
            dbContext.Characters.Add(character);
            dbContext.SaveChanges();
        }

        public IEnumerable<Character> ListAll()
        {
            return dbContext.Characters.AsEnumerable();
        }
    }
}
