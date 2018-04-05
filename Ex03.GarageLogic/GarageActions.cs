using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageActions
    {
        private readonly Dictionary<string, VehicleInGarage> r_VehiclesInGarage = new Dictionary<string, VehicleInGarage>();
        private VehicleInGarage m_CurrentVehicleInGarage;

        public VehicleInGarage CurrentVehicleInGarage
        {
            get { return m_CurrentVehicleInGarage; }
            set { m_CurrentVehicleInGarage = value; }
        }

        public bool i_SetCurrentVehicleInGarage(string i_LicenseNumber)
        {
            bool isInGarage = false;
            if (IsLicenseInGarage(i_LicenseNumber))
            {
                m_CurrentVehicleInGarage = r_VehiclesInGarage[i_LicenseNumber];
                isInGarage = true;
            }

            return isInGarage;
        }

        public void AddCarToGarage(string i_OwnerName, string i_OwnerPhone, string i_LicenseNumber, int i_VehicleTypeInt)
        {
            Vehicle newVehicle;
            Vehicle.eVehicleType vehicleType = (Vehicle.eVehicleType)i_VehicleTypeInt;
            newVehicle = Vehicle.CreateVehicle(vehicleType, i_LicenseNumber);
            m_CurrentVehicleInGarage = new VehicleInGarage(i_OwnerName, i_OwnerPhone, newVehicle);
            r_VehiclesInGarage.Add(i_LicenseNumber, m_CurrentVehicleInGarage);
            m_CurrentVehicleInGarage.OwnerVehicle.VehicleType = vehicleType;
        }

        public bool CheckIfVehicleExistsAndChangeStatus(string i_LicenseNumber, out string o_MsgToUser)
        {
            bool isVehicleExists = IsLicenseInGarage(i_LicenseNumber);

            if (isVehicleExists)
            {
                m_CurrentVehicleInGarage = r_VehiclesInGarage[i_LicenseNumber];
                m_CurrentVehicleInGarage.VehicleStatus = VehicleInGarage.eVehicleStatus.InRepair;
                o_MsgToUser = "The vehicle already exists in the garage.";
            }
            else
            {
                o_MsgToUser = "New vehicle in the garage!";
            }

            return !isVehicleExists;
        }

        public bool IsLicenseInGarage(string i_LicenseNumber)
        {
            return r_VehiclesInGarage.ContainsKey(i_LicenseNumber);
        }

        public string GetListOfVehiclesInGarage()
        {
            StringBuilder listOfVehiclesInGarage = new StringBuilder();

            if (r_VehiclesInGarage.Count > 0)
            {
                int index = 1;
                foreach (VehicleInGarage currentVehicleInGarage in r_VehiclesInGarage.Values)
                {
                    listOfVehiclesInGarage.AppendFormat("{0} - {1}{2}", index, currentVehicleInGarage.OwnerVehicle.LicenseNumber, System.Environment.NewLine);
                    index++;
                }

                listOfVehiclesInGarage.AppendFormat("{0}", Environment.NewLine);
            }
            else
            {
                listOfVehiclesInGarage.AppendLine("The garage is empty!");
            }

            return listOfVehiclesInGarage.ToString();
        }

        public string GetListOfVehiclesInGarageByStatus(int i_vehicleStatusInt)
        {
            StringBuilder listOfVehiclesInGarage = new StringBuilder();
            VehicleInGarage.eVehicleStatus vehicleStatus;
            if (r_VehiclesInGarage.Count > 0)
            {
                if (isUserEnumChoiceLegal<VehicleInGarage.eVehicleStatus>(i_vehicleStatusInt))
                {
                    vehicleStatus = (VehicleInGarage.eVehicleStatus)i_vehicleStatusInt;
                    foreach (VehicleInGarage currentVehicleInGarage in r_VehiclesInGarage.Values)
                    {
                        if (currentVehicleInGarage.VehicleStatus == vehicleStatus)
                        {
                            listOfVehiclesInGarage.AppendFormat("{0}{1}", currentVehicleInGarage.OwnerVehicle.LicenseNumber, System.Environment.NewLine);
                        }
                    }
                }
                else
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(VehicleInGarage.eVehicleStatus)).Length, "Out of range value!");
                }
            }
            else
            {
                listOfVehiclesInGarage.AppendLine("The garage is empty!");
            }

            return listOfVehiclesInGarage.ToString();
        }

        public void TryChangeVehicleStatus(int i_newStatusInt)
        {
            if (m_CurrentVehicleInGarage != null)
            {
                if (isUserEnumChoiceLegal<VehicleInGarage.eVehicleStatus>(i_newStatusInt))
                {
                    m_CurrentVehicleInGarage.VehicleStatus = (VehicleInGarage.eVehicleStatus)i_newStatusInt;
                }
                else
                {
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(VehicleInGarage.eVehicleStatus)).Length, "Out of range value!");
                }
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public void TryAddMaxAirToWheels()
        {
            if (m_CurrentVehicleInGarage != null)
            {
                m_CurrentVehicleInGarage.OwnerVehicle.AddMaxAirToWheels();
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public void AddWheels(string i_Manufacturer, float i_CurrentAirPressuer)
        {
            if (m_CurrentVehicleInGarage != null)
            {
                m_CurrentVehicleInGarage.OwnerVehicle.AddWheels(i_Manufacturer, i_CurrentAirPressuer);
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public string GetTypesOfVehicles()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 1;
            foreach (Vehicle.eVehicleType currentType in Enum.GetValues(typeof(Vehicle.eVehicleType)))
            {
                stringBuilder.AppendFormat("{0} - {1}{2}", index.ToString(), currentType.ToString(), Environment.NewLine);
                index++;
            }

            return stringBuilder.ToString();
        }

        public string GetListOfStatuses()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 1;
            foreach (VehicleInGarage.eVehicleStatus currentType in Enum.GetValues(typeof(VehicleInGarage.eVehicleStatus)))
            {
                stringBuilder.AppendFormat("{0} - {1}{2}", index.ToString(), currentType.ToString(), Environment.NewLine);
                index++;
            }

            return stringBuilder.ToString();
        }

        public bool isUserStatusChoiceLegal(int i_UserStatusChoiceInt)
        {
            return isUserEnumChoiceLegal<VehicleInGarage.eVehicleStatus>(i_UserStatusChoiceInt);
        }

        public bool isUserVehicleTypeChoiceLegal(int i_UserVehicleTypeChoiceInt)
        {
            return isUserEnumChoiceLegal<Vehicle.eVehicleType>(i_UserVehicleTypeChoiceInt);
        }

        private bool isUserEnumChoiceLegal<T>(int i_EnumChoiseInt)
        {
            return Enum.IsDefined(typeof(T), i_EnumChoiseInt);
        }

        public Dictionary<int, string> GetVehicleProperties()
        {
            Dictionary<int, string> vehicleProperties = null;

            if (m_CurrentVehicleInGarage != null)
            {
                vehicleProperties = m_CurrentVehicleInGarage.OwnerVehicle.getVehicleProperties();
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }

            return vehicleProperties;
        }

        public void SetProperty(int i_Property, string i_InputFromUser)
        {
            if (m_CurrentVehicleInGarage != null)
            {
                m_CurrentVehicleInGarage.OwnerVehicle.SetProperty(i_Property, i_InputFromUser);
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public string GetVehicleDetails()
        {
            string details = null;

            if (m_CurrentVehicleInGarage != null)
            {
                details = m_CurrentVehicleInGarage.ToString();
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }

            return details;
        }

        public void CheckMyChoiceFuelType(FuelEngine.eFuelType i_CurrentFuelType)
        {
            bool isInputValid1;
            bool isInputValid2 = false;
            FuelEngine.eFuelType myChoise;
            do
            {
                try
                {
                    myChoise = (FuelEngine.eFuelType)int.Parse(Console.ReadLine());
                    do
                    {
                        isInputValid1 = true;
                        if (myChoise != i_CurrentFuelType)
                        {
                            Console.WriteLine("Your fuel type is incorrect");
                            myChoise = (FuelEngine.eFuelType)int.Parse(Console.ReadLine());
                            i_CurrentFuelType = m_CurrentVehicleInGarage.OwnerVehicle.FuelType;
                            isInputValid1 = false;
                        }
                    }
                    while (!isInputValid1);
                    isInputValid2 = true;
                }
                catch
                {
                    Console.WriteLine("Your fuel type is incorrect");
                }
            }
            while (!isInputValid2);
        }

        public void CheckMyFuelAmount()
        {
            Engine currentEngine = m_CurrentVehicleInGarage.OwnerVehicle.EngineSystem;
            float MaxEnergy = m_CurrentVehicleInGarage.OwnerVehicle.EngineSystem.MaxEngineEnergy;
            float CurrentEnergy = m_CurrentVehicleInGarage.OwnerVehicle.EngineSystem.CurrenteEnergy;
            float MyAmount;
            bool isInputValid;
            do
            {
                isInputValid = true;
                if (float.TryParse(Console.ReadLine(), out MyAmount) && (MaxEnergy >= MyAmount + CurrentEnergy))
                {
                    float NewCurrentEnergy = currentEngine.CurrenteEnergy;
                    NewCurrentEnergy += MyAmount;
                    currentEngine.CurrenteEnergy = NewCurrentEnergy;
                }
                else
                {
                    Console.WriteLine("Please enter value between 0 to {0}", MaxEnergy - CurrentEnergy);
                    isInputValid = false;
                }
            }
            while (!isInputValid);
        }
    }
}
