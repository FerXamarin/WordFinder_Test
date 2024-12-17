namespace WordFinderTest
{
    public class WordFinder
    {
        private char[,] matrix;
        private int rows;
        private int columns;

        public WordFinder(IEnumerable<string> matrix)
        {
            rows = matrix.Count();
            columns = matrix.First().Length;
            this.matrix = new char[rows, columns];

            int rowIndex = 0;
            foreach (var row in matrix)
            {
                for (int column = 0; column < columns; column++)
                {
                    this.matrix[rowIndex, column] = row[column];
                }
                rowIndex++;
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var foundWords = new HashSet<string>();
            var wordCounts = new Dictionary<string, int>();

            foreach (var word in wordstream)
            {
                if (SearchWord(word) && !foundWords.Contains(word))
                {
                    foundWords.Add(word);
                    if (wordCounts.ContainsKey(word))
                    {
                        wordCounts[word]++;
                    }
                    else
                    {
                        wordCounts[word] = 1;
                    }
                }
            }

            return wordCounts.OrderByDescending(kv => kv.Value)
                             .ThenBy(kv => kv.Key)
                             .Take(10)
                             .Select(kv => kv.Key);
        }

        private bool SearchWord(string word)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    if (SearchFrom(word, row, col))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool SearchFrom(string word, int row, int col)
        {
            return SearchHorizontally(word, row, col) || SearchVertically(word, row, col);
        }

        private bool SearchHorizontally(string word, int row, int col)
        {
            if (col + word.Length > columns)
            {
                return false;
            }
            for (int i = 0; i < word.Length; i++)
            {
                if (matrix[row, col + i] != word[i])
                {
                    return false;
                }
            }
            return true;
        }

        private bool SearchVertically(string word, int row, int col)
        {
            if (row + word.Length > rows)
            {
                return false;
            }
            for (int i = 0; i < word.Length; i++)
            {
                if (matrix[row + i, col] != word[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
