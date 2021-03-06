﻿using ContactVerifierLibrary.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactVerifierLibrary.Generators
{
    public class GenerateContacts
    {
        private int ContactCount;
        private int MaxCommunicationFrequecy;

        public GenerateContacts(int Count, int Frequency = 10)
        {
            ContactCount = Count;
            MaxCommunicationFrequecy = Frequency;
        }

        public IEnumerable<ContactEntity> Generate(List<string> FirstNames, List<string> LastNames)
        {
            var Rand = new Random();
            foreach (var i in Enumerable.Range(1, ContactCount))
            {
                yield return new ContactEntity()
                {
                    ContactID = i,
                    ContactName = $"{FirstNames.ElementAt(Rand.Next(FirstNames.Count))} {LastNames.ElementAt(Rand.Next(LastNames.Count))}",
                    Location = Rand.Next(10),
                    CommuncationFrequency = Rand.Next(1, MaxCommunicationFrequecy)
                };
            }
        }
    }
}
