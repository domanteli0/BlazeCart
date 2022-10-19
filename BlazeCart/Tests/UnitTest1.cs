using Scraper;

namespace Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void IKIDuplicateTest()
    {
        // NOTE: This test should take a long time
        // (aprox. 3min, but depending on the machine may take longer)
        IKIScraper IkiScraper = new IKIScraper();
        IkiScraper.scrape();

        foreach (var store in IkiScraper.Stores)
            Assert.AreEqual(IkiScraper.Stores.FindAll(s => s.InternalID.Equals(store.InternalID)).Count, 1, "Duplicate found  in Stores");
        foreach (var cat in IkiScraper.Categories)
            Assert.AreEqual(IkiScraper.Categories.FindAll(s => s.InternalID.Equals(cat.InternalID)).Count, 1, "Duplicate found in Categories");
        foreach (var cat in IkiScraper.AllCategories)
            Assert.AreEqual(IkiScraper.AllCategories.FindAll(s => s.InternalID.Equals(cat.InternalID)).Count, 1, "Duplicate found in AllCategories");
        foreach (var prod in IkiScraper.Products)
            Assert.AreEqual(IkiScraper.Products.FindAll(s => s.InternalID.Equals(prod.InternalID)).Count, 1, "Duplicate found in Products");
    }
}
