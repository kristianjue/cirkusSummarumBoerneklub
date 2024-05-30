namespace Core;

public class EmailRequest
{
    public string Subject { get; set; }
    public string Body { get; set; }
    
    public List<String> Recipients { get; set; }
}