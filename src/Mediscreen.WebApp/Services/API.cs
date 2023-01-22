namespace MediscreenWepApp.Services
{
    public static class API
    {
        public static class Patient
        {
            private const string pathSegment = "Patients/";
            public const string create = $"{pathSegment}Create";
            public const string update = $"{pathSegment}Edit";
            public const string readAll = pathSegment;
            public static string Delete(int id) => $"{pathSegment}Delete/{id}";
            public static string Read(int id) => $"{pathSegment}{id}";
        }
    }
}
