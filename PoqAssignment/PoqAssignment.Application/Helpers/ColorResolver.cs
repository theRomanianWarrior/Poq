using System;
using System.Collections.Generic;
using AutoMapper;
using PoqAssignment.Application.DTO;
using PoqAssignment.Domain.Enums;
using PoqAssignment.Domain.Models.Filters;

namespace PoqAssignment.Application.Helpers
{
    public class ColorResolver : IValueResolver<FilterByUser, UserFilter, List<Highlight>>
    {
        public List<Highlight> Resolve(FilterByUser source, UserFilter destination, List<Highlight> destMember,
            ResolutionContext context)
        {
            if (source.Highlight != null)
            {
                var colorNames = source.Highlight.Split(',');
                var colors = new List<Highlight>();

                foreach (var colorName in colorNames)
                    if (Enum.TryParse(colorName, true, out Highlight color))
                        colors.Add(color);

                return colors;
            }

            return new List<Highlight>();
        }
    }
}