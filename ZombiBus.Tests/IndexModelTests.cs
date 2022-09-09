using System.Threading.Tasks;
using Moq;
using Xunit;
using ZombiBus.Core;
using ZombiBus.Pages;

namespace ZombiBugs.Tests;

public class IndexModelTests
{
    private Mock<IDeadLetterConnectionRepository> _repoMock;
    private Mock<IDeadLetterListenerScheduler> _schedulerMock;
    private IndexModel _sut;

    public IndexModelTests()
    {
        _schedulerMock = new Mock<IDeadLetterListenerScheduler>();
        _repoMock = new Mock<IDeadLetterConnectionRepository>();

        _sut = new IndexModel(_repoMock.Object, _schedulerMock.Object);
    }

    [Fact]
    public async Task OnWatchAsync_ShouldRemoveJob()
    {
        var connection = new DeadLetterConnection { Id = 2 };
        _repoMock.Setup(x => x.Find(2)).ReturnsAsync(connection);

        await _sut.OnPostWatchAsync(2);
        
        _schedulerMock.Verify(x => x.Start(connection));
    }
    
    [Fact]
    public async Task OnStopWatchAsync_ShouldRemoveJob()
    {
        var connection = new DeadLetterConnection { Id = 2 };
        _repoMock.Setup(x => x.Find(2)).ReturnsAsync(connection);

        await _sut.OnPostStopWatchAsync(2);
        
        _schedulerMock.Verify(x => x.Stop(connection));
    }
}