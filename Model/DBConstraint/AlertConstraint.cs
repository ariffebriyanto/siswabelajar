namespace Model.DBConstraint
{
    public class AlertConstraint
    {
        public static class Default
        {
            public const string Loading = "Loading...";
            public const string Error = "Error occurred!";
            public const string Success = "Success!";
            public const string Failed = "Failed!";
            public const string RequiredForm = "Please fill all required fields!";
            public const string ComingSoon = "Coming soon!";
            public const string FailedUploadImage = "Failed to Upload Image!";
            public const string FailedUploadFile = "Failed to Upload File!";
            public const string FileNotFound = "File doesn't exist!";
        }

        public static class Period
        {
            public const string DateNotGreaterThan = "End date should be greater than start date";
            public const string EmptyPeriodName = "Period name must be filled!";
            public const string EmptyDeadlineStart = "Start date must be filled!";
            public const string EmptyDeadlineEnd = "End date must be filled!";
        }

        public static class Login
        {
            public const string LoggingIn = "Logging In...";
            public const string EmptyUsername = "NIM / Username must be filled!";
            public const string UsernameNotFound = "NIM / Username doesn't exist!";
            public const string EmptyPassword = "Password must be filled!";
            public const string InvalidPassword = "Invalid password!";
            public const string InvalidPeriod = "Your account is not in an active recruitment period";
        }

        public static class Registration
        {
            public const string Registering = "Registering...";
            public const string EmptyNIM = "NIM must be filled!";
            public const string NIMNotFound = "NIM doesn't exist!";
            public const string AlreadyRegistered = "NIM is already registered!";
            public const string EmptyEmail = "Active email must be filled!";
            public const string WrongEmailFormat = "Wrong active email format!";
            public const string NonUniEmail = "Please use @uni.ac.id email";
            public const string InsertCandidateUnknownError = "Unknown error while creating candidate.";
            public const string EmptyPosition = "Position must be chosen!";
            public const string RegistrationSuccess = "Registration Success!";
        }

        public static class ModuleConfiguration
        {
            public const string EmptyRole = "Role must be chosen!";
            public const string EmptyModule = "Module name must be filled!";
            public const string EmptyModuleLevel = "Module level must be filled!";
            public const string EmptyRoute = "Route must be filled!";
            public const string RouteNotes = "Notes: The first item will be used as the default route in the sidebar. Please use semicolon ( ; ) as a separator between routes.";
        }

        public static class LogicTest
        {
            public const string NotSelectQuestionType = "Question type must be selected!";
            public const string EmptyQuestionType = "Question type must be filled!";
            public const string ExistQuestionType = "Question type already exist!";
            public const string EmptyQuestion = "Question must be filled!";
            public const string EmptyQuestionUpload = "Question image must be uploaded!";
            public const string EmptyOptionA = "Option A must be filled!";
            public const string EmptyOptionAUpload = "Option A image must be uploaded!";
            public const string EmptyOptionB = "Option B must be filled!";
            public const string EmptyOptionBUpload = "Option B image must be uploaded!";
            public const string EmptyOptionC = "Option C must be filled!";
            public const string EmptyOptionCUpload = "Option C image must be uploaded!";
            public const string EmptyOptionD = "Option D must be filled!";
            public const string EmptyAnswer = "Correct answer must be chosen!";
            public const string EmptyOptionDUpload = "Option D image must be uploaded!";            
            public const string UploadImageError = "Something went wrong when uploading image!";

            public const string LogicTestNotStarted = "Logic test is not started yet!";
            public const string LogicTestDone = "Logic test already finished!";
            public const string LogicTestFinish = "Thank you. We will send the result to you via email soon!";
        }

        public static class Stage
        {
            public const string EmptyStageName = "Stage name must be filled!";
            public const string EmptyStageLevel = "Stage level must be filled!";
        }

        public static class SubStage
        {
            public const string EmptySubStageName = "Substage name must be filled!";
            public const string EmptyStage = "Stage must be chosen!";
        }

        public static class MinimumScore
        {
            public const string EmptyPeriod = "Period must be chosen!";
            public const string EmptyStage = "Stage must be chosen!";
            public const string EmptySubStage = "Substage must be chosen!";
            public const string EmptyMinimumScore = "Minimum score must be filled!";
            public const string AlreadyExists = "Minimum score already exists!";
        }

        public static class ScoringComponent
        {
            public const string EmptyScoringComponentType = "Scoring component type must be filled!";
            public const string ExistScoringComponentType = "Scoring component type already exist!";
            public const string EmptyPeriod = "Period must be chosen!";
            public const string EmptyStage = "Stage must be chosen!";
            public const string EmptySubStage = "Substage must be chosen!";
            public const string EmptyPosition = "Position must be chosen!";
            public const string EmptyScoringComponentTypeList = "Scoring component type must be chosen!";
            public const string EmptyScoringComponent = "Scoring component must be filled!";
            public const string EmptyMinScore = "Min score must be filled!";
            public const string EmptyMaxScore = "Max score must be filled!";
            public const string InvalidMaxScore = "Max score must be higher than min score!";
            public const string EmptyScore = "Score must be filled!";
            public const string InvalidScore = "Score is out of range!";
        }

        public static class Schedule
        {
            public const string EmptyPeriod = "Period must be chosen!";
            public const string EmptyStage = "Stage must be chosen!";
            public const string EmptySubStage = "Substage must be chosen!";
            public const string EmptyPosition = "Position must be chosen at least 1!";
            public const string EmptyDate = "Date must be filled!";            
            public const string EmptyStartTime = "Start time must be filled!";
            public const string EmptyEndTime = "End time must be filled!";
            public const string EmptyRoom = "Room must be filled!";
            public const string EmptyLimit = "Limit must be filled!";
            public const string InvalidEndTime = "End time should be greater than start time!";

            // Candidate
            public const string FullRoom = "Room is full!";
            public const string ScheduleAlreadyStarted = "The schedule already started!";
        }

        public static class Assignment
        {
            public const string EmptyPeriod = "Period must be chosen!";
            public const string EmptyStage = "Stage must be chosen!";
            public const string EmptySubStage = "Substage must be chosen!"; 
            public const string EmptyStartDate = "Start date must be filled!";
            public const string EmptyEndDate = "End date must be filled!";
            public const string EmptyNotes = "Notes must be filled!";
            public const string EmptyFile = "File must be uploaded!";
            public const string InvalidEndDate = "End date should be greater than start date!";
            public const string NoPeriod = "No Active Period Available!";
            public const string NoSchedule = "No Schedule Available!";
            public const string LateSubmission = "Submission Deadline has already passed!";
        }

        public static class User
        {
            public const string UsernameExists = "Username already exists!";
            public const string EmptyUsername = "Username must be filled!";
            public const string EmptyName = "Name must be filled!";
            public const string EmptyEmail = "Email must be filled!";
            public const string EmptyRole = "Role must be chosen!";
            public const string EmptyPassword = "Password must be filled!";
            public const string ConfirmPasswordNotMatch = "Confirm password is not matched!";
            public const string PasswordLength = "Password length must be at least 8 characters!";
            public const string IncorrectOldPassword = "Old password is incorrect!";
            public const string UnauthorizedAccess = "Unauthorized Access!";
        }

        public static class Email
        {
            public const string EmptyRecipients = "Email recipients must be filled!";
            public const string EmptySubject = "Email subject must be filled!";
            public const string EmptyBody = "Email body must be filled!";
        }
    }
}
