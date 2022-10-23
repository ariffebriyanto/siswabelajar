using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreAutoLinkDropdown.AutoLinkDropdownBehavior;
using System;
using System.Collections.Generic;

namespace OneStopRecruitment.Helpers.DropdownHelpers
{
    public class SelectListItemBuilder
    {
        private readonly List<SelectListItem> SelectListItems = new List<SelectListItem>();
        private bool DefaultOptionAdded = false;

        [Obsolete]
        public SelectListItemBuilder WithDefaultOption()
        {
            return AddDefaultOption();
        }

        public SelectListItemBuilder AddDefaultOption(SelectListItem selectListItem)
        {
            if (!DefaultOptionAdded)
            {
                SelectListItems.Insert(0, selectListItem);
                DefaultOptionAdded = true;
            }
            return this;
        }

        public SelectListItemBuilder AddSelectListItem(SelectListItem selectListItem)
        {
            SelectListItems.Add(selectListItem);
            return this;
        }

        public SelectListItemBuilder AddDefaultOption()
        {
            if (!DefaultOptionAdded)
            {
                SelectListItems.Insert(0, new SelectListItem("Please choose one", 0.ToString()));
                DefaultOptionAdded = true;
            }
            return this;
        }

        public SelectListItemBuilder AddDefaultOption(string Value, string Text)
        {
            if (!DefaultOptionAdded)
            {
                SelectListItems.Insert(0, new SelectListItem(Text, Value));
                DefaultOptionAdded = true;
            }
            return this;
        }

        public SelectListItemBuilder AddRangeDropdownItems(IEnumerable<DropdownItem> dropdownItems)
        {
            SelectListItems.AddRange(Dropdown.From(dropdownItems));
            return this;
        }

        public IEnumerable<SelectListItem> Build() => SelectListItems;
    }
}
