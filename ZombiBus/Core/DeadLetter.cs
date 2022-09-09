namespace ZombiBus.Core;

public class DeadLetter
{
    public int Id { get; set; }
    public int ConnectionId { get; set; }
    public string Content { get; set; }
    public string Metadata { get; set; }
    public DateTime CollectedAt { get; set; }
}