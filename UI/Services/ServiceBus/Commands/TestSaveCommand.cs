namespace UI.Services.ServiceBus.Commands
{
    public interface TestSaveCommand
    {
        Guid CommandId { get; set; }
        string SomeProperty { get; set; } 
    }
}
