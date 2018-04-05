using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_isCarryingDangerousMaterials;
        private float m_MaxCarryWeight;
        private const float k_MaxWheelAirPressure = 34;
        private const int k_NumOfWheels = 12;
        private const float k_MaxFuelAmount = 130f;

        internal Truck(string i_LicenseNumber, eVehicleType i_VehicleType)
            : base(i_LicenseNumber, k_MaxWheelAirPressure, k_NumOfWheels, FuelEngine.eFuelType.Soler)
        {
            if (i_VehicleType == eVehicleType.FuelTruck)
            {
                EngineSystem = new FuelEngine(k_MaxFuelAmount, FuelEngine.eFuelType.Soler);
            }   
         
            VehicleType = i_VehicleType;
        }

        public bool IsCarryingDangerousMaterials
        {
            get { return IsCarryingDangerousMaterials; }
            set { m_isCarryingDangerousMaterials = value; }
        }

        public float MaxCarryWeight
        {
            get { return m_MaxCarryWeight; }
            set { m_MaxCarryWeight = value; }
        }

        public override Dictionary<int, string> getVehicleProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();

            properties.Add((int)eTruckProperties.Model, "\nPlease enter model");
            properties.Add((int)eTruckProperties.IsCarryingDangerousMaterials, "\nCarry dangerous materials?. 1 - YES, 2 - NO");
            properties.Add((int)eTruckProperties.MaxCarryWeight, "\nPlease enter max carry weight");

            return properties;
        }

        public override void SetProperty(int i_Property, string i_InputFromUserStr)
        {
            eTruckProperties property = (eTruckProperties)i_Property;
            int inputFromUserInt;
            float inputFromUserFloat;

            switch (property)
            {
                case eTruckProperties.Model:
                    {
                        Model = i_InputFromUserStr;
                        break;
                    }

                case eTruckProperties.IsCarryingDangerousMaterials:
                    {
                        if (int.TryParse(i_InputFromUserStr, out inputFromUserInt))
                        {
                            if (inputFromUserInt == 1)
                            {
                                IsCarryingDangerousMaterials = true;
                            }
                            else if (inputFromUserInt == 2)
                            {
                                IsCarryingDangerousMaterials = false;
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(1, 2, "Please enter 1 - YES, 2 - NO");
                            }
                        }
                        else
                        {
                            throw new FormatException("You have enterd wrong input!");
                        }

                        break;
                    }

                case eTruckProperties.MaxCarryWeight:
                    {
                        if (float.TryParse(i_InputFromUserStr, out inputFromUserFloat))
                        {
                            MaxCarryWeight = inputFromUserFloat;
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
 @"Max carry weight  :{0}
Contains dangerous goods : {1}
Vehicle type : {2}
", m_MaxCarryWeight.ToString(), m_isCarryingDangerousMaterials.ToString(), VehicleType.ToString());

            return stringBuilder.ToString() + base.ToString();
        }

        public enum eTruckProperties
        {
            Model = 1,
            IsCarryingDangerousMaterials = 2,
            MaxCarryWeight = 3,
        }
    }
}
