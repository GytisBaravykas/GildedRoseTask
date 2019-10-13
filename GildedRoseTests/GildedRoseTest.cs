using GildedRoseTask;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Fact]
        public void SellInIsDecreased()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "Banana", SellIn = 3, Quality = 2 },
                new Item { Name = "Sulfuras, Holy Water", SellIn = 1, Quality = 80 },
                new Item { Name = "Aged Brie with lemons", SellIn = 10, Quality = 20 },
                new Item { Name = "Backstage passes to FallOut Boy in Kaunas", SellIn = 12, Quality = 30 },
                new Item { Name = "Conjured fish", SellIn = 10, Quality = 5 },
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].SellIn == 2);
            Assert.True(Items[1].SellIn == 1);
            Assert.True(Items[2].SellIn == 9);
            Assert.True(Items[3].SellIn == 11);
            Assert.True(Items[4].SellIn == 9);
        }

        [Fact]
        public void SellInIsNotDecreased()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "Banana", SellIn = 0, Quality = 2 },
                new Item { Name = "Sulfuras, Holy Water", SellIn = 2, Quality = 80 },
                new Item { Name = "Aged Brie with lemons", SellIn = 0, Quality = 20 },
                new Item { Name = "Backstage passes to FallOut Boy in Kaunas", SellIn = 0, Quality = 30 },
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].SellIn == 0);
            Assert.True(Items[1].SellIn == 2);
            Assert.True(Items[2].SellIn == 0);
            Assert.True(Items[3].SellIn == 0);
        }

        [Fact]
        public void DegradeAfterSellDate()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "Banana", SellIn = 0, Quality = 8 },
                new Item { Name = "Sulfuras, Holy Water", SellIn = 0, Quality = 80 },
                new Item { Name = "Aged Brie with lemons", SellIn = 0, Quality = 20 },
                new Item { Name = "Backstage passes to FallOut Boy in Kaunas", SellIn = 0, Quality = 30 },
                new Item { Name = "Conjured fish", SellIn = 0, Quality = 6 }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].Quality == 6);
            Assert.True(Items[1].Quality == 80);
            Assert.False(Items[2].Quality == 20);
            Assert.True(Items[3].Quality == 0);
            Assert.True(Items[4].Quality == 2);
        }

        [Fact]
        public void BackstagePassesLessThanElevenDays()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "Backstage passes to FallOut Boy in Kaunas", SellIn = 10, Quality = 36 },
                new Item { Name = "Backstage passes to Maroon 5 in Kaunas", SellIn = 7, Quality = 30 },
                new Item { Name = "Backstage passes to Rat City in Kaunas", SellIn = 5, Quality = 40 },
                new Item { Name = "Backstage passes to Queen in Kaunas", SellIn = 0, Quality = 50 },
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].Quality == 38);
            Assert.True(Items[1].Quality == 32);
            Assert.False(Items[2].Quality == 42);
            Assert.False(Items[3].Quality == 52);
        }

        [Fact]
        public void BackstagePassesLessThanSixDays()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "Backstage passes to FallOut Boy in Kaunas", SellIn = 1, Quality = 36 },
                new Item { Name = "Backstage passes to Maroon 5 in Kaunas", SellIn = 5, Quality = 30 },
                new Item { Name = "Backstage passes to Rat City in Kaunas", SellIn = 10, Quality = 40 },
                new Item { Name = "Backstage passes to Queen in Kaunas", SellIn = 0, Quality = 50 },
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].Quality == 39);
            Assert.True(Items[1].Quality == 33);
            Assert.False(Items[2].Quality == 43);
            Assert.False(Items[3].Quality == 53);
        }

        [Fact]
        public void QualityNeverMoreThanFifty()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "Aged Brie with lemons", SellIn = 4, Quality = 49 },
                new Item { Name = "Backstage passes to FallOut Boy in Kaunas", SellIn = 10, Quality = 46 }
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            app.UpdateQuality();
            app.UpdateQuality();

            Assert.True(Items[0].Quality <= 50);
            Assert.True(Items[1].Quality <= 50);
        }

        [Fact]
        public void QualityNeverLessThanZero()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "Banana", SellIn = 1, Quality = 3 },
                new Item { Name = "Conjured milk", SellIn = 2, Quality = 3}
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            app.UpdateQuality();
            app.UpdateQuality();

            Assert.False(Items[0].Quality < 0);
            Assert.False(Items[1].Quality < 0);
        }

        [Fact]
        public void ConjuredDegradeDouble()
        {
            IList<Item> Items = new List<Item> {
                new Item { Name = "Conjured Banana", SellIn = 0, Quality = 8 },
                new Item { Name = "Conjured milk", SellIn = 2, Quality = 4}
            };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();

            Assert.True(Items[0].Quality == 4);
            Assert.True(Items[1].Quality == 2);
        }
    }
}
