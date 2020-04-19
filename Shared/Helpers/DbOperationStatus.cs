namespace Shared.Helpers
{
    /// <summary>
    /// These are the db operation statuses which is useful inside the api
    /// structure to distinguise the operation status
    /// </summary>
    public enum DbStatusCode
    {
        Exception,
        DbError,
        NotFound,
        Created,
        Updated,
        Deleted
    }
}
