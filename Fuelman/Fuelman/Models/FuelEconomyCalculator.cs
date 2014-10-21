using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuelman.Models
{
    public class FuelEconomyCalculator
    {
        private static FuelEconomyCalculator _Calculator;
        public static FuelEconomyCalculator Calculator 
        {
            get 
            {
                if (_Calculator == null)
                    _Calculator = new FuelEconomyCalculator();
                return _Calculator;
            } 
        }

        public FuelEconomyResult GetFuelEconomy(Vehicle vehicle)
        {
            FuelEconomyResult fer = new FuelEconomyResult();

            float totalFuel = 0.0f;
            float? odometerStart = null;
            foreach (Refill refill in vehicle.Refills.OrderBy(x => x.RefillDate))
            {
                if (!refill.IsFullTank)
                {
                    totalFuel += refill.RefillAmount;
                }
                else
                {
                    // Create a new FuelEconomyResult for this segment.                    
                    if (odometerStart != null)
                    {
                        FuelEconomyEntry fee = new FuelEconomyEntry()
                        {
                            Fuel = totalFuel,
                            Distance = refill.Odometer - (float) odometerStart
                        };
                        fer.Add(fee);
                    }

                    // Reset the odometer start for the next segment.
                    odometerStart = refill.Odometer;
                }
            }

            return fer;
        }
    }

    public class FuelEconomyResult : List<FuelEconomyEntry>
    {
        
    }

    public class FuelEconomyEntry
    {
        public float Fuel { get; set; }

        public float Distance { get; set; }

        public float Economy
        {
            get 
            {
                return this.Distance / this.Fuel;
            }
        }
    }
}