using Microsoft.AspNetCore.Mvc.RazorPages;
using RepositoryPatternSample.Interfaces;
using RepositoryPatternSample.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPatternSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICharacterRepository characterRepository;

        public IndexModel(ICharacterRepository characterRepository)
        {
            this.characterRepository = characterRepository;
        }

        public IEnumerable<Character> Characters { get; set; }

        public void OnGet()
        {
            PopulateCharactersIfNoneExist();

            Characters = characterRepository.ListAll();
        }

        private void PopulateCharactersIfNoneExist()
        {
            if (!characterRepository.ListAll().Any())
            {
                characterRepository.Add(new Character("Darth Maul"));
                characterRepository.Add(new Character("Darth Vader"));
                characterRepository.Add(new Character("Yoda"));
                characterRepository.Add(new Character("Mace Windu"));
            }
        }
    }
}
