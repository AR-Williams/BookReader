using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Williams_Program_Four
{
    class BookReader
    {
        static Dictionary<string, int> book;

        public BookReader()
        {
            book = new Dictionary<string, int>();
        }

        public void ReadFile(string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);
                while (!reader.EndOfStream)
                {
                    string entry = reader.ReadLine();
                    string[] delimit = entry.Split();

                    for (int i = 0; i < delimit.Length - 1; i++)
                    {
                        if (!book.ContainsKey(delimit[i]))
                        {
                            book.Add(delimit[i], i);
                        }
                        else if(book.ContainsKey(delimit[i]))
                        {
                            book[delimit[i]]++;
                        }
                    }
                }
                Console.WriteLine("File has been processed.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public void ReadDictionary()
        {
            foreach (KeyValuePair<string, int> entry in book)
            {
               Console.WriteLine("key: {0}, value: {1} ", entry.Key, entry.Value);              
            }  
            
        }

        public void WordCount(string word)
        {
           int count = 0;

           if(book.ContainsKey(word))
           {
                int num = 0;
                bool exist = book.TryGetValue(word, out num);
                   
                if(exist == true)
                {
                   count = num;
                   Console.WriteLine(count); 
                }
           }
           else
           {
               Console.WriteLine(word + " was not found in the book.");
           }     
        }

        public void WordFreq(int num)
        {
            List<string> list = book.Keys.ToList();
            list.Sort();

            foreach(string i in list)
            {
                if (book[i] == num)
                {
                    Console.WriteLine(i, book[i]);
                }
            }

        }

        public void WordLength(int number)
        {
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string,int>>(book);
            list.Sort(ComparePairs);

            foreach(KeyValuePair<string, int> entry in list)
            {
                if(entry.Key.Length == number)
                {
                    Console.WriteLine("Word: {0}, Appearances: {1}", entry.Key, entry.Value);
                }
            }
        }

        public double WordAvg()
        {
            double average = 0.0;
            double sum = 0.0;
            int unique = UniqueWords();

            foreach(KeyValuePair<string, int> entry in book)
            {                                
                sum += entry.Value;
            }

            average = unique / sum;

            return average;
        }

        public int UniqueWords()
        {
            int unique = 0;

            foreach(KeyValuePair<string, int> entry in book.Distinct())
            {
                unique++;
            }
           
            return unique;
        }
        
        public void GreatestMatch()
        {            
            foreach (KeyValuePair<string, int> entry in book)
            {
                 if(entry.Value > 1000)
                 {
                     Console.WriteLine("Word: {0} ", entry.Key);
                     Console.WriteLine("Frequency: {0} ", entry.Value);
                     Console.WriteLine();
                 }
            }
        }

        public int ComparePairs (KeyValuePair<string, int> a, KeyValuePair<string, int> b)
        {
            return Comparer<int>.Default.Compare(a.Value, b.Value); 
        }

        public void MenuSelection()
        {
            Console.WriteLine("Choose from the menu what queries you would like done:");
            Console.WriteLine("(C) -> Find out how many times one word appears in the file.");
            Console.WriteLine("(A) -> Show all words that appear a certain number of times.");
            Console.WriteLine("(L) -> Find all words of a specific length.");
            Console.WriteLine("(Q) -> Quit");
            char response = Console.ReadLine()[0];

            while (Char.ToUpper(response) != 'Q')
            {
                if (Char.ToUpper(response) == 'C')
                {
                    Console.WriteLine("What word would you like to search for?");
                    string word = Console.ReadLine();

                    WordCount(word);

                    Console.WriteLine("Would you like to return to menu? Choose (Y) for yes, (Q) to Quit.");
                    response = Console.ReadLine()[0];
                }
                else if (Char.ToUpper(response) == 'A')
                {
                    Console.WriteLine("What word count would you like to search for?");
                    string search = Console.ReadLine();
                    int number = int.Parse(search);

                    WordFreq(number);

                    Console.WriteLine("Would you like to return to menu? Choose (Y) for yes, (Q) to Quit.");
                    response = Console.ReadLine()[0];
                }
                else if (Char.ToUpper(response) == 'L')
                {
                    Console.WriteLine("What length would you like to search for?");
                    string search = Console.ReadLine();
                    int number = int.Parse(search);

                    WordLength(number);

                    Console.WriteLine("Would you like to return to menu? Choose (Y) for yes, (Q) to Quit.");
                    response = Console.ReadLine()[0];
                }
                else if (Char.ToUpper(response) == 'Y')
                {
                    Console.WriteLine("Choose from the menu what queries you would like done:");
                    Console.WriteLine("(C) -> Find out how many times one word appears in the file.");
                    Console.WriteLine("(A) -> Show all words that appear a certain number of times.");
                    Console.WriteLine("(L) -> Find all words of a specific length.");
                    Console.WriteLine("(Q) -> Quit");
                    response = Console.ReadLine()[0];
                }
                else
                {
                    Console.WriteLine("Please try again.");
                    Console.WriteLine("(C) -> Find out how many times one word appears in the file.");
                    Console.WriteLine("(A) -> Show all words that appear a certain number of times.");
                    Console.WriteLine("(L) -> Find all words of a specific length.");
                    Console.WriteLine("(Q) -> Quit");
                    response = Console.ReadLine()[0];
                }
            }
        }

        static void Main(string[] args)
        {
            BookReader readBook = new BookReader();

            Console.WriteLine("What is the location of the file you would like processed?");
            string filename = Console.ReadLine();
            readBook.ReadFile(filename);

            Console.WriteLine();

            Console.WriteLine("Name of file: {0}", filename);
            Console.WriteLine("Number of unique words: {0}", readBook.UniqueWords());
            Console.WriteLine("Average number of words: {0}", readBook.WordAvg());
            Console.WriteLine("Words with the most frequency: ");
            Console.WriteLine();
            readBook.GreatestMatch();

            Console.WriteLine();

            readBook.MenuSelection();

            Console.ReadKey();
        }
    }
}
