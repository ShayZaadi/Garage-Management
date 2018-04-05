using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const float k_MaxWheelAirPressure = 32;
        private const int k_NumOfWheels = 4;
        private const float k_MaxFuelAmount = 50f;
        private const float k_MaxBatteryTime = 2.8f;
        private eCarNumberOfDoors m_NumberOfDoors;
        private eCarColor m_CarColor;

        internal Car(string i_LicenseNumber, eVehicleType i_VehicleType)
            : base(i_LicenseNumber, k_MaxWheelAirPressure, k_NumOfWheels, FuelEngine.eFuelType.Octan98)
        {
            if (i_VehicleType == eVehicleType.FuelCar)
            {
                EngineSystem = new FuelEngine(k_MaxFuelAmount, FuelEngine.eFuelType.Octan98);
            }

            else
            {
                EngineSystem = new ElectricEngine(k_MaxBatteryTime);
            }
            VehicleType = i_VehicleType;
        }

        public eCarNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
        }

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public override void SetProperty(int i_Property, string i_InputFromUserStr)
        {
            eCarProperties property = (eCarProperties)i_Property;
            int inputFromUserInt;

            switch (property)
            {
                case eCarProperties.Model:
                    {
                        Model = i_InputFromUserStr;
                        break;
                    }

                case eCarProperties.Color:
                    {
                        if (int.TryParse(i_InputFromUserStr, out inputFromUserInt))
                        {
                            if (Enum.IsDefined(typeof(eCarColor), inputFromUserInt))
                            {
                                CarColor = (eCarColor)inputFromUserInt;
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eCarColor)).Length, "You have enterd out of range input!");
                            }
                        }
                        else
                        {
                            throw new FormatException("You have enterd wrong input!");
                        }

                        break;
                    }

                case eCarProperties.NumberOfDoors:
                    {
                        if (int.TryParse(i_InputFromUserStr, out inputFromUserInt))
                        {
                            if (Enum.IsDefined(typeof(eCarNumberOfDoors), inputFromUserInt))
                            {
                                NumberOfDoors = (eCarNumberOfDoors)inputFromUserInt;
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eCarNumberOfDoors)).Length, "You have enterd out of range input!");
                            }
                        }
                        else
                        {
                            throw new FormatException("You have enterd wrong input!");
                        }

                        break;
                    }
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat(
@"Car color : {0}
Number of doors : {1}
Vehicle type : {2}
", m_CarColor.ToString(), NumberOfDoors.ToString(), VehicleType.ToString());

            return base.ToString() + stringBuilder.ToString();
        }

        public override Dictionary<int, string> getVehicleProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();

            properties.Add((int)eCarProperties.Model, "\nPlease enter model");
            properties.Add((int)eCarProperties.Color, Vehicle.enterEnumMsg<eCarColor>("car color"));
            properties.Add((int)eCarProperties.NumberOfDoors, Vehicle.enterEnumMsg<eCarNumberOfDoors>("number of doors"));

            return properties;
        }

        public enum eCarProperties
        {
            Model = 1,
            Color = 2,
            NumberOfDoors = 3
        }

        public enum eCarNumberOfDoors
        {
            TwoDoors = 1,
            ThreeDoors = 2,
            FourDoors = 3,
            FiveDoors = 4,
        }

        public enum eCarColor
        {
            Green = 1,
            Silver = 2,
            White = 3,
            Black = 4,
        }
    }
}
