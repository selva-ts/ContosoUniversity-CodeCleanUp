using ContosoUniversity.Models;

namespace ContosoUniversity.Extensions
{
    public static class LoggerExtensions
    {
        private static readonly Action<ILogger, Exception> _indexPageRequested;
        private static readonly Action<ILogger, Student, Exception> _studentAdded;
        private static readonly Action<ILogger, string, int, Exception> _studentDeleted;
        private static readonly Action<ILogger, int, Exception> _studentDeleteFailed;
        private static readonly Func<ILogger, int, IDisposable> _allStudentsDeletedScope;

        static LoggerExtensions()
        {
            _indexPageRequested = LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1, nameof(IndexPageRequested)),
                "GET request for Index page");

            _studentAdded = LoggerMessage.Define<Student>(
                LogLevel.Information,
                new EventId(2, nameof(StudentAdded)),
                "Student added (Student = '{Student}')");

            _studentDeleted = LoggerMessage.Define<string, int>(
                LogLevel.Information,
                new EventId(4, nameof(StudentDeleted)),
                "Student deleted (Student = '{Student}' Id = {Id})");

            _studentDeleteFailed = LoggerMessage.Define<int>(
                LogLevel.Error,
                new EventId(5, nameof(StudentDeleteFailed)),
                "Student delete failed (Id = {Id})");

            _allStudentsDeletedScope =
                LoggerMessage.DefineScope<int>("All students deleted (Count = {Count})");
        }

        public static void IndexPageRequested(this ILogger logger)
        {
            _indexPageRequested(logger, null);
        }

        public static void StudentAdded(this ILogger logger, Student student)
        {
            _studentAdded(logger, student, null);
        }

        public static void StudentModified(this ILogger logger, string id, string priorStudent, string newStudent)
        {
            // Reserve for future feature
        }

        public static void StudentDeleted(this ILogger logger, string student, int id)
        {
            _studentDeleted(logger, student, id, null);
        }

        public static void StudentDeleteFailed(this ILogger logger, int id, Exception ex)
        {
            _studentDeleteFailed(logger, id, ex);
        }

        public static IDisposable AllStudentsDeletedScope(
            this ILogger logger, int count)
        {
            return _allStudentsDeletedScope(logger, count);
        }
    }
}
