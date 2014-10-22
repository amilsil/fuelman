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
            bool fullTankFoundPreviously= false;
            foreach (Refill refill in vehicle.Refills.OrderBy(x => x.RefillDate))
            {
                if (odometerStart == null)
                {
                    // From first full tank only we can start calculating fuel economy.
                    if (refill.IsFullTank)
                        odometerStart = refill.Odometer;
                }
                else
                    totalFuel += refill.RefillAmount;

                // If this one is a full tank, we can calculate the economy.
                if (refill.IsFullTank)
                {
                    if (fullTankFoundPreviously)
                    {
                        // If previous one is a full tank, and this one also
                        // Calcualte the economy.
                        FuelEconomyEntry fee = new FuelEconomyEntry()
                        {
                            Fuel = totalFuel,
                            Distance = refill.Odometer - (float)odometerStart
                        };
                        fer.Add(fee);

                        // Reset the values for next calc.
                        totalFuel = 0.0f;
                        odometerStart = refill.Odometer;
                    }
                    fullTankFoundPreviously = true;
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