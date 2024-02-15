namespace SystemInfo.Core.ViewModels
{
    /// <summary>
    /// A marker class for view models.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Gets or sets a setting object for this view model.
        /// </summary>
        object? Settings
        {
            get;
            set;
        }
    }
}
