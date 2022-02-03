using System.ComponentModel.DataAnnotations.Schema;
using Xunit;

namespace Tremble.Tests;

public class ReflectionTests
{
    [Fact]
    public void AnnotatedClassesAreCorrectlyFound()
    {
        var types = Utilities.Reflection.FindAllTypesAnnotatedWith(typeof(TableAttribute));
        Assert.Contains(typeof(Yes), types);
        Assert.DoesNotContain(typeof(No), types);
    }

    [Table("")] private class Yes { }
    private class No { }
}
