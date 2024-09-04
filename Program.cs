using System;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        bool reRun;
        do
        {
            // Display a welcome message and prompt user for names
            Console.WriteLine("Welcome!!!\n");
            Console.WriteLine("------------------------------------------");
            string studentLname = GetInput("Enter Last Name: ");
            string studentFname = GetInput("Enter First Name: ");
            string studentMname = GetInput("Enter Middle Name: ");
            char middleInitial = studentMname.Length > 0 ? studentMname[0] : '\0'; // Handle empty middle name

            // Prompt user for payment
            double payment = GetPayment();
            if (payment < 32000)
            {
                Console.WriteLine("Not enough money. Please make sure you provide at least 32,000.");
                reRun = ShouldRerun();
                continue; // Skip the rest of the loop if payment is insufficient
            }

            // Define and display the list of courses
            string[] courses = { "BSCS", "BSIT", "BSBA", "BSAIS", "BSA", "BSHM", "BACOMM", "BMMA", "BSTM" };
            DisplayCourses(courses);

            // Prompt user to select a course
            string selectedCourse = SelectCourse(courses);
            Console.WriteLine($"You selected: {selectedCourse}");
            Console.WriteLine("------------------------------------------");

            // Check if required files exist
            if (!CheckFiles())
            {
                // If files are missing, prompt to rerun the program
                Console.WriteLine("Required files are missing. Please make sure all files are present.");
                reRun = ShouldRerun();
                continue; // Skip the rest of the loop if files are missing
            }
            Console.WriteLine("------------------------------------------");

            // Display full name
            Console.WriteLine("\nYou are now Enrolled in STI!!!");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Full name is: {studentLname}, {studentFname} {middleInitial}.");
            DisplayCourseDescription(selectedCourse);
            Console.WriteLine("------------------------------------------");


            // Determine if we should rerun
            reRun = ShouldRerun();

        } while (reRun);

        Console.WriteLine("------------------------------------------");
        Console.WriteLine("Program stopped! Thank you!!");
    }

    // Method to get user input
    static string GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    // Method to get payment amount
    static double GetPayment()
    {
        double payment;
        while (true)
        {
            Console.Write("Enter payment amount: ");
            if (double.TryParse(Console.ReadLine(), out payment) && payment >= 0)
            {
                return payment;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid payment amount.");
            }
        }
    }

    // Method to display list of courses
    static void DisplayCourses(string[] courses)
    {
        Console.WriteLine("Available Courses:");
        for (int i = 0; i < courses.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {courses[i]}");
        }
    }

    // Method to handle course selection
    static string SelectCourse(string[] courses)
    {
        int choice;
        while (true)
        {
            Console.Write("Select a course by entering the corresponding number: ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= courses.Length)
            {
                return courses[choice - 1];
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a number corresponding to one of the courses.");
            }
        }
    }

    // Method to display the description of the selected course
    static void DisplayCourseDescription(string courseCode)
    {
        string description = courseCode switch
        {
            "BSCS" => "Bachelor of Science in Computer Science",
            "BSIT" => "Bachelor of Science in Information Technology",
            "BSBA" => "Bachelor of Science in Business Administration",
            "BSAIS" => "Bachelor of Science in Accounting Information System",
            "BSA" => "Bachelor of Science in Accountancy",
            "BSHM" => "Bachelor of Science in Hospitality Management",
            "BACOMM" => "Bachelor of Arts in Communication",
            "BMMA" => "Bachelor of Multimedia Arts",
            "BSTM" => "Bachelor of Science in Tourism Management",
            _ => "Description not available"
        };
        Console.WriteLine($"Your Course: {description}");
    }

    // Method to check if required files exist
    static bool CheckFiles()
    {
        string directorPath = @"C:\Users\Zoey\Documents\SampleRequirements";

        // File names
        string[] filesToCheck = {
            "Diploma.pdf",
            "GoodMoral.pdf",
            "form137.pdf",
            "MedCert.pdf",
            "PSA.pdf",
        };

        // Flag to track file completeness
        bool allFilesExist = true;

        // Loop to check if files exist
        foreach (string fileName in filesToCheck)
        {
            string filePath = Path.Combine(directorPath, fileName);
            if (File.Exists(filePath))
            {
                Console.WriteLine($"File {fileName} exists!");
            }
            else
            {
                Console.WriteLine($"File {fileName} does not exist!!!");
                allFilesExist = false; // Mark as incomplete if any file is missing
            }
        }

        return allFilesExist; // Return true if all files exist, false otherwise
    }

    // Method to determine if the program should rerun
    static bool ShouldRerun()
    {
        Console.WriteLine("Enter 'y' if you want to run the program again and 'n' to stop:");
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        char ansRerun = char.ToLower(keyInfo.KeyChar);

        switch (ansRerun)
        {
            case 'y':
                return true;
            case 'n':
                return false;
            default:
                Console.WriteLine("\nInvalid input. Please enter 'y' or 'n'.");
                return false; // Optionally stop the loop on invalid input
        }
    }
}