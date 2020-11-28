using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace TwitterClassLibrary.Validation
{
    public class Validation
    {
        private static Dictionary<string, Func<string, bool>> regexDict = new Dictionary<string, Func<string, bool>>()
        {
            {"FullName", s => Regex.IsMatch(s, @"^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$")}, //Full name as one string
            {"FirstName", s => Regex.IsMatch(s, @"^[a-zA-Z]*$")}, //First Name, just a valid string
            {"LastName", s => Regex.IsMatch(s, @"^(([',. -][a-zA-Z ])?[a-zA-Z]*)*$")}, //Last Name, a string that can contain a - or ' 
            {"NotEmpty", s => !string.IsNullOrEmpty(s)}, //Not empty string
            {"Username", s => Regex.IsMatch(s, @"^[a-zA-Z0-9]{6,24}$")}, //6-24 characters, can contain letters or numbers, not symbols
            {"PhoneNumber", s => Regex.IsMatch(s, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")},//(###) ###-#### or ###-###-####
            {"Time", s => Regex.IsMatch(s, @"^((([0]?[1-9]|1[0-2])(:)[0-5][0-9]((:)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:)[0-5][0-9]((:)[0-5][0-9])?))$") }, //Matches 1:00 AM format, optional seconds
            {"Date", s => Regex.IsMatch(s, @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$")} //matches mm/dd/yyyy
        };

        public static bool Validate(string textToValidate, string key)
        {
            Func<string, bool> test = regexDict[key];
            return test(textToValidate);
        }

        public static bool ValidateDate(string date, ref DateTime dt)
        {
            return DateTime.TryParse(date, out dt);
        }

        public static bool ValidateInteger(string date, ref int value)
        {
            return int.TryParse(date, out value) && value > 0;
        }
    }
}
