using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    
    
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
    private List<Shirt> _shirts;
        [SetUp]
        public void Setup()
        {
            _shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };
        }
        [Test]
        public void Test()
        {

            var searchEngine = new SearchEngine(_shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            Assert.AreEqual(results.Shirts.Count, 1);
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }
        [Test]
        public void TestWithNonExistentShirt()
        {
            var searchEngine = new SearchEngine(_shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

           Assert.AreEqual(results.Shirts.Count,0);
        }
        [Test]
        public void TesEmptyInput()
        {
            Assert.Throws<NullReferenceException>(() => new SearchEngine(_shirts).Search(null));
        }
    }
}
