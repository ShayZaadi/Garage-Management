using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;
using Ex03.ConsoleUI;

namespace Ex03.GarageManagementSystem.ConsoleUI
{
    public class GarageManagementUI
    {
        private static GarageManagementUI s_Instance = null;
        private readonly GarageActions m_GarageActions = new GarageActions();

        private GarageManagementUI() { }

        public static GarageManagementUI Instance 
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new GarageManagementUI();
                }

                return s_Instance;
            }
        }

        public void OpenGarage()
        {
            UIMenu.RunMenu();
        }

        public void CreateVehicle()
        {
            Console.WriteLine("Please enter license number");
            string licenseNumber = Console.ReadLine();
            string msg;
            if (m_GarageActions.CheckIfVehicleExistsAndChangeStatus(licenseNumber, out msg))
            {
                Console.WriteLine(msg);
                Console.WriteLine("\nPlease enter owner name");
                string ownerName = Console.ReadLine();

                Console.WriteLine("\nPlease enter owner phone number ");
                string ownerPhoneNumber = Console.ReadLine();
                int typeOfVehicleFromUser = GetVehicleTypeFromUser();
                try
                {
                    m_GarageActions.AddCarToGarage(ownerName, ownerPhoneNumber, licenseNumber, typeOfVehicleFromUser);
                    InsertProperties();
                    AddWheels();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public void LoadFuelOrBattery()
        {
            SetLicenseNumber();
            Dictionary<int, string> energyProperties = m_GarageActions.GetEnergyProperties();
            bool isValid;

            for (int i = 1; i <= energyProperties.Count; i++)
            {
                isValid = false;
                Console.WriteLine(energyProperties[i]);
                do
                {
                    try
                    {
                        m_GarageActions.SetEngineProperty(i, Console.ReadLine());
                        isValid = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("value are between {0} to {1}", ex.MinValue, ex.MaxValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (!isValid);
            }
        }

        public void ShowVehicleFullDetails()
        {
            try
            {
                SetLicenseNumber();
                Console.WriteLine(m_GarageActions.GetVehicleDetails());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowListOfAllVehicleLicenseNumber()
        {
            int yesNoChoice;

            bool isInputValid = true;
            do
            {
                isInputValid = true;
                Console.WriteLine("Filter by status? 1 - Yes, 2 - No");

                if (int.TryParse(Console.ReadLine(), out yesNoChoice) &&
                    (yesNoChoice == 1 || yesNoChoice == 2))
                {
                    if (yesNoChoice == 1)
                    {
                        Console.Clear();
                        ShowListOfAllVehicleLicenseNumberByStatus();
                    }
                    else if (yesNoChoice == 2)
                    {
                        Console.Clear();
                        Console.WriteLine(m_GarageActions.GetListOfVehiclesInGarage());
                    }
                    else
                    {
                        isInputValid = false;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid selection ");
                    isInputValid = false;
                }
            }
            while (!isInputValid);
        }        

        public void ChangeVehicleStatus()
        {
            int status;

            SetLicenseNumber();
            Console.WriteLine("Choose status");
            Console.WriteLine(m_GarageActions.GetListOfStatuses());
            if (int.TryParse(Console.ReadLine(), out status))
            {
                try
                {
                    m_GarageActions.TryChangeVehicleStatus(status);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("value are between {0} to {1}", ex.MinValue, ex.MaxValue);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void FillAirPressureToMax()
        {
            try
            {
                SetLicenseNumber();
                m_GarageActions.TryAddMaxAirToWheels();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ShowListOfAllVehicleLicenseNumberByStatus()
        {
            int userChoice;
            bool isInputValid = false;
            do
            {
                Console.WriteLine(m_GarageActions.GetListOfStatuses());
                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    try
                    {
                        Console.WriteLine(m_GarageActions.GetListOfVehiclesInGarageByStatus(userChoice));
                        isInputValid = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("value are between {0} to {1}", ex.MinValue, ex.MaxValue);
                    }
                }
            }
            while (!isInputValid);
        }

        private void SetLicenseNumber()
        {
            Console.WriteLine("Please enter license number");

            while (!m_GarageActions.i_SetCurrentVehicleInGarage(Console.ReadLine()))
            {
                Console.WriteLine("vehicle does not exists! pLease enter correct license number!");
            }
        }

        private void InsertProperties()
        {
            Dictionary<int, string> properties = m_GarageActions.GetVehicleProperties();
            bool isValid;

            for (int i = 1; i <= properties.Count; i++)
            {
                isValid = false;
                Console.WriteLine(properties[i]);
                do
                {
                    try
                    {
                        m_GarageActions.SetProperty(i, Console.ReadLine());
                        isValid = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("value are between {0} to {1}", ex.MinValue, ex.MaxValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (!isValid);
            }
        }

        private int GetVehicleTypeFromUser()
        {
            int userChoice;
            bool isInputValid = false;
            Console.WriteLine();
            do
            {
                Console.WriteLine(m_GarageActions.GetTypesOfVehicles());
                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    isInputValid = m_GarageActions.isUserVehicleTypeChoiceLegal(userChoice);
                }
            }
            while (!isInputValid);
            Console.WriteLine();

            return userChoice;
        }

        private void AddWheels()
        {
            Console.WriteLine("\nPlease enter wheel manufacturer name");
            string manufacturer = Console.ReadLine();
            float currentAirPressure;
            bool isInputValid = false;

            do
            {
                Console.WriteLine("\nPlease enter current wheel air pressure");
                if (float.TryParse(Console.ReadLine(), out currentAirPressure))
                {
                    try
                    {
                        m_GarageActions.AddWheels(manufacturer, currentAirPressure);
                        isInputValid = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("value are between {0} to {1}", ex.MinValue, ex.MaxValue);
                    }
                }
            }
            while (!isInputValid);
        }
    }
}
