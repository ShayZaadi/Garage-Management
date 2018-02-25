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

        internal Vehicle(string i_LicenseNumber, float i_MaxWheelAirPressure, int i_NumOfWheels)
        {
            m_LicenseNumber = i_LicenseNumber;
            r_NumOfWheels = i_NumOfWheels;
            r_MaxWheelAirPressure = i_MaxWheelAirPressure;
            r_WheelsCollection = new List<Wheel>();
        }

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelMotorCycle:
                    {
                        newVehicle = new FuelMotorCycle(i_LicenseNumber);
                        break;
                    }

                case eVehicleType.ElectricMotorCycle:
                    {
                        newVehicle = new ElectricMotorCycle(i_LicenseNumber);
                        break;
                    }

                case eVehicleType.FuelCar:
                    {
                        newVehicle = new FuelCar(i_LicenseNumber);
                        break;
                    }

                case eVehicleType.ElectricCar:
                    {
                        newVehicle = new ElectricCar(i_LicenseNumber);
                        break;
                    }

                case eVehicleType.FuelTruck:
                    {
                        newVehicle = new FuelTruck(i_LicenseNumber);
                        break;
                    }
            }
            return newVehicle;
        }

        public float EnergyPrecentLeft
        {
            get
            {
                return m_Engine.EnergyPrecentLeft;
            }
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

        public virtual Dictionary<int, string> getVehicleProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();

            foreach (eVehicleProperties currentProperty in Enum.GetValues(typeof(eVehicleProperties)))
            {
                properties.Add((int)currentProperty, currentProperty.ToString());
            }

            return properties;
        }

        public virtual void SetProperty(int i_Property, string i_InputFromUserStr)
        {
            eVehicleProperties property = (eVehicleProperties)i_Property;

            switch (property)
            {
                case eVehicleProperties.Model:
                    Model = i_InputFromUserStr;
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int wheelNumber = 1;
            stringBuilder.AppendFormat(
@"LicenseNumber : {0}
ModelName {1}: 
EnergyPercentage :{2}%
", m_LicenseNumber, m_Model, string.Format("{0:0.00}", EnergyPrecentLeft));

            foreach (Wheel currentWheel in r_WheelsCollection)
            {
                stringBuilder.AppendFormat(
@"Wheel number {0} :
{1}
", wheelNumber, currentWheel.ToString());
                wheelNumber++;
            }
            return stringBuilder.ToString() + m_Engine.ToString();
        }

        public enum eVehicleProperties
        {
            Model = 1
        }

        public enum eVehicleType
        {
            FuelMotorCycle = 1,
            ElectricMotorCycle = 2,
            FuelCar = 3,
            ElectricCar = 4,
            FuelTruck = 5,
        }
    }
}
