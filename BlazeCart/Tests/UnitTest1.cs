using Scraper;
using Common;

namespace Tests;

[TestClass]
public class UnitTest1
{

    private class A
    {
        public int B { get; set; }
        public int C { get; set; }
    }

    [TestMethod]
    public void UpdateOrAddByPropertyTest()
    {
        var a0 = new A() { B = 1, C = 2, };
        var a1 = new A() { B = 3, C = 4, };
        var a2 = new A() { B = 1, C = 40, };

        var list = new List<A>(new A[] { a0, a1 });

        list.UpdateOrAddByProperty(a2, "B");

        Assert.AreEqual(list.Contains(a0), false);
        Assert.AreEqual(list.Contains(a1), true);
        Assert.AreEqual(list.Contains(a2), true);
    }

    [TestMethod()]
    public async Task BarboraDuplicateTest()
    {
        // NOTE: This test should take a long time
        // (aprox. 3min, but depending on the machine may take longer)
        BarboraScraper scraper = new();
        await scraper.Scrape();

        //foreach (var store in scraper.Stores)
        //    Assert.AreEqual(scraper.Stores.FindAll(s => s.InternalID.Equals(store.InternalID)).Count, 1, "Duplicate found  in Stores");
        foreach (var cat in scraper.Categories)
            Assert.AreEqual(scraper.Categories.FindAll(s => s.InternalID!.Equals(cat.InternalID)).Count, 1, "Duplicate found in Categories");
        foreach (var item in scraper.Items)
            Assert.AreEqual(scraper.Items.FindAll(s => s.InternalID.Equals(item.InternalID)).Count, 1, "Duplicate found in Products");
    }

    [TestMethod()]
    public async Task IKIDuplicateTest()
    {
        // NOTE: This test should take a long time
        // (aprox. 3min, but depending on the machine may take longer)
        IKIScraper IkiScraper = new();
        await IkiScraper.Scrape();

        foreach (var store in IkiScraper.Stores)
            Assert.AreEqual(IkiScraper.Stores.FindAll(s => s.Equals(store)).Count, 1, "Duplicate found  in Stores");
        foreach (var cat in IkiScraper.Categories)
            Assert.AreEqual(IkiScraper.Categories.FindAll(s => s.Equals(cat)).Count, 1, "Duplicate found in Categories");
        foreach (var item in IkiScraper.Items)
            Assert.AreEqual(IkiScraper.Items.FindAll(s => s.Equals(item)).Count, 1, "Duplicate found in Products");
    }
}
