using Sample.Core.Enums;

namespace Sample.Web.Infrastructure.Identity
{
    public abstract class AdminClaimType : Enumeration
    {
        public static readonly AdminClaimType UserId = new UserIdType();
        //public static readonly AdminClaimType ClientId = new ClientIdType();

        private AdminClaimType(int value, string displayName) : base(value, displayName)
        {

        }

        public abstract string Auth0Key { get; }
        public abstract string DefaultValue { get; }

        private class UserIdType : AdminClaimType
        {
            public UserIdType() : base(1, "UserId")
            {
            }

            public override string Auth0Key => "accountId";
            public override string DefaultValue => "0";
        }

        //private class ClientIdType : AdminClaimType
        //{
        //    //todo
        //}
    }
}
