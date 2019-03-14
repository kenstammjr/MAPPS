
namespace MAPPS{
    public static class Alert {
        public const string DELETE_CONFIRMATION = "return confirm('Are you sure you want to delete this item?');";
        public const string DELETE_DEFAULT_ITEM = "alert('Default item cannot be deleted. \\nAnother item must be set to default prior to deleting.');";
        public const string MISSING_APP_SETTING = "alert('This action cannot be completed.\\nThe following application setting is missing or not configured: \\n\\n {0}');";
        public const string PURGE_CONFIRMATION = "return confirm('Are you sure you want to purge all records for the selected view?');";
    }
}
