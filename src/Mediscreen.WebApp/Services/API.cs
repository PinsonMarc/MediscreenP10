namespace MediscreenWepApp.Services
{
    public static class API
    {
        public static class Patient
        {
            private const string pathSegment = "Patients/";
            public const string create = $"{pathSegment}Create";
            public const string update = $"{pathSegment}Edit";
            public const string readAll = $"{pathSegment}Read";
            public static string Delete(int id) => $"{pathSegment}Delete/{id}";
            public static string Read(int id) => $"{pathSegment}Read/{id}";
        }
        public static class PatHistory
        {
            private const string pathSegment = "PatHistory/";
            public const string create = $"{pathSegment}Add";
            public const string update = $"{pathSegment}Edit";
            public static string Delete(string id) => $"{pathSegment}Delete/{id}";
            public static string Read(string id) => $"{pathSegment}Read/{id}";
            public static string ReadByPatId(int id) => $"{pathSegment}ReadByPatId/{id}";
        }
        public static class Assess
        {
            private const string pathSegment = "Assess/";
            public static string ById(int id) => $"{pathSegment}ById/{id}";
            public static string ByFamilyName(string familyName) => $"{pathSegment}ByFamilyName/{familyName}";
        }
    }
}
