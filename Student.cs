using System;

namespace ASEANPhonebook
{
    // ============================================================
    // Class that represents ONE student / contact
    // ============================================================
    class Student
    {
        // Properties (data that every student has)
        public string StudentNumber { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Occupation { get; set; }
        public char Gender { get; set; }          // 'M' or 'F'
        public int CountryCode { get; set; }
        public int AreaCode { get; set; }
        public string Number { get; set; }

        // Constructor – creates a new Student object with all required info
        public Student(string studentNumber, string surname, string firstName,
                       string occupation, char gender, int countryCode,
                       int areaCode, string number)
        {
            StudentNumber = studentNumber;
            Surname = surname;
            FirstName = firstName;
            Occupation = occupation;
            Gender = gender;
            CountryCode = countryCode;
            AreaCode = areaCode;
            Number = number;
        }

        // Returns the full phone number in the format: country-area-number
        public string GetFullPhoneNumber()
        {
            return $"{CountryCode}-{AreaCode}-{Number}";
        }

        // Returns "His" or "Her" based on gender
        public string GetPossessivePronoun()
        {
            return Gender == 'M' || Gender == 'm' ? "His" : "Her";
        }

        // Returns a short description used in the Edit menu
        public string GetShortDescription()
        {
            return $"{FirstName} {Surname} is a {Occupation}. " +
                   $"{GetPossessivePronoun()} number is {GetFullPhoneNumber()}";
        }

        // Returns the formatted line used when searching / displaying students
        public string GetSearchDisplayLine()
        {
            return $"{Surname}, {FirstName}, with student number {StudentNumber}, " +
                   $"is a {Occupation.ToLower()}. {GetPossessivePronoun()} phone number is {GetFullPhoneNumber()}";
        }
    }
}
