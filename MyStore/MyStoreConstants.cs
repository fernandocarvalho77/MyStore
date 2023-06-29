namespace MyStore;

public class MyStoreConstants
{
    public readonly struct EMAIL
    {
        public static readonly string EMAIL_TEMPLATES_FOLDER = "EmailTemplates";
        public static readonly string TOMORROW_APPOINTMENT_EMAIL_TEMPLATE = "tomorrow_appointment.html";
    }

    public readonly struct Roles
    {
        public static readonly string ADMINISTRATOR = "administrator";
        public static readonly string WORKER = "worker";
    }

    public readonly struct Users
    {
        public readonly struct Administrator
        {
            public static readonly string USERNAME = "admin@mystore.pt";

            public static readonly string PASSWORD = "1234Qwe.";
        }

        public readonly struct Worker
        {
            public static readonly string USERNAME = "worker@mystore.pt";

            public static readonly string PASSWORD = "1234Qwe_";
        }
    }

    public readonly struct Policies
    {
        public readonly struct AppPolicyAdmin
        {
            public const string NAME = "AppPolicyAdmin";

            public static readonly string[] AppPolicyRoles =
            {
                Roles.ADMINISTRATOR,
                Roles.WORKER
            };
        }

        public readonly struct AppPolicyWorker
        {
            public const string NAME = "AppPolicyWorker";

            public static readonly string[] AppPolicyRoles =
            {
                Roles.WORKER
            };
        }
    }
}