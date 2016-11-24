using System;

namespace Task2.Tests
{
    public class Person
    {
        public string Name { get; }
        public string Surname { get; }

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }

        public override bool Equals(object o)
        {
            if (!(o is Person)) return false;
            if (ReferenceEquals(this,o)) return true;
            return (Name == ((Person) o).Name) && (Surname == ((Person) o).Surname);
        }

        protected bool Equals(Person other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Surname, other.Surname);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name?.GetHashCode() ?? 0)*397) ^ (Surname?.GetHashCode() ?? 0);
            }
        }
    }
}
