using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        internal ElectricEngine(float i_MaxHoursOfBattary)
            : base(i_MaxHoursOfBattary)
        {
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("EnergyPercentage : {0}% ", string.Format("{0:0.00}", EnergyPrecentLeft));
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}
