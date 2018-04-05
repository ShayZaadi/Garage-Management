using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_Model;
        private string m_LicenseNumber;
        private readonly List<Wheel> r_WheelsCollection;
        private Engine m_Engine;
        private readonly int r_NumOfWheels;
        private readonly float r_MaxWheelAirPressure;
        private eVehicleType m_VehicleType;
        private FuelEngine.eFuelType m_FuelType;

        internal Vehicle(string i_LicenseNumber, float i_MaxWheelAirPressure, int i_NumOfWheels, FuelEngine.eFuelType i_FuelType)
        {
            m_LicenseNumber = i_LicenseNumber;
            r_NumOfWheels = i_NumOfWheels;
            r_MaxWheelAirPressure = i_MaxWheelAirPressure;
            r_WheelsCollection = new List<Wheel>();
            m_FuelType = i_FuelType;
        }

        public FuelEngine.eFuelType FuelType
        {
            get { return m_FuelType; }
        }

        public eVehicleType VehicleType
        {
            get { return m_VehicleType; }
            set { m_VehicleType = value; }
        }

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelMotorCycle:
                    {
                        newVehicle = new MotorCycle(i_LicenseNumber, eVehicleType.FuelMotorCycle);
                        break;
                    }

                case eVehicleType.ElectricMotorCycle:
                    {
                        newVehicle = new MotorCycle(i_LicenseNumber, eVehicleType.ElectricMotorCycle);
                        break;
                    }

                case eVehicleType.FuelCar:
                    {
                        newVehicle = new Car(i_LicenseNumber, eVehicleType.FuelCar);
                        break;
                    }

                case eVehicleType.ElectricCar:
                    {
                        newVehicle = new Car(i_LicenseNumber, eVehicleType.ElectricCar);
                        break;
                    }

                case eVehicleType.FuelTruck:
                    {
                        newVehicle = new Truck(i_LicenseNumber, eVehicleType.FuelTruck);
                        break;
                    }
            }

            return newVehicle;
        }

        public string Model
        {
            get { return m_Model; }
            set { m_Model = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public Engine EngineSystem
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        public void AddWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            r_WheelsCollection.Clear();

            if (i_CurrentAirPressure <= r_MaxWheelAirPressure)
            {
                for (int i = 0; i < r_NumOfWheels; i++)
                {
                    Wheel newWheel = new Wheel(i_ManufacturerName, r_MaxWheelAirPressure, i_CurrentAirPressure);
                    r_WheelsCollection.Add(newWheel);
                }
            }
            else
            {
                throw new ValueOutOfRangeException(1, r_MaxWheelAirPressure, "Out of range input!");
            }
        }

        public void AddAirToWheels(float i_AirToAdd)
        {
            if (r_WheelsCollection.Count > 0)
            {
                foreach (Wheel currentWheel in r_WheelsCollection)
                {
                    currentWheel.AddAirToWheel(i_AirToAdd);
                }
            }
        }

        public void AddMaxAirToWheels()
        {
            if (r_WheelsCollection.Count > 0)
            {
                foreach (Wheel currentWheel in r_WheelsCollection)
                {
                    currentWheel.AddAirToWheel(currentWheel.MaxAirPressureByManufacturer - currentWheel.CurrentAirPressure);
                }
            }
        }

        public abstract Dictionary<int, string> getVehicleProperties();

        public abstract void SetProperty(int i_Property, string i_InputFromUserStr);

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int wheelNumber = 1;
            stringBuilder.AppendFormat(
@"LicenseNumber : {0}
ModelName {1}: 
", m_LicenseNumber, m_Model);

            foreach (Wheel currentWheel in r_WheelsCollection)
            {
                stringBuilder.AppendFormat(
@"Wheel number :{0} 
{1}
", wheelNumber, currentWheel.ToString());
                wheelNumber++;
            }

            return stringBuilder.ToString() + m_Engine.ToString();
        }

        public static string enterEnumMsg<T>(string i_ValueName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int index = 1;

            stringBuilder.AppendFormat("\nPlease enter {0}{1}", i_ValueName, Environment.NewLine);
            foreach (T currentValue in Enum.GetValues(typeof(T)))
            {
                stringBuilder.AppendFormat("{0} - {1}{2}", index, currentValue.ToString(), Environment.NewLine);
                index++;
            }

            return stringBuilder.ToString();
        }

        public enum eVehicleType
        {
            FuelMotorCycle = 1,
            ElectricMotorCycle = 2,
            FuelCar = 3,
            ElectricCar = 4,
            FuelTruck = 5,
        }

        public enum eFuelVeicle
        {
            FuelMotorCycle = 1,
            FuelCar = 2,
            FuelTruck = 3,
        }
    }
}
