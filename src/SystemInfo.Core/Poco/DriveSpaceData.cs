namespace SystemInfo.Core.Poco;

/// <summary>
/// Represents the <see cref="UsageData"/> of an hard drive.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DriveSpaceData"/> class.
/// </remarks>
/// <param name="Name">The name of the drive.</param>
/// <param name="Size">The total size.</param>
/// <param name="Available">The available size.</param>
public record class DriveSpaceData(string Name, ulong Size, ulong Available) : UsageData(Size, Available);
