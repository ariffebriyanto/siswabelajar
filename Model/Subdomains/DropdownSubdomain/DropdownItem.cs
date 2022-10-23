namespace Model.Subdomains.DropdownSubdomain
{
    public class DropdownItem : NetCoreAutoLinkDropdown.AutoLinkDropdownBehavior.DropdownItem
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool DropdownItemEnabled() => true;
        public string DropdownValue() => Value;
        public string DropdownText() => Text;
    }
}