using System.Collections.Generic;
using System.Linq;

namespace GildedRoseTask
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void DegradeQuality(int points, Item item)
        {
            if (item.Quality > 0)
                if (item.Quality < points)
                    item.Quality = 0;
                else
                    item.Quality -= points;
        }

        public void IncreaseQuality(int points, Item item)
        {
            if (item.Quality < 50)
                if (item.Quality + points > 50)
                    item.Quality = 50;
                else
                    item.Quality += points;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name.Contains("Sulfuras"))
                {
                    continue;
                }
                else if (item.Name.Contains("Aged Brie"))
                {
                    IncreaseQuality(1, item);
                }
                else if (item.Name.Contains("Backstage passes"))
                {
                    if (item.SellIn == 0)
                        item.Quality = 0;
                    else if (item.SellIn < 6)
                        IncreaseQuality(3, item);
                    else if (item.SellIn < 11)
                        IncreaseQuality(2, item);
                    else if (item.SellIn > 10)
                        IncreaseQuality(1, item);
                }
                else if (item.Name.Contains("Conjured"))
                {
                    if (item.SellIn > 0)
                        DegradeQuality(2, item);
                    else if (item.SellIn == 0)
                        DegradeQuality(4, item);
                }
                else
                {
                    if (item.SellIn > 0)
                        DegradeQuality(1, item);
                    else if (item.SellIn == 0)
                        DegradeQuality(2, item);
                }

                if (item.SellIn > 0)
                    item.SellIn -= 1;
            }
        }
    }
}
