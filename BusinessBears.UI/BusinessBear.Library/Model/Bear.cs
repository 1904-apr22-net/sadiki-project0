using BusinessBears.Library.Abstracts;
using System;
using System.Collections.Generic;

namespace BusinessBears.Library
{


    /// <summary>
    /// The Bear Class is the primary product subclass. Contains a Hashset used to 
    /// hold additional products sold alongside the bear
    /// </summary>
    public class Bear : Product
    {
        public Bear()
        {
            this._name = "Bear";
        }

        public Bear(HashSet<Training> t)
        {
            this._name = "Bear";
            this.upgrades = t;
        }

        protected readonly double _price = 199.99;
        public HashSet<Training> upgrades;
        
        /// <summary>
        /// Used to add Training objects to the bear. Used for order processing
        /// </summary>
        /// <param name="training">It takes a traing object in order to add it to the upgrades Hashset</param>
        public void AddTraining(Training training)
        {
            upgrades.Add(training);
        }
        /// <summary>
        /// Bear.getPrice() returns the total cost of the bear, accounting for any training the bear will receive as part of the purchase
        /// </summary>
        /// <returns></returns>
        public override double getPrice()
        {
            double d = this._price;
            foreach (Training item in upgrades)
            {
                d += item.getPrice();
            }
            return d;
        }

    }
        }

  
