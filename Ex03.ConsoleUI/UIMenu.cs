using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;
using Ex03.GarageManagementSystem.ConsoleUI;

namespace Ex03.ConsoleUI
{
    public class UIMenu
    {
        Menu m_Menu;

        public static void RunMenu()
        {            
            UIMenu after = new UIMenu();
            after.InitMenu();
            after.Execute();
        }
        
        public void InitMenu()
        {
            m_Menu = new Menu()
            {
                new MenuItem{Text = "Insert new car to the garage", Command = new InsertCarCommand{Client = this}},
                new MenuItem { Text = "Show list of all vehicles at the garage", Command = new ListVehiclesCommand{ Client = this }},
                new MenuItem { Text = "Change vehicle status in the garage", Command = new ChangeStatusCommand { Client = this }},
                new MenuItem { Text = "Fill wheels air pressure to maximum", Command = new FillWheelsCommand { Client = this }},
                new MenuItem { Text = "Load fuel/battery", Command = new LoadCommand { Client = this }},
                new MenuItem { Text = "Show full details of vehicle", Command = new DetailsCommand { Client = this }}
            };
        }

        public void Execute()
        {
            m_Menu.Run();
        }

        public class InsertCarCommand : ICommand
        {
            public UIMenu Client { get; set; }
            GarageManagementUI garageUI = GarageManagementUI.Instance;

            public void Execute()
            {
                garageUI.CreateVehicle();
            }
        }

        public class ListVehiclesCommand : ICommand
        {
            public UIMenu Client { get; set; }
            GarageManagementUI garageUI = GarageManagementUI.Instance;

            public void Execute()
            {
                garageUI.ShowListOfAllVehicleLicenseNumber();
            }
        }

        public class ChangeStatusCommand : ICommand
        {
            public UIMenu Client { get; set; }
            GarageManagementUI garageUI = GarageManagementUI.Instance;

            public void Execute()
            {
                garageUI.ChangeVehicleStatus();
            }
        }

        public class FillWheelsCommand : ICommand
        {
            public UIMenu Client { get; set; }
            GarageManagementUI garageUI = GarageManagementUI.Instance;

            public void Execute()
            {
                garageUI.FillAirPressureToMax();
            }
        }

        public class LoadCommand : ICommand
        {
            public UIMenu Client { get; set; }
            GarageManagementUI garageUI = GarageManagementUI.Instance;

            public void Execute()
            {
                garageUI.LoadFuelOrBattery();
            }
        }

        public class DetailsCommand : ICommand
        {
            public UIMenu Client { get; set; }
            GarageManagementUI garageUI = GarageManagementUI.Instance;

            public void Execute()
            {
                garageUI.ShowVehicleFullDetails();
            }
        }
    }

    public interface ICommand
    {
        void Execute();
    }

    public class MenuItem
    {
        public ICommand Command { get; set; }
        public string Text { get; set; }

        public virtual void Selected()
        {
            if (Command != null)
            {
                Command.Execute();
            }
        }
    }

    public class Menu : List<MenuItem>
    {
        public void Run()
        {
            bool userQuit = false;
            int userSelection;
            while (!userQuit)
            {
                ShowMenu();
                try
                {
                    userQuit = !GetUserSelection(out userSelection);
                    if (!userQuit)
                    {
                        Console.Clear();
                        this[userSelection - 1].Selected();
                    }
                }
                catch 
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid selection");
                    Console.WriteLine();    
                }
            }
        }

        private bool GetUserSelection(out int o_UserSelection)
        {
            bool userQuit = false;
            string userSelection = Console.ReadLine();
            if (userSelection.ToLower() != "q")
            {
                o_UserSelection = int.Parse(userSelection);
                if (!(o_UserSelection > 0 && o_UserSelection <= this.Count))
                {
                    throw new ArgumentException(string.Format("You must select a number between 1-{0}", this.Count));
                }
            }
            else
            {
                o_UserSelection = 0;
                userQuit = true;
            }

            return !userQuit;
        }

        private void ShowMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("Please select your choice:\n");

            int itemNum = 1;
            foreach (MenuItem item in this)
            {
                Console.Write(itemNum.ToString() + ": ");
                Console.WriteLine(item.Text + ".\n");
                itemNum++;
            }

            Console.WriteLine(@"
Type your selection number and press 'enter'.
To quit type 'Q' and then 'enter'
");
        }
    }
}
