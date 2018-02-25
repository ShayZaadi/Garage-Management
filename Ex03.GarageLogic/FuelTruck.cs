using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal sealed class FuelTruck : Truck
    {
        private const FuelEngine.eFuelType k_FuelType = FuelEngine.eFuelType.Soler;
        private const float k_MaxWheelAirPressure = 34;
        private const int k_NumOfWheels = 12;
        private const float k_MaxFuelAmount = 130f;

        public FuelTruck(string i_LicenseNumber)
            : base(i_LicenseNumber, k_MaxWheelAirPressure, k_NumOfWheels)
        {
            EngineSystem = new FuelEngine(k_MaxFuelAmount, k_FuelType);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Vehicle type : Fuel Truck ");

            return stringBuilder.ToString() + base.ToString();
        }
    }
}
