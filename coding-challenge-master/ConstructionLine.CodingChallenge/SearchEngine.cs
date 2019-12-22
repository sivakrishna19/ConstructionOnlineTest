using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

        }

        public SearchResults Search(SearchOptions searchOptions)
        {
            // TODO: search logic goes here.

            var filteredShirts = _shirts.Where(x => searchOptions.Colors.Any(color => color.Id.Equals(x.Color.Id))
                                                    && searchOptions.Sizes.Any(size => size.Id.Equals(x.Size.Id))).ToList();
            var groupedSizes = GroupedSizes(filteredShirts, searchOptions);
            var groupedColors = GroupedColors(filteredShirts, searchOptions);

            var result = new SearchResults()
            {
                Shirts = filteredShirts,
                SizeCounts = groupedSizes,
                ColorCounts = groupedColors
            };
            return result;
        }

        private List<SizeCount> GroupedSizes(List<Shirt> filteredShirts, SearchOptions searchOptions)
        {
            var groupedSizes = filteredShirts.GroupBy(a => a.Size.Id).Select(x => new SizeCount
            {
                Size = x.First().Size,
                Count = x.Count()
            }).ToList();
            foreach (var size in Size.All.Where(siz => groupedSizes.All(a => a.Size.Id != siz.Id)))
            {
                groupedSizes.Add(new SizeCount
                {
                    Size = size,
                    Count = _shirts.Count(a => a.Size.Id == size.Id && searchOptions.Colors.Select(c => c.Id).Contains(a.Color.Id))
                });
            }
            return groupedSizes;
        }

        private List<ColorCount> GroupedColors(IEnumerable<Shirt> filteredShirts, SearchOptions searchOptions)
        {
            var groupedColors = filteredShirts.GroupBy(a => a.Color.Id).Select(x => new ColorCount
            {
                Color = x.First().Color,
                Count = x.Count()
            }).ToList();
            
            foreach (var color in Color.All.Where(color => groupedColors.All(a => a.Color.Id != color.Id)))
            {
                groupedColors.Add(new ColorCount
                {
                    Color = color,
                    Count = _shirts.Count(c => c.Color.Id == color.Id && (!searchOptions.Sizes.Any() || searchOptions.Sizes.Select(s => s.Id).Contains(c.Size.Id)))
                });
            }
            return groupedColors;
        }
    }
}
