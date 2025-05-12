using Kaizen.CampaignCodeGenerator.Services.Abstract;
using Kaizen.CampaignCodeGenerator.Services.Concrete;

namespace Kaizen.CampaignCodeGenerator;

internal class Program
{
    static void Main()
    {
        ICodeGenerator generator = new CodeGenerator();
        ICodeValidator validator = new CodeValidator();

        // 10 adet kod ürettim test için.
        var codes = generator.GenerateUniqueCodes(10);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Campaign Code Validator");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1 - Enter a code to validate");
            Console.WriteLine("2 - View all valid generated codes");
            Console.WriteLine("0 - Exit");
            Console.WriteLine(new string('-', 40));
            Console.Write("\nYour choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string userCode;
                    while (true)
                    {
                        Console.Write("\nEnter your code (8 characters only): ");
                        userCode = Console.ReadLine()?.ToUpper();

                        if (userCode != null && userCode.Length == 8)
                            break;

                        Console.WriteLine("Invalid length. Code must be exactly 8 characters.");
                    }

                    bool isValid = validator.IsValid(userCode);

                    Console.WriteLine(isValid
                        ? "\n[OK] The code is VALID."
                        : "\n[ERR] The code is INVALID.");

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "2":
                    Console.WriteLine("\nGenerated Valid Codes:\n");
                    foreach (var code in codes)
                    {
                        Console.WriteLine(code);
                    }
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "0":
                    Console.WriteLine("\nExiting application...");
                    return;

                default:
                    Console.WriteLine("\nInvalid selection. Please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}

