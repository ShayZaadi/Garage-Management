﻿using System;
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

            stringBuilder.AppendLine("Fuel Type : " + m_FuelType.ToString());

            return stringBuilder.ToString();
        }

        public override Dictionary<int, string> GetEngineProperties()
        {
            Dictionary<int, string> properties = new Dictionary<int, string>();

            properties.Add(1, Car.enterEnumMsg<eFuelType>("fuel type"));
            properties.Add(2, "Please enter fuel amount");

            return properties;
        }

        public override void SetProperty(int i_Property, string i_InputFromUserStr)
        {
            eFuelEngineProperties property = (eFuelEngineProperties)i_Property;
            float inputFromUserFloat;
            int inputFromUserInt;

            switch (property)
            {
                case eFuelEngineProperties.FuelType:
                    {
                        if (int.TryParse(i_InputFromUserStr, out inputFromUserInt))
                        {
                            if (Enum.IsDefined(typeof(eFuelType), inputFromUserInt))
                            {
                                if ((eFuelType)inputFromUserInt != m_FuelType)
                                {
                                    throw new ArgumentException("Wrong fuel type!");
                                }
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eFuelType)).Length, "You have enterd out of range input!");
                            }
                        }
                        else
                        {
                            throw new FormatException("You have enterd wrong input!");
                        }

                        break;
                    }

                case eFuelEngineProperties.CurrentAmount:
                    {
                        if (float.TryParse(i_InputFromUserStr, out inputFromUserFloat) && (inputFromUserFloat > 0))
                        {
                            CurrenteEnergy = inputFromUserFloat;
                        }
                        else
                        {
                            throw new FormatException("You have enterd wrong input!");
                        }

                        break;
                    }
            }
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
