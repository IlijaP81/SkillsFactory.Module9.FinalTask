
using FinalTask;

class Program
{
    static void Main(string[] args)
    {
        Program program = new Program();
        program.StartProgram();
    }

    private void StartProgram()
    {
        HumanOperations humanOperations = new HumanOperations();
        humanOperations.UserChoiceDefinedEvent += humanOperations.DoSorting;
        try
        {
            humanOperations.ReadData();
        }
        catch (SpecialException)
        { Console.WriteLine("Введенные вами данные должны содержать только буквы русского алфавита"); }
        catch (FormatException)
        { Console.WriteLine("Необходимо ввести значение 1 или 2"); }
        catch (ArgumentOutOfRangeException)
        { Console.WriteLine("Для выбора порядка сортировки необходимо ввести значение 1 или 2"); } 
        catch (ArgumentNullException)
        { Console.WriteLine("Введенные вами данные должны быть не пустыми"); }
        catch (Exception)
        { Console.WriteLine("Произошла непредвиденная ошибка"); }
    }
}

