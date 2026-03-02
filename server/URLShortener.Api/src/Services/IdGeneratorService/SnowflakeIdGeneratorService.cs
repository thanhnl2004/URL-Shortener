namespace URLShortener.Api.Services;

/// <summary>
/// Twitter Snowflake-inspired unique ID generator.
/// Produces 64-bit IDs: [1 bit unused | 41 bits timestamp | 10 bits machine ID | 12 bits sequence]
/// </summary>
public class SnowflakeIdGeneratorService : IIdGeneratorService
{
    private const long Epoch = 1704067200000L; // Jan 1, 2024 UTC in milliseconds

    private const int MachineIdBits = 10;
    private const int SequenceBits = 12;

    private const long MaxMachineId = (1L << MachineIdBits) - 1;  // 1023
    private const long MaxSequence = (1L << SequenceBits) - 1;    // 4095

    private const int MachineIdShift = SequenceBits;               // 12
    private const int TimestampShift = MachineIdBits + SequenceBits; // 22

    private readonly long _machineId;
    private readonly object _lock = new();

    private long _lastTimestamp = -1L;
    private long _sequence = 0L;

    public SnowflakeIdGeneratorService(long machineId = 1)
    {
        if (machineId is < 0 or > MaxMachineId)
        {
            throw new ArgumentException($"Machine ID must be between 0 and {MaxMachineId}");
        }
        _machineId = machineId;
    }

    public long NextId()
    {
        lock (_lock)
        {
            var timestamp = GetCurrentTimestamp();

            if (timestamp == _lastTimestamp)
            {
                // Same millisecond - increment sequence
                _sequence = (_sequence + 1) & MaxSequence;
                if (_sequence == 0)
                {
                    // Sequence exhausted - wait for next millisecond
                    timestamp = WaitNextMillis(_lastTimestamp);
                }
            }
            else
            {
                // New millisecond - reset sequence
                _sequence = 0;
            }

            _lastTimestamp = timestamp;

            return ((timestamp - Epoch) << TimestampShift)
                   | (_machineId << MachineIdShift)
                   | _sequence;
        }
    }

    private static long GetCurrentTimestamp()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    private static long WaitNextMillis(long lastTimestamp)
    {
        var timestamp = GetCurrentTimestamp();
        while (timestamp <= lastTimestamp)
        {
            timestamp = GetCurrentTimestamp();
        }
        return timestamp;
    }
}