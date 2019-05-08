using BusinessBears.Library;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessBears.Tests
{
    public class BearTest
    {
        private readonly Bear testbear = new Bear(new HashSet<Training>());
        private readonly Bear testbear2 = new Bear();
        [Fact]
        public void BearWithUpgradesCanTakeUpgrades()
        {
            Training t2 = new Training("Walnut Crushing", 27.99);
            testbear.AddTraining(t2);

            //assert
            Assert.NotNull(testbear.upgrades);

        }

        [Fact]
        public void BearWithoutUpgradesCanTakeUpgrades()
        {
            Training t2 = new Training("Walnut Crushing", 27.99);
            Assert.Throws<NullReferenceException>(() => testbear2.AddTraining(t2));
        }

        [Fact]
        public void BearWithoutUpgradesCanGainUpgrades()
        {
            Training t2 = new Training("Walnut Crushing", 27.99);
            HashSet<Training> h = new HashSet<Training> { t2 };
            testbear2.upgrades = h;
            //assert
            Assert.NotNull(testbear2.upgrades);

        }
    }
}