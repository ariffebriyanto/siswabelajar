using Model.Subdomains.EmailSubdomain;

namespace Service.Helpers.EmailHelper
{
    public interface IEmailHelper
    {
        bool Send(Email email);
    }
}
