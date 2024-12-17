// See https://aka.ms/new-console-template for more information
using WordFinderTest;

IEnumerable<string> matrix = new List<string>
        {
            "chill",
            "oabcd",
            "lolde",
            "dwind"
        };

IEnumerable<string> wordStream = new List<string> { "chill", "cold", "wind", "snow" };

WordFinder wordFinder = new WordFinder(matrix);

var foundWords = wordFinder.Find(wordStream);

foreach (var word in foundWords)
{
    Console.WriteLine($"'{word}' found in the matrix.");
}