using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private readonly float r_MaxEnergy;
        private float m_CurrentEnergy;

        internal Engine(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }

        public float CurrenteEnergy
        {
            get 
            {
                return m_CurrentEnergy; 
            }

            set
            {
                if (value <= r_MaxEnergy)
                {
                    m_CurrentEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(1, r_MaxEnergy, "You have entered out of range input!");
                }
            }
        }

        public float MaxEngineEnergy
        {
            get { return r_MaxEnergy; }
        }

        public float EnergyPrecentLeft
        {
            get
            {
                return (m_CurrentEnergy * 100) / r_MaxEnergy;
            }
        }
    }
}
