using System.Linq;
using Xunit;

public class BloodMatchServiceTests
{
    [Fact]
    public void IsMatch_CoversAllCompatibilityPairs()
    {
        var service = new BloodMatchService();

        var compat = new System.Collections.Generic.Dictionary<BloodType, System.Collections.Generic.List<string>>()
        {
            [BloodType.ON] = new() { "O-" },
            [BloodType.OP] = new() { "O-", "O+" },
            [BloodType.AN] = new() { "O-", "A-" },
            [BloodType.AP] = new() { "O-", "O+", "A-", "A+" },
            [BloodType.BN] = new() { "O-", "B-" },
            [BloodType.BP] = new() { "O-", "O+", "B-", "B+" },
            [BloodType.ABN] = new() { "O-", "A-", "B-", "AB-" },
            [BloodType.ABP] = new() { "O-", "O+", "A-", "A+", "B-", "B+", "AB-", "AB+" }
        };

        // recipients: enum values
        var recipients = compat.Keys;

        // donors: all unique donor strings used in the compatibility lists
        var donors = new System.Collections.Generic.HashSet<string>(compat.Values.SelectMany(list => list));

        foreach (var recipient in recipients)
        {
            foreach (var donor in donors)
            {
                var expected = compat.ContainsKey(recipient) && compat[recipient].Contains(donor);
                var actual = service.IsMatch(donor, recipient);
                Assert.Equal(expected, actual);
            }
        }
    }

    [Fact]
    public void GetCompatibleDonors_ReturnsExpectedLists()
    {
        var service = new BloodMatchService();

        var compat = new System.Collections.Generic.Dictionary<BloodType, System.Collections.Generic.List<string>>()
        {
            [BloodType.ON] = new() { "O-" },
            [BloodType.OP] = new() { "O-", "O+" },
            [BloodType.AN] = new() { "O-", "A-" },
            [BloodType.AP] = new() { "O-", "O+", "A-", "A+" },
            [BloodType.BN] = new() { "O-", "B-" },
            [BloodType.BP] = new() { "O-", "O+", "B-", "B+" },
            [BloodType.ABN] = new() { "O-", "A-", "B-", "AB-" },
            [BloodType.ABP] = new() { "O-", "O+", "A-", "A+", "B-", "B+", "AB-", "AB+" }
        };

        foreach (var recipient in compat.Keys)
        {
            var expected = compat[recipient];
            var actual = service.GetBloodTypeMatch(recipient);
            Assert.Equal(expected, actual);
        }
    }

    [Fact]
    public void GetSupportedBloodTypes_ReturnsAllEnumNames()
    {
        var service = new BloodMatchService();
        var expected = Enum.GetNames<BloodType>();

        var actual = service.GetSupportedBloodTypes();

        Assert.Equal(expected, actual);
    }

}
