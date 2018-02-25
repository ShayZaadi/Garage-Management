using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public sealed class ElectricCar : Car
    {
        private const float k_MaxWheelAirPressure = 32;
        private const int k_NumOfWheels = 4;
        private const float k_MaxBatteryTime = 2.8f;

        public ElectricCar(string i_LicenseNumber)
            : base(i_LicenseNumber, k_MaxWheelAirPressure, k_NumOfWheels)
        {
            EngineSystem = new ElectricEngine(k_MaxBatteryTime);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Vehicle type : Electric Automobile ");

            return stringBuilder + base.ToString();
        }
    }
}
