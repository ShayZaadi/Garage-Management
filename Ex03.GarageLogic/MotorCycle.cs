using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class MotorCycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        internal MotorCycle(string i_LicenseNumber, float i_MaxWheelAirPressure, int i_NumOfWheels)
            : base(i_LicenseNumber, i_MaxWheelAirPressure, i_NumOfWheels)
        {
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int Enginecapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public override Dictionary<int, string> getVehicleProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();

            properties.Add((int)eMotorCycleProperties.Model, "\nPlease enter model");
            properties.Add((int)eMotorCycleProperties.LicenseType, Car.enterEnumMsg<eLicenseType>("nPlease enter LicenseNumber type"));
            properties.Add((int)eMotorCycleProperties.EngineCapacity, "\nPlease enter engine capacity");

            return properties;
        }

        public override void SetProperty(int i_Property, string i_InputFromUserStr)
        {
            eMotorCycleProperties property = (eMotorCycleProperties)i_Property;
            int inputFromUserInt;

            switch (property)
            {
                case eMotorCycleProperties.Model:
                    {
                        Model = i_InputFromUserStr;
                        break;
                    }

                case eMotorCycleProperties.EngineCapacity:
                    {
                        if (int.TryParse(i_InputFromUserStr, out inputFromUserInt))
                        {
                            EngineCapacity = inputFromUserInt;
                        }
                        else
                        {
                            throw new FormatException("You have enterd wrong input!");
                        }

                        break;
                    }

                case eMotorCycleProperties.LicenseType:
                    {
                        if (int.TryParse(i_InputFromUserStr, out inputFromUserInt))
                        {
                            if (Enum.IsDefined(typeof(eLicenseType), inputFromUserInt))
                            {
                                LicenseType = (eLicenseType)inputFromUserInt;
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eLicenseType)).Length, "You have enterd out of range input!");
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
@"License Type : {0}
Engine capacity : {1}", m_LicenseType.ToString(), m_EngineCapacity.ToString());

            return base.ToString() + stringBuilder.ToString();
        }

        public int EngineCapacity { get; set; }

        public enum eMotorCycleProperties
        {
            Model = 1,
            LicenseType = 2,
            EngineCapacity = 3
        }

        public enum eLicenseType
        {
            A1 = 1,
            B1 = 2,
            AA = 3,
            BB = 4,
        }
    }
}
