﻿using System;

namespace PrimeNumbers
{
    public class PrimeService
    {
        public bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number % 2 == 0) return (number == 2);
            int root = (int)Math.Sqrt(number);
            for (int i = 3; i <= root; i+=2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
    class Program
    {
        static void Main()
        {
        }
    }
}
