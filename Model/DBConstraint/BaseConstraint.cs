namespace Model.DBConstraint
{
    public class IntObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StringObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class BaseConstraint
    {
        public static class Regex
        {
            public const string Email = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            public const string Empty = @"";
            public const string NonMandatoryIntDropdown = @"^(-1|[1-9][0-9]*)$";
        }

        public static class Uniian
        {
            public const string EmailEnds = "@uni.ac.id";
        }

        public static class OptionType
        {
            public const string Text = "text";
            public const string Image = "image";
        }

        public static class NotificationType
        {
            public const string Success = "SUCCESS";
            public const string Failed = "FAILED";
            public const string Loading = "LOADING";
        }

        public static class StsRc
        {
            public const char Active = 'A';
            public const char Delete = 'D';
            public const char Inactive = 'I';
        }

        public static class Bool
        {
            public const char Yes = 'Y';
            public const char No = 'N';
        }

        public static class Role
        {
            public static IntObject Staff = new IntObject
            {
                Id = 1,
                Name = "Staff"
            };

            public static IntObject Candidate = new IntObject
            {
                Id = 2,
                Name = "Candidate"
            };

            public static IntObject Trainer = new IntObject
            {
                Id = 3,
                Name = "Trainer"
            };
        }

        public static class LogicTest
        {
            public const int NotStarted = 1;
            public const int OnGoing = 2;
            public const int Done = 3;
        }

        public static class Assignemnt
        {
            public const string Completed = "Completed";
            public const string NotCompleted = "Not Completed";
        }
    }
}
