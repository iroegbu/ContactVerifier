﻿using ContactVerifierCLI.Command.Commands;
using ContactVerifierCLI.Factories;
using ContactVerifierCLI.Parameters;
using ContactVerifierCLI.Response;
using ContactVerifierLibrary.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactVerifierCLI
{
    class Program
    {
        private static List<ContactEntity> Contacts;
        private static List<ContactConnection> Connections;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter command or help to get help:");
            var input = Console.ReadLine();
            while (input != "exit")
            {
                var ProcessedInput = GetInput(input);
                try
                {
                    var command = CommandFactory.GetCommand(ProcessedInput);
                    var parameter = ParameterFactory.GetParameter(ProcessedInput);
                    command.SetParameters(parameter);
                    command.SetState(GetState(input, parameter));
                    var response = command.Run();
                    SavePayload(input, response);
                    DisplayOutput(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                input = Console.ReadLine();
            }
        }

        private static void DisplayOutput(IResponse response)
        {
            Console.WriteLine(response.ResponseMessage());
            Console.WriteLine($"Run time: {response.ResponseTime().TotalMilliseconds} milliseconds{Environment.NewLine}");
        }

        private static void SavePayload(string input, IResponse response)
        {
            var commandText = input.Split(' ').First();
            switch (commandText)
            {
                case "contacts":
                    Contacts = response.Payload() as List<ContactEntity>;
                    break;
                case "connections":
                    Connections = response.Payload() as List<ContactConnection>;
                    break;
                case "list":
                default:
                    break;
            }
        }

        private static object GetState(string input, IParameter parameter)
        {
            var commandText = GetInput(input);
            switch (commandText[0])
            {
                case "contacts":
                    return null;
                case "connections":
                    if (Contacts == null || Contacts.Count == 0)
                    {
                        var _parameter = parameter as ConnectionsParameter;
                        string[] args = { "contacts", _parameter.NumberOfContacts.ToString() };
                        ContactsParameter contactParameter = new ContactsParameter(args);
                        var command = new ContactsCommand();
                        command.SetParameters(contactParameter);
                        var response = command.Run();
                        DisplayOutput(response);
                        SavePayload(string.Join(" ", args), response);
                    }
                    return Contacts;
                case "search":
                    if (Connections == null || Connections.Count == 0)
                    {
                        throw new Exception("You need to run the \"connections <connections_frequency> [<number_of_contacts>]\" command before you can search");
                    }
                    return new Dictionary<string, object>
                    {
                        { "contacts", Contacts },
                        { "connections", Connections }
                    };
                case "list":
                    if (Contacts == null || Contacts.Count == 0)
                    {
                        throw new Exception("You need to run the \"contacts <number_of_contacts>\" command before you can list");
                    }
                    return Contacts;
                default:
                    return null;
            }
        }

        private static string[] GetInput(string input)
        {
            return input.Split(' ').ToArray();
        }
    }
}
