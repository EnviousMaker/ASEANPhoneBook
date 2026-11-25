using System;
using System.Collections.Generic;

namespace ASEANPhonebook
{
    // ============================================================
    // Main program – handles menus and user interaction
    // ============================================================
    class Program
    {
        static Phonebook phonebook = new Phonebook();

        static void Main(string[] args)
        {
            Console.Title = "ASEAN Phonebook";
            bool running = true;

            while (running)
            {
                ShowMainMenu();
                string? choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        StoreToPhonebook();
                        break;
                    case "2":
                        EditEntry();
                        break;
                    case "3":
                        SearchByCountry();
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("\nThank you for using the ASEAN Phonebook. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please choose 1-4.\n");
                        break;
                }
            }
        }

        // -------------------- MAIN MENU --------------------
        static void ShowMainMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("          ASEAN PHONEBOOK");
            Console.WriteLine("========================================");
            Console.WriteLine("[1] Store to ASEAN phonebook");
            Console.WriteLine("[2] Edit entry in ASEAN phonebook");
            Console.WriteLine("[3] Search ASEAN phonebook by country");
            Console.WriteLine("[4] Exit");
            Console.Write("\nEnter your choice: ");
        }

        // -------------------- STORE --------------------
        static void StoreToPhonebook()
        {
            bool addMore = true;

            while (addMore)
            {
                Console.WriteLine("\n--- Store New Entry ---");

                Console.Write("Enter student number: ");
                string studentNumber = Console.ReadLine()?.Trim() ?? "";

                // Optional: prevent duplicate student numbers
                if (phonebook.FindByStudentNumber(studentNumber) != null)
                {
                    Console.WriteLine("A student with this number already exists. Please use a different number.");
                    continue;
                }

                Console.Write("Enter surname: ");
                string surname = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Enter first name: ");
                string firstName = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Enter occupation: ");
                string occupation = Console.ReadLine()?.Trim() ?? "";

                char gender;
                while (true)
                {
                    Console.Write("Enter gender (M for male, F for female): ");
                    string g = Console.ReadLine()?.Trim().ToUpper() ?? "";
                    if (g == "M" || g == "F")
                    {
                        gender = g[0];
                        break;
                    }
                    Console.WriteLine("Invalid gender. Please enter M or F.");
                }

                int countryCode;
                while (true)
                {
                    Console.Write("Enter country code: ");
                    if (int.TryParse(Console.ReadLine(), out countryCode) &&
                        phonebook.IsValidCountryCode(countryCode))
                    {
                        break;
                    }
                    Console.WriteLine("Invalid country code. Valid codes: 60, 62, 63, 65, 66");
                }

                Console.Write("Enter area code: ");
                int.TryParse(Console.ReadLine(), out int areaCode);

                Console.Write("Enter number: ");
                string number = Console.ReadLine()?.Trim() ?? "";

                // Create a new Student object and store it
                Student newStudent = new Student(
                    studentNumber, surname, firstName, occupation,
                    gender, countryCode, areaCode, number);

                phonebook.AddStudent(newStudent);
                Console.WriteLine("\nStudent successfully added!");

                // Ask if user wants to add another
                Console.Write("Do you want to enter another entry [Y/N]? ");
                string answer = Console.ReadLine()?.Trim().ToUpper() ?? "N";
                addMore = (answer == "Y");
            }

            Console.WriteLine();
        }

        // -------------------- EDIT --------------------
        static void EditEntry()
        {
            Console.WriteLine("\n--- Edit Entry ---");
            Console.Write("Enter student number: ");
            string studentNumber = Console.ReadLine()?.Trim() ?? "";

            Student? student = phonebook.FindByStudentNumber(studentNumber);

            if (student == null)
            {
                Console.WriteLine("Error: Student number not found.\n");
                return;
            }

            bool editing = true;
            while (editing)
            {
                Console.WriteLine($"\nHere is the existing information about {student.StudentNumber}:");
                Console.WriteLine(student.GetShortDescription());
                Console.WriteLine();
                Console.WriteLine("Which of the following information do you wish to change?");
                Console.WriteLine("[1] Student number  [2] Surname  [3] Gender  [4] Occupation");
                Console.WriteLine("[5] Country code    [6] Area code  [7] Phone number");
                Console.WriteLine("[8] None – Go back to main menu");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter new student number: ");
                        string newNum = Console.ReadLine()?.Trim() ?? "";
                        if (phonebook.FindByStudentNumber(newNum) != null &&
                            !newNum.Equals(student.StudentNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("That student number is already used by another student.");
                        }
                        else
                        {
                            student.StudentNumber = newNum;
                            Console.WriteLine("Student number updated.");
                        }
                        break;

                    case "2":
                        Console.Write("Enter new surname: ");
                        student.Surname = Console.ReadLine()?.Trim() ?? student.Surname;
                        Console.WriteLine("Surname updated.");
                        break;

                    case "3":
                        while (true)
                        {
                            Console.Write("Enter new gender (M/F): ");
                            string g = Console.ReadLine()?.Trim().ToUpper() ?? "";
                            if (g == "M" || g == "F")
                            {
                                student.Gender = g[0];
                                Console.WriteLine("Gender updated.");
                                break;
                            }
                            Console.WriteLine("Invalid gender. Please enter M or F.");
                        }
                        break;

                    case "4":
                        Console.Write("Enter new occupation: ");
                        student.Occupation = Console.ReadLine()?.Trim() ?? student.Occupation;
                        Console.WriteLine("Occupation updated.");
                        break;

                    case "5":
                        while (true)
                        {
                            Console.Write("Enter new country code: ");
                            if (int.TryParse(Console.ReadLine(), out int newCode) &&
                                phonebook.IsValidCountryCode(newCode))
                            {
                                student.CountryCode = newCode;
                                Console.WriteLine("Country code updated.");
                                break;
                            }
                            Console.WriteLine("Invalid country code. Valid codes: 60, 62, 63, 65, 66");
                        }
                        break;

                    case "6":
                        Console.Write("Enter new area code: ");
                        if (int.TryParse(Console.ReadLine(), out int newArea))
                        {
                            student.AreaCode = newArea;
                            Console.WriteLine("Area code updated.");
                        }
                        break;

                    case "7":
                        Console.Write("Enter new phone number: ");
                        student.Number = Console.ReadLine()?.Trim() ?? student.Number;
                        Console.WriteLine("Phone number updated.");
                        break;

                    case "8":
                        editing = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select 1-8.");
                        break;
                }
            }

            Console.WriteLine();
        }

        // -------------------- SEARCH --------------------
        static void SearchByCountry()
        {
            Console.WriteLine("\n--- Search by Country ---");
            Console.WriteLine("From which country:");
            Console.WriteLine("[1] Philippines  [2] Thailand  [3] Singapore");
            Console.WriteLine("[4] Indonesia    [5] Malaysia  [6] ALL");
            Console.WriteLine("[0] No More");

            List<int> selectedCodes = new List<int>();
            List<string> selectedNames = new List<string>();
            bool selecting = true;
            int choiceNumber = 1;

            while (selecting)
            {
                Console.Write($"Enter choice {choiceNumber}: ");
                string? input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "0":
                        selecting = false;
                        break;
                    case "1":
                        if (!selectedCodes.Contains(63))
                        {
                            selectedCodes.Add(63);
                            selectedNames.Add("Philippines");
                        }
                        choiceNumber++;
                        break;
                    case "2":
                        if (!selectedCodes.Contains(66))
                        {
                            selectedCodes.Add(66);
                            selectedNames.Add("Thailand");
                        }
                        choiceNumber++;
                        break;
                    case "3":
                        if (!selectedCodes.Contains(65))
                        {
                            selectedCodes.Add(65);
                            selectedNames.Add("Singapore");
                        }
                        choiceNumber++;
                        break;
                    case "4":
                        if (!selectedCodes.Contains(62))
                        {
                            selectedCodes.Add(62);
                            selectedNames.Add("Indonesia");
                        }
                        choiceNumber++;
                        break;
                    case "5":
                        if (!selectedCodes.Contains(60))
                        {
                            selectedCodes.Add(60);
                            selectedNames.Add("Malaysia");
                        }
                        choiceNumber++;
                        break;
                    case "6": // ALL
                        selectedCodes.Clear();
                        selectedNames.Clear();
                        selectedCodes.AddRange(new[] { 60, 62, 63, 65, 66 });
                        selectedNames.Add("ALL countries");
                        selecting = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Enter 0-6.");
                        break;
                }
            }

            if (selectedCodes.Count == 0)
            {
                Console.WriteLine("No countries selected.\n");
                return;
            }

            List<Student> results = phonebook.SearchByCountries(selectedCodes);

            string countryList = string.Join(" and ", selectedNames);
            Console.WriteLine($"\nHere are the students from {countryList}:");

            if (results.Count == 0)
            {
                Console.WriteLine("No students found for the selected country/countries.");
            }
            else
            {
                foreach (Student s in results)
                {
                    Console.WriteLine(s.GetSearchDisplayLine());
                }
            }

            Console.WriteLine();
        }
    }
}
