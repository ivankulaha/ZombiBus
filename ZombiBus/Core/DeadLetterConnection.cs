using System.ComponentModel.DataAnnotations;

namespace ZombiBus.Core;

public class DeadLetterConnection
{
    public int Id { get; set; }
    
    [Required, MaxLength(100)]
    public string QueueName { get; set; }
    
    [Required, MaxLength(1024)]
    public string ConnectionString { get; set; }
}