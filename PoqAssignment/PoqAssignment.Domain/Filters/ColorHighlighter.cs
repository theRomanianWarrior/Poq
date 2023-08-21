using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PoqAssignment.Domain.Abstractions;
using PoqAssignment.Domain.Enums;
using PoqAssignment.Domain.Helpers;
using PoqAssignment.Domain.Models.MockyIo;

namespace PoqAssignment.Domain.Filters
{
    public class ColorHighlighter : BaseDataProcessor
    {
        private readonly List<Highlight> _colors;

        public ColorHighlighter(List<Highlight> colors)
        {
            _colors = colors;
        }

        public override IEnumerable<Product> Process(IEnumerable<Product> data)
        {
            if (_colors.Any())
            {
                var dataList = data.ToList();

                foreach (var product in dataList)
                foreach (var color in _colors)
                {
                    var colorName = color.GetEnumMemberValue();
                    var regexPattern = $@"\b{Regex.Escape(colorName)}\b";
                    var replacement = $"<em>{colorName}</em>";

                    if (!product.Description.Contains(replacement))
                        product.Description = Regex.Replace(product.Description, regexPattern, replacement,
                            RegexOptions.IgnoreCase);
                }

                return base.Process(dataList);
            }

            return base.Process(data);
        }
    }
}