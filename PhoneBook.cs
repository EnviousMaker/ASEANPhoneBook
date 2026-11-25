using System;
using System.Collections.Generic;
using System.Linq;

namespace ASEANPhonebook
{
    // ============================================================
    // Class that manages the whole collection of students
    // ============================================================
    class Phonebook
    {
        // The list that stores all Student objects (no limit)
        private List<Student> students = new List<Student>();

        // Official country codes and their names
        private static readonly Dictionary<int, string> CountryNames = new Dictionary<int, string>
        {
            { 60, "Malaysia" },
            { 62, "Indonesia" },
            { 63, "Philippines" },
            { 65, "Singapore" },
            { 66, "Thailand" }
        };

        // Check if a country code is valid
        public bool IsValidCountryCode(int code)
        {
            return CountryNames.ContainsKey(code);
        }

        // Get country name from code (or "Unknown" if invalid)
        public string GetCountryName(int code)
        {
            return CountryNames.ContainsKey(code) ? CountryNames[code] : "Unknown";
        }

        // Add a new student to the phonebook
        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        // Find a student by student number. Returns null if not found.
        public Student? FindByStudentNumber(string studentNumber)
        {
            return students.FirstOrDefault(s =>
                s.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase));
        }

        // Search for students whose country code is in the given list.
        // Results are sorted alphabetically by surname.
        public List<Student> SearchByCountries(List<int> countryCodes)
        {
            return students
                .Where(s => countryCodes.Contains(s.CountryCode))
                .OrderBy(s => s.Surname)
                .ToList();
        }

        // Get all students (sorted by surname) – used for "ALL"
        public List<Student> GetAllStudents()
        {
            return students.OrderBy(s => s.Surname).ToList();
        }

        // How many students are currently stored
        public int Count => students.Count;
    }
}
