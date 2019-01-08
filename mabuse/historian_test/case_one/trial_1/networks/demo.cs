public class demo
{
    private static IDemoInterface Idemo = new DemoInterface();

    public static int Main(string[] args)
    {
        int ReturnCode = 0;
        Console.WriteLine("Start app");
        try
        {
            ProcessCommand(args[1]);
        }
        catch(Exception Ex)
        {
            Console.WriteLine($"[ERROR] {Ex.Message}");
            Console.WriteLine($"[STACK] {Ex.StackTrace}");
            ReturnCode = 1;
        }
        finally
        {
            Console.WriteLine("End app");
        }
        return ReturnCode;
    }

    private static bool ProcessCommand(string command)
    {
        bool Success = false;
        switch (command)
        {
            case "Command1": Success = Idemo.Command1(); break;
            case "Command2": Success = IDemo.Command2(); break;
            case "Command3": Success = IDemo.Command3(); break;
            default: throw new Exception("Unrecognized command");
        }
    }
    return Success;
}

internal interface IDemoInterface
{
    bool Command1();
    bool Command2();
    bool Command3();
}

internal class DemoInterface : IDemoInterface
{
    public bool Command1()
    {
        throw new NotImplementedException();
    }

    public bool Command2()
    {
        throw new NotImplementedException();
    }

    public bool Command3() => SubDemo.Command3IsAlwaysTrue();
}

internal class SubDemo
{
    public static bool Command3IsAlwaysTrue()
    {
        return true;
    }
}