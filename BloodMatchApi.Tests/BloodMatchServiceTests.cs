using Xunit;

public class BloodMatchServiceTests
{
    [Fact]
    public void IsMatch_CoversAllCompatibilityPairs()
    {
        var service = new BloodMatchService();

        var compat = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>()
        {
            ["O-"] = new() { "O-" },
            ["O+"] = new() { "O-", "O+" },
            ["A-"] = new() { "O-", "A-" },
            ["A+"] = new() { "O-", "O+", "A-", "A+" },
            ["B-"] = new() { "O-", "B-" },
            ["B+"] = new() { "O-", "O+", "B-", "B+" },
            ["AB-"] = new() { "O-", "A-", "B-", "AB-" },
            ["AB+"] = new() { "O-", "O+", "A-", "A+", "B-", "B+", "AB-", "AB+" }
        };

        var allTypes = new System.Collections.Generic.HashSet<string>(compat.Keys);
        // also include donors that appear in lists (should be same set but safer)
        foreach (var list in compat.Values) foreach (var d in list) allTypes.Add(d);

        foreach (var recipient in allTypes)
        {
            foreach (var donor in allTypes)
            {
                var expected = compat.ContainsKey(recipient) && compat[recipient].Contains(donor);
                var actual = service.IsMatch(donor, recipient);
                Assert.Equal(expected, actual);
            }
        }
    }
}
