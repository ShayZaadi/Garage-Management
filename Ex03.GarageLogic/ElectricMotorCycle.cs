﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal sealed class ElectricMotorCycle : MotorCycle
    {
        private const float k_MaxWheelAirPressure = 28;
        private const int k_NumOfWheels = 2;
        private const float k_MaxBatteryTime = 1.6f;

        public ElectricMotorCycle(string i_LicenseNumber)
            : base(i_LicenseNumber, k_MaxWheelAirPressure, k_NumOfWheels)
        {
            EngineSystem = new ElectricEngine(k_MaxBatteryTime);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Vehicle type : Electric Motorbike ");

            return stringBuilder + base.ToString();
        }
    }
}
