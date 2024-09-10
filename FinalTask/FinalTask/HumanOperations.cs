using System.Data;
using System.Text.RegularExpressions;

namespace FinalTask
{
    internal class HumanOperations
    {
        public delegate void SortingOrderDelegate(int userChoice);
        public event SortingOrderDelegate UserChoiceDefinedEvent;
        List<Human> humans = new List<Human>();
        List<Human> sortedHumans = new List<Human>();

        /// <summary>
        /// Get surnames and data sorting order values from console. Calls user's input event. 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ReadData()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Сортировка фамилий на русском языке");
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine("Введите " + i + "-ю фамилию на русском языке: ");
                string surname = Console.ReadLine();
                Human human = new Human(surname);
                humans.Add(human);
            }

            Console.WriteLine("Введите порядок сортировки (1 - по убыванию, 2 - по возрастанию): ");
            string userAnswer = Console.ReadLine();

            if (CheckUserInput(humans, userAnswer))
            {
                int userChoice = int.Parse(userAnswer);
                UserChoiceDone(userChoice);
            }
            else
            {
                // other various exeptions
                throw new Exception();
            }
        }

        /// <summary>
        ///  Call appropriate methods according to data sorting order
        /// </summary>
        /// <param name="userChoice"></param>
        public void DoSorting(int userChoice)
        {
            switch (userChoice)
            {
                case 1:
                    {
                        SortDataAscending();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n" + "Сортировка по убыванию: " + "\n");
                        break;
                    }
                case 2:
                    {
                        SortDataDescending();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n" + "Сортировка по возрастанию: " + "\n");
                        break;
                    }
            }
            ShowSortedData();
        }


        /// <summary>
        /// Sorting data in ascending order
        /// </summary>
        private void SortDataAscending()
        {
            sortedHumans = humans.OrderBy(human => human.SurName).ToList();
        }

        /// <summary>
        /// Sorting data in descending order
        /// </summary>
        private void SortDataDescending()
        {
            sortedHumans = humans.OrderByDescending(human => human.SurName).ToList();
        }

        /// <summary>
        /// Shows sorted data on console
        /// </summary>
        private void ShowSortedData()
        {
            foreach (Human h in sortedHumans)
            {
                Console.WriteLine(h.SurName.ToString());
            }
        }

        /// <summary>
        ///  Checks user input by different criterias
        /// </summary>
        /// <param name="humans"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="SpecialException"></exception>
        private bool CheckUserInput(List<Human> humans, string input)
        {
            bool checkResult = false;
            
            // if sorting order value is not a digit
            if (int.TryParse(input, out int result) == false)
            {
                throw new FormatException();
            }

            // if sorting order value equals to 1 or 2
            else if (input == "1" || input == "2")
            {
                checkResult = true;
            }

            // if sorting order value is in a digit form but not in accessible range
            else
            {
                throw new ArgumentOutOfRangeException(); 
            }

            foreach (Human h in humans)
            {
                string s = h.SurName.ToString();

                // if one or more surname(s) value is empty
                if (s.Length == 0)
                {
                    throw new ArgumentNullException();
                }

                // if one or more surname(s) value has non-cyrrilic symbols
                if (!Regex.IsMatch(s, @"\P{IsCyrillic}"))
                {
                    checkResult = true;
                }
                else
                {
                    throw new SpecialException();
                }
            }
            return checkResult;
        }

        /// <summary>
        /// Calls delegate which defines event if event is not null
        /// </summary>
        /// <param name="choice"></param>
        protected virtual void UserChoiceDone(int choice)
        {
            UserChoiceDefinedEvent?.Invoke(choice);
        }
    }
}
