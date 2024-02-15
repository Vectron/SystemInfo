namespace SystemInfo.Core.Poco;

/// <summary>
/// Represent usage data of an resource.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UsageData"/> class.
/// </remarks>
/// <param name="Size">The total size.</param>
/// <param name="Available">The total amount available.</param>
public record class UsageData(ulong Size, ulong Available)
{
    /// <summary>
    /// Gets the available amount in percentages.
    /// </summary>
    public float Percentage
        => Size == 0 ? 0 : (Size - Available) / (float)Size * 100;

    /// <summary>
    /// Gets the used size of this resource.
    /// </summary>
    public ulong TotalUsed
        => Size - Available;
}
