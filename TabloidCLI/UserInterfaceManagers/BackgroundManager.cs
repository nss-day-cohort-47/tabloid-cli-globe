using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BackgroundManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BackgroundRepository _backgroundRepository;
        private string _connectionString;

        public BackgroundManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _backgroundRepository = new BackgroundRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Background Color Menu");
            Console.WriteLine(" 1) Blue");
            Console.WriteLine(" 2) Green");
            Console.WriteLine(" 3) Red");
            Console.WriteLine(" 4) Cyan");
            Console.WriteLine(" 5) Magenta");
            Console.WriteLine(" 6) Black");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Blue();
                    return this;
                case "2":
                    Green();
                    return this;
                case "3":
                    Red();
                    return this;
                case "4":
                    Cyan();
                    return this;
                case "5":
                    Magenta();
                    return this;
                case "6":
                    Black();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Blue()
        {
            ConsoleColor background = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
        }
        private void Green()
        {
            ConsoleColor background = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
        }
        private void Red()
        {
            ConsoleColor background = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();
        }
        private void Cyan()
        {
            ConsoleColor background = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
        }
        private void Magenta()
        {
            ConsoleColor background = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Clear();
        }
        private void Black ()
        {
            Console.ResetColor();
            Console.Clear();
        }
    }
}