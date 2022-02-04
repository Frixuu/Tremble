using System.ComponentModel.DataAnnotations.Schema;
using Tremble.Utilities;
using Xunit;

namespace Tremble.Tests;

public class ReflectionTests
{
    [Fact]
    public void AnnotatedClassesAreCorrectlyFound()
    {
        var types = Reflections.FindTypesAnnotatedWith(typeof(TableAttribute));
        Assert.Contains(typeof(Yes), types);
        Assert.DoesNotContain(typeof(No), types);
    }

    [Table("")] private class Yes { }
    private class No { }
}
