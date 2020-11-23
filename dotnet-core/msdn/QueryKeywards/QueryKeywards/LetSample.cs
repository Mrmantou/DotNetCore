using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryKeywards
{
    class LetSample
    {
        public void Test()
        {
            string[] strings =
            {
                "A penny saved is a penny earned.",
                "The early bird catches the worm.",
                "The pen is mightier than the sword."
            };

            // Split the sentence into an array of words
            // and select those whose first letter is a vowel.
            var earlyBirdQuery = from sentence in strings
                                 let words = sentence.Split(' ')
                                 from word in words
                                 let w = word.ToLower()
                                 where w[0] == 'a' || w[0] == 'e' || w[0] == 'i' || w[0] == 'o' || w[0] == 'u'
                                 select word;

            // Execute the query
            foreach (var v in earlyBirdQuery)
            {
                Console.WriteLine($"\"{v}\" starts with a vowel");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
