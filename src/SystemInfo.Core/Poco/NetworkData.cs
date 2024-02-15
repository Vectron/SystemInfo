namespace SystemInfo.Core.Poco;

/// <summary>
/// Container for network status.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="NetworkData"/> class.
/// </remarks>
/// <param name="Name">THe name of the connection.</param>
/// <param name="Send">The amount send.</param>
/// <param name="Received">The amount received.</param>
public record class NetworkData(string Name, ulong Send, ulong Received);
