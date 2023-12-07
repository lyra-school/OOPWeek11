using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Position
{
    Goalkeeper,
    Defender,
    Midfielder,
    Forward
}
namespace Q1
{
    public class Player : IComparable<Player>
    {
        private string _firstName;
        private string _surname;
        private Position _preferredPosition;
        private DateTime _dateOfBirth;

        private static string[] firstNameSet = { "Sophie", "Grace", "Harry", "Luke", "Michael", "Sean", "Ava", "Jack" };
        private static string[] lastNameSet = { "O'Neill", "Walsh", "Lynch", "Daly", "Nolan", "McCarthy", "Dunne", "Brennan", "O'Sullivan" };
        private static int[] monthDaySet = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private static Random rnd = new Random();

        public string FirstName { get { return _firstName; } }
        public string Surname { get { return _surname; } }
        public Position PreferredPosition { get { return _preferredPosition; } }
        public DateTime DateOfBirth { get { return _dateOfBirth; } }
        public int Age { get { return AgeCalc(_dateOfBirth); } }
        

        public Player(Position pos)
        {
            _firstName = SelectRnd(firstNameSet, rnd);
            _surname = SelectRnd(lastNameSet, rnd);
            _preferredPosition = pos;
            _dateOfBirth = DateGenerator(rnd);
        }

        private string SelectRnd(string[] nameSet, Random rnd)
        {
            int i = rnd.Next(0, nameSet.Length);
            return nameSet[i];
        }

        private DateTime DateGenerator(Random rnd)
        {
            DateTime current = DateTime.Now;
            int year = rnd.Next(current.Year - 30, current.Year - 19);
            int month = rnd.Next(1, 13);
            int day;
            if (month == current.Month)
            {
                day = rnd.Next(1, current.Day);
            } else if (month == 2 && year % 4 == 0)
            {
                day = rnd.Next(1, 30);
            } else
            {
                day = rnd.Next(1, monthDaySet[month - 1] + 1);
            }
            return new DateTime(year, month, day);
        }

        private int AgeCalc(DateTime dt)
        {
            DateTime current = DateTime.Now;
            int yearsSince = current.Year - dt.Year;
            if(dt.Month == current.Month && dt.Day > current.Day)
            {
                yearsSince -= 1;
            }
            return yearsSince;
        }

        public override string ToString()
        {
            return $"{FirstName} {Surname} ({Age}) {PreferredPosition.ToString().ToUpper()}";
        }

        public int CompareTo(Player other)
        {
           if(this.PreferredPosition > other.PreferredPosition)
           {
                return 1;
           } else if(this.PreferredPosition < other.PreferredPosition)
           {
                return -1;
           }
           else
           {
                return this.FirstName.CompareTo(other.FirstName);
           }
        }
    }
}
