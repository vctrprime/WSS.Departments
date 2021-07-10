namespace WSS.Departments.Web.Infrastructure
{
    /// <summary>
    /// Тексты ошибок
    /// </summary>
    public static class Errors
    {
        public static string RootElementCannotBeDeletedError => "Root element cannot be deleted!";

        public static string ConcurrencyError =>
            "The row does not exist or has been modified by another user. Data will be update, try again after it!";
        
    }
}