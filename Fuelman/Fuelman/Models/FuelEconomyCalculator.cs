using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fuelman.Models
{
    /// <summary>
    /// A singleton service that calculates the FuelEconomy.
    /// </summary>
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
                // We can only start calculating fuel economy after the first fuel full tank fill.
                // That's where the mileage start should be.
                // Then onwards we should accumulate the fuel refilled, until another full tank fill.
                if (odometerStart == null)
                {
                    if (refill.IsFullTank)
                        odometerStart = refill.Odometer;
                }
                else
                    totalFuel += refill.RefillAmount;

                // If we have had two full tank fills, then we can calculate the fuel economy
                // between them.
                if (refill.IsFullTank)
                {
                    if (fullTankFoundPreviously)
                    {
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

    /// <summary>
    /// FuelEconomyResult is an Enumeration of FuelEconomyEntry s.
    /// </summary>
    public class FuelEconomyResult : List<FuelEconomyEntry>
    {
        
    }

    /// <summary>
    /// A fuel economy calculation result between two full tank refills.
    /// </summary>
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