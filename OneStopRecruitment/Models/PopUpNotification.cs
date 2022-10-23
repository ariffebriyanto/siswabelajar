namespace OneStopRecruitment.Models
{
    public class PopUpNotification
    {
        public string Type { get; set; }
        public string Message { get; set; }

        private PopUpNotification()
        {

        }

        private PopUpNotification(string Type, string Message)
        {
            this.Type = Type;
            this.Message = Message;
        }

        public static PopUpNotification Notify(string Type, string Message)
        {
            return new PopUpNotification(Type, Message);
        }
    }
}