using projMaxPark.BL;
using Quartz;
using System.Threading.Tasks;

public class DailyAlgorithmJob : IJob
{
    private readonly SmartAlgorithm _smartAlgorithm;

    public DailyAlgorithmJob(SmartAlgorithm smartAlgorithm)
    {
        _smartAlgorithm = smartAlgorithm;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (_smartAlgorithm != null)
        {
            _smartAlgorithm.GetDailyAlgorithm();
        }

        await Task.CompletedTask; // If you have any asynchronous operations
    }
}
