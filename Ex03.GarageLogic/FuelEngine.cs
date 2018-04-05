using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eFuelType m_FuelType;

        internal FuelEngine(float i_MaxFuelAmount, eFuelType i_FuelType)
            : base(i_MaxFuelAmount)
        { 
            m_FuelType = i_FuelType;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat(
@"EnergyPercentage : {0}% 
Fuel Type : {1}", string.Format("{0:0.00}", EnergyPrecentLeft), m_FuelType.ToString());
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }

        public enum eFuelEngineProperties
        {
            FuelType = 1,
            CurrentAmount = 2
        }

        public enum eFuelType
        {
            Soler = 1,
            Octan95 = 2,
            Octan96 = 3,
            Octan98 = 4
        }
    }
}
