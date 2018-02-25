using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.GarageManagementSystem.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            GarageManagementUI garageUI = GarageManagementUI.Instance;
            garageUI.OpenGarage();
        }
    }
}
