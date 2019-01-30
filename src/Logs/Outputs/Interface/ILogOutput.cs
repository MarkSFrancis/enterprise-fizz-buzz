namespace FizzBuzz.Logs.Outputs
{
    public interface ILogOutput
    {
        void WriteTrace(string message);

        void WriteInfo(string message);

        void WriteError(string message);

        void WriteWarning(string message);
    }
}
