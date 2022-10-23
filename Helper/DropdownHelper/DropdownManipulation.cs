namespace Helper.DropdownHelper
{
    public class DropdownManipulation
    {
        public static int NormalizeEmptyDropdown(int Value)
        {
            if (Value == -1)
            {
                return 0;
            }

            return Value;
        }
    }
}
